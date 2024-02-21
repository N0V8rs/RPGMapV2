using System;
using static RPGMap.Character;

namespace RPGMap
{
    internal class Player
    {
        public int maxHP {  get; set; }
        public int currentHP {  get; set; }
        public int playerDamage {  get; set; }
        public int posX {  get; set; }
        public int posY { get; set; }
        public bool YouWin { get; set; }
        public bool GameOver => gameOver;
        public  bool gameOver;
        Enemy enemy;



        public Player(int maxPlayerHP, int playerHP, int damage, int startPosX, int startPosY)
        {
            maxHP = maxPlayerHP;
            currentHP = playerHP;
            playerDamage = damage;
            posX = startPosX;
            posY = startPosY;
        }

        public void CheckForWin(Map map, Exit exit)
        {
            if (map.mapLayout[posY, posX] == 'X')
            {
                YouWin = true;
            }
        }

        public void ReceiveDamage(int damage) // Player takes damage
        {
            currentHP -= damage;

            if (currentHP <= 0)
            {
                currentHP = 0;
                gameOver = true;
            }
        }

        public void AttackEnemy()
        {
            if (Math.Abs(posX - enemy.posX) <= 1 && Math.Abs(posY - enemy.posY) <= 1)
            {
                enemy.ReceiveDamage(playerDamage);
            }
        }

        public void PlayerInput(Map map, Exit exit) // Moves the player 
        {
            bool moved = false; // Checks for the moved

            int movementX = posX;
            int movementY = posY;

            ConsoleKeyInfo playerMovement = Console.ReadKey(true);

            if (playerMovement.Key == ConsoleKey.Spacebar) // Attack the enemy
            {
                AttackEnemy();
                moved = true;
            }

            if (!moved)
            {
                switch (playerMovement.Key)
                {
                    case ConsoleKey.UpArrow:
                    case ConsoleKey.W:
                        movementY = Math.Max(posY - 1, 0);
                        break;

                    case ConsoleKey.DownArrow:
                    case ConsoleKey.S:
                        movementY = Math.Min(posY + 1, map.maxY);
                        break;

                    case ConsoleKey.LeftArrow:
                    case ConsoleKey.A:
                        movementX = Math.Max(posX - 1, 0);
                        break;

                    case ConsoleKey.RightArrow:
                    case ConsoleKey.D:
                        movementX = Math.Min(posX + 1, map.maxX);
                        break;
                }

                if (map.mapLayout[movementY, movementX] != '#')
                {
                    posX = movementX;
                    posY = movementY;
                }

                if (playerMovement.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(1);
                }
            }
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("+");
            Console.ResetColor();
        }
    }
}

