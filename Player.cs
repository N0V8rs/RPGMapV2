using RPGMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace FirstPlayable
{
    internal class Player
    {
        // variables | encapsulation

        public HPManager HPManager;
        private ItemPickup Power;
        public Map map;

        public Player(int maxHealth, int health, int damage, int startX, int startY)
        {
            HPManager = new HPManager(maxHealth);
            HPManager.Heal(health);
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
        }

        public int currentDiamond { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }
        public int playerDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool youWin { get; set; }

        public void DrawExit()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("X");
            Console.ResetColor();
        }

        public void DrawPlayer()
        {
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("!");
            Console.ResetColor();
        }

        public void HealFullHealth()
        {
            HPManager.Heal(HPManager.ReturnsMaxHP() - HPManager.ReturnsCurrentHP());
        }

        public void IncreaseDamage()
        {
            GameSettings.PlayerDamage += GameSettings.PowerUp;
        }

        // recieves player input
        public void PlayerInput(Map map, List<Enemy> commonEnemy, Enemy boss, Enemy bomb)
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
                foreach (var enemy in commonEnemy)
                {
                    AttackEnemy(enemy);
                }
                moved = true;
                return;
            }

            // moves up
            if (playerController.Key == ConsoleKey.UpArrow || playerController.Key == ConsoleKey.W)
            {
                movementY = Math.Max(positionY - 1, 0);
                PlayerMovement(map, commonEnemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                PlayerMovement(map, commonEnemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                PlayerMovement(map, commonEnemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                PlayerMovement(map, commonEnemy, boss, bomb, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
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
            if (map.layout[movementY, movementX] == 'P')
            {
                IncreaseDamage();
                map.layout[movementY, movementX] = '.'; // replace the power-up with a floor tile or empty space
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

            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        private void AttackEnemy(Enemy enemy)
        {
            if (Math.Abs(positionX - enemy.positionX) <= 1 && Math.Abs(positionY - enemy.positionY) <= 1)
            {
                enemy.HPManager.Damage(GameSettings.PlayerDamage);
                if (enemy.HPManager.IsDead())
                {
                    enemy.positionX = 0;
                    enemy.positionY = 0;
                    enemy.enemyAlive = false;
                }
            }
        }

        private void PlayerMovement(Map map, List<Enemy> commonEnemy, Enemy boss, Enemy bomb, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {
                foreach (var enemy in commonEnemy)
                {
                    if (movementY == enemy.positionY && movementX == enemy.positionX)
                    {
                        enemy.HPManager.Damage(GameSettings.PlayerDamage);
                        if (enemy.HPManager.IsDead())
                        {
                            enemy.positionX = 0;
                            enemy.positionY = 0;
                            enemy.enemyAlive = false;
                        }
                        return;
                    }
                }

                if (movementY == boss.positionY && movementX == boss.positionX)
                {
                    boss.HPManager.Damage(GameSettings.PlayerDamage);
                    if (boss.HPManager.IsDead())
                    {
                        boss.positionX = 0;
                        boss.positionY = 0;
                        boss.enemyAlive = false;
                    }
                    return;
                }

                if (movementY == bomb.positionY && movementX == bomb.positionX)
                {
                    bomb.HPManager.Damage(GameSettings.PlayerDamage);
                    if (bomb.HPManager.IsDead())
                    {
                        bomb.positionX = 0;
                        bomb.positionY = 0;
                        bomb.enemyAlive = false;
                    }
                    return;
                }

                if (moved == false && map.layout[movementY, movementX] != '#')
                {
                    foreach (var enemy in commonEnemy)
                    {
                        if (movementY == enemy.positionY && movementX == enemy.positionX)
                        {
                            AttackEnemy(enemy);
                            return;
                        }
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
        }
    }
}