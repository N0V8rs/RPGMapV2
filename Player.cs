using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Player : Map
    {
        private ConsoleKeyInfo playerMovement;
        public int playerPosX; // Position of Player X
        public int playerPosY; // Position of Player Y
        public int maxPlayerHP; // Player Health can't go pass
        public int playerHP; // Player Health
        public int playerDamage; // Player Damage

        public int positionX {  get; set; }
        public int positionY { get; set; }

        Enemy1 enemy1 = new Enemy1();
        Enemy2 enemy2 = new Enemy2();
        Map mapPlayer = new Map();


        public void PlayerSpawn() 
        {
            positionX = 5;
            positionY = 10;
            Console.SetCursorPosition(positionX,positionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("P");
            Console.ResetColor();
        }

        public void PlayerInput()
        {
            bool moved = false; // Checks to see if the player moves

            int movementX;
            int movementY;

            int newPlayerPositionX = playerPosX;
            int newPlayerPositionY = playerPosY;

            moved = false;

            playerMovement = Console.ReadKey(true);

            if (moved == false)
            {
                // Attack Key for the player
                if (playerMovement.Key == ConsoleKey.Spacebar)
                {
                    AttackEnemy();
                    AttackEnemy2();
                    moved = true;
                    return;
                }
            }

            // Up movement for the player
            if (playerMovement.Key == ConsoleKey.UpArrow || playerMovement.Key == ConsoleKey.W)
            {
                movementY = Math.Max(playerPosY - 1, 0);

                if (mapLayout[movementY, playerPosX] != '#')
                {
                    moved = true;
                    playerPosY = movementY;
                    if (playerPosY <= 0)
                    {
                        playerPosY = 0;
                    }

                    if (movementY == enemyPosY && playerPosX == enemyPosX)
                    {
                        enemyHP -= 1;
                        if (enemyHP <= 0)
                        {
                            enemyPosX = 0;
                            enemyPosY = 0;
                            enemyAlive = false;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == '^')
                    {
                        playerHP -= 1;
                        if (playerHP <= 0)
                        {
                            gameOver = true;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == 'B')
                    {
                        movementY = playerPosY;
                        playerPosY = movementY;
                        return;
                    }

                    if (mapLayout[playerPosY, playerPosX] == '?')
                    {
                        movementY = playerPosY;
                        playerPosY = movementY;
                        return;
                    }
                }
            }

            // Down movement for the player
            if (playerMovement.Key == ConsoleKey.DownArrow || playerMovement.Key == ConsoleKey.S)
            {
                movementY = Math.Min(playerPosY + 1, maxY);

                if (mapLayout[movementY, playerPosX] != '#')
                {
                    moved = true;
                    playerPosY = movementY;
                    if (playerPosY >= maxY)
                    {
                        playerPosY = maxY;
                    }

                    if (movementY == enemyPosY && playerPosX == enemyPosX)
                    {
                        enemyHP -= 1;
                        if (enemyHP <= 0)
                        {
                            enemyPosX = 0;
                            enemyPosY = 0;
                            enemyAlive = false;
                        }
                    }

                    if (mapLayout[movementY, playerPosX] == '^')
                    {
                        playerHP -= 1;
                        if (playerHP <= 0)
                        {
                            gameOver = true;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == 'B')
                    {
                        movementY = playerPosY;
                        playerPosY = movementY;
                        return;
                    }

                    if (mapLayout[playerPosY, playerPosX] == '?')
                    {
                        movementY = playerPosY;
                        playerPosY = movementY;
                        return;
                    }
                }
            }

            // Left movement for the player
            if (playerMovement.Key == ConsoleKey.LeftArrow || playerMovement.Key == ConsoleKey.A)
            {
                movementX = Math.Max(playerPosX - 1, 0);

                if (mapLayout[playerPosY, movementX] != '#')
                {
                    moved = true;
                    playerPosX = movementX;
                    if (playerPosX <= 0)
                    {
                        playerPosX = 0;
                    }

                    if (movementX == enemyPosX && playerPosY == enemyPosY)
                    {
                        enemyHP -= 1;
                        if (enemyHP <= 0)
                        {
                            enemyPosX = 0;
                            enemyPosY = 0;
                            enemyAlive = false;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == '^')
                    {
                        playerHP -= 1;
                        if (playerHP <= 0)
                        {
                            gameOver = true;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == 'B')
                    {
                        movementX = playerPosX;
                        playerPosX = movementX;
                        return;
                    }

                    if (mapLayout[playerPosY, playerPosX] == '?')
                    {
                        movementX = playerPosY;
                        playerPosY = movementX;
                        return;
                    }
                }
            }

            // Right movement for the player
            if (playerMovement.Key == ConsoleKey.RightArrow || playerMovement.Key == ConsoleKey.D)
            {
                movementX = Math.Min(playerPosX + 1, maxX);

                if (mapLayout[playerPosY, movementX] != '#')
                {
                    moved = true;
                    playerPosX = movementX;
                    if (playerPosX >= maxX)
                    {
                        playerPosX = maxX;
                    }

                    if (movementX == enemyPosX && playerPosY == enemyPosY)
                    {
                        enemyHP -= 1;
                        if (enemyHP <= 0)
                        {
                            enemyPosX = 0;
                            enemyPosY = 0;
                            enemyAlive = false;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == '^')
                    {
                        playerHP -= 1;
                        if (playerHP <= 0)
                        {
                            gameOver = true;
                        }
                    }

                    if (mapLayout[playerPosY, playerPosX] == 'B')
                    {
                        movementX = playerPosX;
                        playerPosX = movementX;
                        return;
                    }

                    if (mapLayout[playerPosY, playerPosX] == '?')
                    {
                        movementX = playerPosY;
                        playerPosY = movementX;
                        return;
                    }
                }
            }

            // Exit of the level
            if (mapLayout[playerPosY, playerPosX] == 'X')
            {
                youWin = true;
                gameOver = true;
            }

            // Collectible Diamonds
            if (mapLayout[playerPosY, playerPosX] == '@')
            {
                Diamonds += 1;
                mapLayout[playerPosY, playerPosX] = '-';
            }

            // Exit game using a key
            if (playerMovement.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }


        public void Heal(int hp)
        {
            Health = hp;
            Shield = hp;

            if (hp  < 0)
            {
                hp = 0;
            }
        }

        public void Attack(int Damage)
        {
            if (Health > 0)
            {
                Health -= Damage;
            }
        }
    }
}
