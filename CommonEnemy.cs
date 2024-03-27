using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FirstPlayable
{
    public class CommonEnemy : EnemyManager
    {

        public CommonEnemy(int maxHealth, int damage, int startX, int startY, string name, char[,] mapLayout) : base(maxHealth, damage, startX, startY, name, mapLayout)
        {
            healthSystem = new HPManager(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            enemyAlive = true;
            Name = name;
            icon = 'E';
        }

        public override void Draw()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(icon);
                Console.ResetColor();
            }
        }

        public override void DrawMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout, Player player, List<EnemyManager> enemies)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;
            int newEnemyPositionX = positionX;
            int newEnemyPositionY = positionY;

            // random roll to move
            Random randomRoll = new Random();

            // checks if enemy is alive so it doesn't bug out when it is actually killed
            if (enemyAlive)
            {
                int rollResult = randomRoll.Next(1, 5);

                // Calculate the new position based on the roll result
                switch (rollResult)
                {
                    case 1:
                        enemyMovementY = Math.Min(positionY + 1, mapHeight - 1);
                        break;
                    case 2:
                        enemyMovementY = Math.Max(positionY - 1, 0);
                        break;
                    case 3:
                        enemyMovementX = Math.Max(positionX - 1, 0);
                        break;
                    case 4:
                        enemyMovementX = Math.Min(positionX + 1, mapWidth - 1);
                        break;
                }

                char nextTile = mapLayout[enemyMovementY, enemyMovementX];
                if (nextTile == '#' || nextTile == '@' || nextTile == 'H' || nextTile == '!')
                {
                    return;
                }
                if (enemyMovementX == playerX && enemyMovementY == playerY)
                {
                    return;
                }



                if (mapLayout[newEnemyPositionY, newEnemyPositionX] != '#')
                {

                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);


                    positionX = newEnemyPositionX;
                    positionY = newEnemyPositionY;


                    currentTile = mapLayout[newEnemyPositionY, newEnemyPositionX];
                }


                if (enemyMovementX == playerX && enemyMovementY == playerY)
                {
                    player.healthSystem.Damage(enemyDamage);
                    player.UpdateLiveLog($"Enemy dealt {enemyDamage} damage to you!");
                    if (player.healthSystem.IsDead())
                    {
                        player.gameOver = true;
                    }
                }


                if (mapLayout[newEnemyPositionY, newEnemyPositionX] == '#')
                {

                    return;
                }

                // Clear the old position of the enemy on the map layout
                mapLayout[positionY, positionX] = '-';

                // Redraw the old position
                Console.SetCursorPosition(positionX, positionY);
                Console.Write('-');

                // Update the enemy's position
                positionY = enemyMovementY;
                positionX = enemyMovementX;

                // Update the enemy's position on the map layout
                mapLayout[positionY, positionX] = icon;

                // Redraw the new position
                Console.SetCursorPosition(positionX, positionY);
                Console.Write(icon);
            }
        }
    }
}