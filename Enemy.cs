using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Enemy
    {
        // variables | encapsulation

        public HPManager HPManager;
        Player player;
        public int enemyDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool enemyAlive { get; set; }
        public int movesSinceLastAttack;

        public Enemy(int maxHealth, int damage, int startX, int startY)
        {
            HPManager = new HPManager(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = true;
        }

        public Enemy(int maxHealth, int damage, int startX, int startY, bool isAlive)
        {
            HPManager = new HPManager(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = isAlive;
        }
        public Enemy(int maxHealth, int damage, int startX, int startY, int turns)
        {
            HPManager = new HPManager(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            enemyAlive = true;
            movesSinceLastAttack = turns;
        }

        public void EnemyMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout)
        {
            int enemyMovementX = positionX;
            int enemyMovementY = positionY;
            int newEnemyPositionX = positionX;
            int newEnemyPositionY = positionY;

            Random randomRoll = new Random();

            if (enemyAlive)
            {
                int rollResult = randomRoll.Next(1, 5);
                while ((enemyMovementX == playerX && enemyMovementY == playerY) ||
                       (enemyMovementX == newEnemyPositionX && enemyMovementY == newEnemyPositionY) ||
                       mapLayout[enemyMovementY, enemyMovementX] == '#' || // Walls
                       mapLayout[enemyMovementY, enemyMovementX] == 'P' || // Power pickup
                       mapLayout[enemyMovementY, enemyMovementX] == 'H' || // Health pickup
                       mapLayout[enemyMovementY, enemyMovementX] == '^')   // Spike trap
                {
                    rollResult = randomRoll.Next(1, 5);

                    if (rollResult == 1)
                    {
                        enemyMovementY = positionY + 1;
                        if (enemyMovementY >= mapHeight)
                        {
                            enemyMovementY = mapHeight - 1;
                        }
                    }
                    else if (rollResult == 2)
                    {
                        enemyMovementY = positionY - 1;
                        if (enemyMovementY <= 0)
                        {
                            enemyMovementY = 0;
                        }
                    }
                    else if (rollResult == 3)
                    {
                        enemyMovementX = positionX - 1;
                        if (enemyMovementX <= 0)
                        {
                            enemyMovementX = 0;
                        }
                    }
                    else // rollResult == 4
                    {
                        enemyMovementX = positionX + 1;
                        if (enemyMovementX >= mapWidth)
                        {
                            enemyMovementX = mapWidth - 1;
                        }
                    }
                }
            }

            // Update enemy position
            positionY = enemyMovementY;
            positionX = enemyMovementX;
        }

        public void MoveTowardsPlayer(int playerPosX, int playerPosY, char[,] mapLayout)
        {
            if (enemyAlive)
            {
                int playerDistanceX = Math.Abs(playerPosX - positionX);
                int playerDistanceY = Math.Abs(playerPosY - positionY);
                int enemyMovementX = positionX;
                int enemyMovementY = positionY;
                int newEnemyPositionX = enemyMovementX;
                int newEnemyPositionY = enemyMovementY;

                // Checks to see if the player is near
                if (playerDistanceX <= 2 && playerDistanceY <= 2)
                {
                    // Moves towards the player
                    if (playerPosX < positionX && mapLayout[positionY, positionX - 1] != '#')
                    {
                        enemyMovementX--;
                    }
                    else if (playerPosX > positionX && mapLayout[positionY, positionX + 1] != '#')
                    {
                        enemyMovementX++;
                    }

                    if (playerPosY < positionY && mapLayout[positionY - 1, positionX] != '#')
                    {
                        enemyMovementY--;
                    }
                    else if (playerPosY > positionY && mapLayout[positionY + 1, positionX] != '#')
                    {
                        enemyMovementY++;
                    }
                }

                if ((enemyMovementX != playerPosX || enemyMovementY != playerPosY) &&
                    mapLayout[enemyMovementY, enemyMovementX] != '#')
                {
                   
                    mapLayout[newEnemyPositionY, newEnemyPositionX] = '-';
                  
                    if (enemyAlive)
                    {
                        mapLayout[enemyMovementY, enemyMovementX] = '-';
                        positionX = enemyMovementX;
                        positionY = enemyMovementY;
                        newEnemyPositionX = positionX;
                        newEnemyPositionY = positionY;
                    }
                }
            }
        }
        public void EnemyMovement(Player player, char[,] mapLayout)
        {
            int distanceX = Math.Abs(positionX - player.positionX);
            int distanceY = Math.Abs(positionY - player.positionY);

            if (distanceX <= 2 && distanceY <= 2)
            {
                if (distanceX <= 1 && distanceY <= 1)
                {
                    AttackPlayer(player);
                }
                else
                {
                    MoveTowardsPlayer(player.positionX, player.positionY, mapLayout);
                }
            }
        }

        public void AttackEveryTwoMoves(Player player)
        {
            movesSinceLastAttack++;

            if (movesSinceLastAttack == 2 && enemyAlive)
            {
                player.HPManager.Damage(enemyDamage);

                if (player.HPManager.IsDead())
                {
                    player.gameOver = true;
                }

                movesSinceLastAttack = 0;
            }
        }


        public void AttackPlayer(Player player)
        {
            // Calculate the distance between the enemy and the player along both axes
            int distanceX = Math.Abs(positionX - player.positionX);
            int distanceY = Math.Abs(positionY - player.positionY);

            // Check if the enemy and the player are aligned either horizontally or vertically
            if ((distanceX == 0 && distanceY <= 1) || (distanceY == 0 && distanceX <= 1))
            {
                player.HPManager.Damage(enemyDamage); 

                // Check if the player is defeated
                if (player.HPManager.IsDead())
                {
                    Console.WriteLine("Player has been defeated by an enemy!");
                    player.gameOver = true;
                }
            }
        }

        public void DrawEnemy()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("E");
                Console.ResetColor();
            }
        }
        public void DrawBomb()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("W");
                Console.ResetColor();
            }
        }

        public void DrawBoss()
        {
            if (enemyAlive == true)
            {
                Console.SetCursorPosition(positionX, positionY);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("B");
                Console.ResetColor();
            }
        }
    }
}
