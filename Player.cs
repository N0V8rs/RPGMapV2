using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Player : Character
    {
        public int maxHP;
        public int currentHP;
        private int damage;
        internal bool YouWin {  get; set; }
        public bool GameOver => gameOver;
        private bool gameOver;

        public Player(int maxHP, int damage)
        {
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.damage = damage;
        }

        public void PlayerPosition() // Makes the player 
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("+");
            Console.ResetColor();
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
        public void AttackEnemy(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (Math.Abs(posX - enemy.posX) <= 1 && Math.Abs(posY - enemy.posY) <= 1)
                {
                    enemy.ReceiveDamage(damage);
                }
            }
        }
        public void PlayerInput(Map map, List<Enemy> enemies, Exit exit) // Moves the player 
        {
            bool moved = false; // Checks for the moved

            int movementX = posX;
            int movementY = posY;

            ConsoleKeyInfo playerMovement = Console.ReadKey(true);

            if (playerMovement.Key == ConsoleKey.Spacebar) // Attack the enemy
            {
                AttackEnemy(enemies);
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
    }
}
