using RPGMap;
using System;

namespace FirstPlayable
{

    internal class Enemy
    {
        public HPManager HPManager;
        private Player player;
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

            Random randomRoll = new Random();

            if (enemyAlive)
            {
                int rollResult = randomRoll.Next(1, 5);

                switch (rollResult)
                {
                    case 1:
                        enemyMovementY = positionY + 1;
                        if (enemyMovementY >= mapHeight || mapLayout[enemyMovementY, enemyMovementX] == '#' || mapLayout[enemyMovementY, enemyMovementX] == 'P' || mapLayout[enemyMovementY, enemyMovementX] == 'H' || mapLayout[enemyMovementY, enemyMovementX] == '^')
                        {
                            enemyMovementY = positionY;
                        }
                        break;
                    case 2:
                        enemyMovementY = positionY - 1;
                        if (enemyMovementY < 0 || mapLayout[enemyMovementY, enemyMovementX] == '#' || mapLayout[enemyMovementY, enemyMovementX] == 'P' || mapLayout[enemyMovementY, enemyMovementX] == 'H' || mapLayout[enemyMovementY, enemyMovementX] == '^')
                        {
                            enemyMovementY = positionY;
                        }
                        break;
                    case 3:
                        enemyMovementX = positionX - 1;
                        if (enemyMovementX < 0 || mapLayout[enemyMovementY, enemyMovementX] == '#' || mapLayout[enemyMovementY, enemyMovementX] == 'P' || mapLayout[enemyMovementY, enemyMovementX] == 'H' || mapLayout[enemyMovementY, enemyMovementX] == '^')
                        {
                            enemyMovementX = positionX;
                        }
                        break;
                    case 4:
                        enemyMovementX = positionX + 1;
                        if (enemyMovementX >= mapWidth || mapLayout[enemyMovementY, enemyMovementX] == '#' || mapLayout[enemyMovementY, enemyMovementX] == 'P' || mapLayout[enemyMovementY, enemyMovementX] == 'H' || mapLayout[enemyMovementY, enemyMovementX] == '^')
                        {
                            enemyMovementX = positionX;
                        }
                        break;
                }

                positionY = enemyMovementY;
                positionX = enemyMovementX;
            }
        }

        public void MoveTowardsPlayer(Player player, char[,] mapLayout)
        {
            if (enemyAlive)
            {
                int playerDistanceX = Math.Abs(player.positionX - positionX);
                int playerDistanceY = Math.Abs(player.positionY - positionY);
                int enemyMovementX = positionX;
                int enemyMovementY = positionY;

                if (playerDistanceX <= 1 && playerDistanceY <= 1)
                {
                    player.HPManager.Damage(enemyDamage);

                    if (player.HPManager.IsDead())
                    {
                        player.gameOver = true;
                    }
                }
                else if (playerDistanceX <= 2 && playerDistanceY <= 2)
                {
                    int deltaX = Math.Sign(player.positionX - positionX);
                    int deltaY = Math.Sign(player.positionY - positionY);

                    enemyMovementX = positionX + deltaX;
                    enemyMovementY = positionY + deltaY;

                    if (mapLayout[enemyMovementY, enemyMovementX] == '^')
                    {
                        return;
                    }

                    if (mapLayout[enemyMovementY, enemyMovementX] != '#')
                    {
                        mapLayout[positionY, positionX] = '-';
                        positionX = enemyMovementX;
                        positionY = enemyMovementY;
                        mapLayout[positionY, positionX] = '-';
                    }
                }
            }
        }

        public void EnemyMovement2(Player player, char[,] mapLayout)
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
                    MoveTowardsPlayer(player, mapLayout);
                }
            }
        }

        public void AttackEveryTwoMoves(Player player, char[,] mapLayout)
        {
            movesSinceLastAttack++;

            if (movesSinceLastAttack >= 3 && enemyAlive)
            {
                player.HPManager.Damage(enemyDamage);

                if (player.HPManager.IsDead())
                {
                    player.gameOver = true;
                }
                int playerDirectionX = Math.Sign(player.positionX - positionX);
                int playerDirectionY = Math.Sign(player.positionY - positionY);

                int moveX = -playerDirectionX;
                int moveY = -playerDirectionY;

                if (mapLayout[positionY + moveY, positionX + moveX] != '#')
                {
                    mapLayout[positionY, positionX] = '-';
                    positionX += moveX;
                    positionY += moveY;
                    mapLayout[positionY, positionX] = '-';
                }

                movesSinceLastAttack = 0;
            }
        }

        public void AttackPlayer(Player player)
        {
            int distanceX = Math.Abs(positionX - player.positionX);
            int distanceY = Math.Abs(positionY - player.positionY);

            if ((distanceX == 0 && distanceY <= 1) || (distanceY == 0 && distanceX <= 1))
            {
                player.HPManager.Damage(enemyDamage);

                if (player.HPManager.IsDead())
                {
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