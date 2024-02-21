using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Enemy : Character
    {
        public int MaxHP;
        public int currentHP;
        private int damage;
        public bool enemyAlive;
        private Map map;
        private Random randomRoll = new Random();
        public int PosX { get; set; }
        public int PosY { get; set; }

        public Enemy(int enemyMaxHP, int enemyHP, int enemyPosX, int enemyPosY)
        {
            PosX = enemyPosX;
            PosY = enemyPosY;
            MaxHP = enemyMaxHP;
            currentHP = enemyHP;
        }
    
        public void EnemyPosition()
        {
            Console.SetCursorPosition(posX, posY);

            if (enemyAlive)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("B");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("-");
            }

            Console.ResetColor();
        }


        public void Move(Map map, Player player)
        {
            if (!enemyAlive)
            {
                return;
            }

            int newEnemyPositionX;
            int newEnemyPositionY;

            do
            {
                int rollResult = randomRoll.Next(1, 5);

                // Random movement by one tile 
                switch (rollResult)
                {
                    case 1:
                        newEnemyPositionY = Math.Min(posY + 1, map.maxY);
                        newEnemyPositionX = posX;
                        break;
                    case 2:
                        newEnemyPositionY = Math.Max(posY - 1, 0);
                        newEnemyPositionX = posX;
                        break;
                    case 3:
                        newEnemyPositionX = Math.Max(posX - 1, 0);
                        newEnemyPositionY = posY;
                        break;
                    case 4:
                        newEnemyPositionX = Math.Min(posX + 1, map.maxX);
                        newEnemyPositionY = posY;
                        break;
                    default:
                        newEnemyPositionX = posX;
                        newEnemyPositionY = posY;
                        break;
                }

                if (map.mapLayout[newEnemyPositionY, newEnemyPositionX] != '#' &&
                    map.mapLayout[newEnemyPositionY, newEnemyPositionX] != '^' &&
                    map.mapLayout[newEnemyPositionY, newEnemyPositionX] != 'X' &&
                    !(newEnemyPositionX == player.posX && newEnemyPositionY == player.posY))
                {
                    map.mapLayout[posY, posX] = '-'; 

                    
                    if (enemyAlive)
                    {
                        map.mapLayout[newEnemyPositionY, newEnemyPositionX] = 'B';
                        posX = newEnemyPositionX;
                        posY = newEnemyPositionY;
                    }
                    else
                    {
                        map.mapLayout[newEnemyPositionY, newEnemyPositionX] = '-';
                    }

                    break; 
                }
            } while (true); 
        }
        public void Attack(Player player)
        {
            if (!enemyAlive)
            {
                return;
            }

            // Attacks the player next to the player
            if ((Math.Abs(player.posX - posX) == 1 && player.posY == posY) ||
                (Math.Abs(player.posY - posY) == 1 && player.posX == posX))
            { 
                player.ReceiveDamage(damage);
            }
        }
        public void ReceiveDamage(int damage) // Damage to player, if enemy is alive
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                enemyAlive = false;
            }
        }
    }
}
