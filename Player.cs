using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class Player
    {
        // variables | encapsulation

        public HPManager HPManager;
        ItemPickup Power;
        public int playerDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public int currentDiamond { get; set; }
        public bool youWin { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }

        public Player(int maxHealth, int health, int damage, int startX, int startY)
        {
            HPManager = new HPManager(maxHealth);
            HPManager.Heal(health);
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
        }

        // recieves player input
        public void PlayerInput(Map map, Enemy enemy, Enemy boss, Enemy bomb)
        {
            ConsoleKeyInfo playerController;
            bool moved = false;

            int movementX = positionX;
            int movementY = positionY;

            int newPlayerPositionX = positionX;
            int newPlayerPositionY = positionY;

            moved = false;

            playerController = Console.ReadKey(true);

            if (moved == false && playerController.Key == ConsoleKey.Spacebar)
            {
                AttackEnemy(enemy);
                moved = true;
                return;
            }

            // moves up
            if (playerController.Key == ConsoleKey.UpArrow || playerController.Key == ConsoleKey.W)
            {
                movementY = Math.Max(positionY - 1, 0);
                HandleMovement(map, enemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                HandleMovement(map, enemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                HandleMovement(map, enemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                HandleMovement(map, enemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // winning door
            if (map.layout[positionY, positionX] == 'X')
            {
                youWin = true;
                gameOver = true;
            }

            if (map.layout[positionY, positionX] == '@')
            {
                currentDiamond += 1;
                map.layout[positionY, positionX] = '-'; 
                foreach (var diamond in map.diamonds)
                {
                    if (diamond.X == positionX && diamond.Y == positionY)
                    {
                        diamond.Collect(); 
                        break; 
                    }
                }
            }
            if (map.layout[positionY, positionX] == 'P')
            {
                IncreaseDamage();
                map.layout[positionY, positionX] = '-';
                foreach (var powerPickup in map.powerPickups)
                {
                    if (powerPickup.X == positionX && powerPickup.Y == positionY)
                    {
                        powerPickup.Collect();
                        break;
                    }
                }
            }

            if (map.layout[positionY, positionX] == 'H')
            {
                HealFullHealth();
                map.layout[positionY, positionX] = '-';
                foreach (var healthPickup in map.healthPickups)
                {
                    if (healthPickup.X == positionX && healthPickup.Y == positionY)
                    {
                        healthPickup.Collect();
                        break;
                    }
                }
            }

            // exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        // handles things like collision checks and what the player is moving towards
        private void HandleMovement(Map map, Enemy enemy, Enemy boss, Enemy bomb, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {
                if (movementY == enemy.positionY && movementX == enemy.positionX)
                {
                    enemy.HPManager.Damage(playerDamage);
                    if (enemy.HPManager.IsDead())
                    {
                        enemy.positionX = 0;
                        enemy.positionY = 0;
                        enemy.enemyAlive = false;
                    }
                    return;
                }
                if (movementY == boss.positionY && movementX == boss.positionX)
                {
                    boss.HPManager.Damage(playerDamage);
                    if (boss.HPManager.IsDead())
                    {
                        boss.positionX = 0;
                        boss.positionY = 0;
                        boss.enemyAlive = false;
                    }
                    return;
                }
                if(movementY == bomb.positionY && movementX == bomb.positionX)
                {
                    bomb.HPManager.Damage(playerDamage);
                    if (bomb.HPManager.IsDead())
                    {
                        bomb.positionX = 0;
                        bomb.positionY = 0;
                        bomb.enemyAlive = false;
                    }
                    return;
                }

                if (map.layout[movementY, movementX] == '^')
                {
                    HPManager.Damage(1);
                    if (HPManager.IsDead())
                    {
                        gameOver = true;
                    }
                }

                if (map.layout[movementY, movementX] == 'E')
                {
                    movementY = positionY;
                    movementX = positionX;
                    return;
                }

                else
                {
                    moved = true;
                    positionY = movementY;
                    positionX = movementX;
                }
            }
        }
        public void IncreaseDamage()
        {
            playerDamage += 1;
        }

        public void HealFullHealth()
        {
            HPManager.Heal(HPManager.ReturnsMaxHP() - HPManager.ReturnsCurrentHP());
        }

        private void AttackEnemy(Enemy enemy)
        {
            if (Math.Abs(positionX - enemy.positionX) <= 1 && Math.Abs(positionY - enemy.positionY) <= 1)
            {
                enemy.HPManager.Damage(playerDamage);
                if (enemy.HPManager.IsDead())
                {
                    enemy.positionX = 0;
                    enemy.positionY = 0;
                    enemy.enemyAlive = false;
                }
            }
        }
        private void AttackBoss(Enemy boss)
        {
            if (Math.Abs(positionX - boss.positionX) <= 1 && Math.Abs(positionY - boss.positionY) <= 1)
            {
                boss.HPManager.Damage(playerDamage);
                if (boss.HPManager.IsDead())
                {
                    boss.positionX = 0;
                    boss.positionY = 0;
                    boss.enemyAlive = false;
                }
            }
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("!");
            Console.ResetColor();
        }

        public void DrawExit()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("X");
            Console.ResetColor();

        }
    }
}
