using RPGMap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.AccessControl;

namespace FirstPlayable
{
    //C# naming convention is to use PascalCase for class names?

    internal class Map
    {
        private readonly string path;
        private readonly string[] floor;
        public List<ItemPickup> diamonds;
        public List<ItemPickup> powerPickups;
        public List<ItemPickup> healthPickups;
        public List<Enemy> CommonEnemy;
        public List<Enemy> boss;
        public List<Enemy> bomb;
        public Enemy enemy;

        public char[,] layout;
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int initialPlayerPositionX { get; set; }
        public int initialPlayerPositionY { get; set; }
        public int initialEnemyPositionX { get; set; }
        public int initialEnemyPositionY { get; set; }

        public Map(string mapFileName)
        {
            path = mapFileName;
            floor = File.ReadAllLines(path);
            diamonds = new List<ItemPickup>();
            powerPickups = new List<ItemPickup>();
            healthPickups = new List<ItemPickup>();
            CommonEnemy = new List<Enemy>();
            boss = new List<Enemy>();
            bomb = new List<Enemy>();
            CreateMap();
        }

        private void CreateMap()
        {
            mapWidth = floor[0].Length;
            mapHeight = floor.Length;
            layout = new char[mapHeight, mapWidth];

            for (int i = 0; i < mapHeight; i++)
            {
                for (int j = 0; j < mapWidth; j++)
                {
                    layout[i, j] = floor[i][j];

                    if (layout[i, j] == '!')
                    {
                        initialPlayerPositionX = j;
                        initialPlayerPositionY = i;
                    }
                    else if (layout[i, j] == 'E')
                    {
                        CommonEnemy.Add(new Enemy(10, 1, j, i));
                        layout[i,j] = '-';
                    }
                    if (layout[i, j] == 'P')
                    {
                        powerPickups.Add(new ItemPickup(j, i));
                    }
                    else if (layout[i, j] == 'H')
                    {
                        healthPickups.Add(new ItemPickup(j, i));
                    }
                }
            }
        }

        public void DrawMap(Player player, List<Enemy> CommonEnmey, Enemy boss, Enemy bomb)
        {
            Console.Clear();

            for (int k = 0; k < mapHeight; k++)
            {
                for (int l = 0; l < mapWidth; l++)
                {
                    char tile = layout[k, l];

                    switch (tile)
                    {
                        case '=':
                            Console.ForegroundColor = ConsoleColor.Green; break;
                        case '-':
                            Console.ForegroundColor = ConsoleColor.White; break;
                        case '#':
                            Console.ForegroundColor = ConsoleColor.DarkGreen; break;
                        case '@':
                            Console.ForegroundColor = ConsoleColor.Yellow; break;
                        case 'P':
                            Console.ForegroundColor = ConsoleColor.Magenta; break;
                        case 'H':
                            Console.ForegroundColor = ConsoleColor.Yellow; break;
                        case '^':
                            Console.ForegroundColor = ConsoleColor.Red; break;
                        case 'X':
                            Console.ForegroundColor = ConsoleColor.Blue; break;
                    }

                    if (tile == '#' && !player.levelComplete)
                    {
                        layout[k, l] = '#';
                    }
                    if (tile == '=' && !player.levelComplete)
                    {
                        player.positionX = l;
                        player.positionY = k - 1;
                        player.levelComplete = true;
                        layout[k, l] = '#';
                    }
                   //else if (tile == 'E' && !player.levelComplete)
                   //{
                   //    foreach (var commonEnemy in CommonEnemy)
                   //    {
                   //        //aaaenemy.DrawEnemy();
                   //        //Console.SetCursorPosition(commonEnemy.positionX, commonEnemy.positionY);
                   //        //Console.ForegroundColor = ConsoleColor.Red;
                   //        //Console.Write("E");
                   //        //Console.ResetColor();
                   //    }
                   //}
                    else if (tile == 'B' && !player.levelComplete)
                    {
                        boss.positionX = l;
                        boss.positionY = k;
                        layout[k, l] = '-';
                    }
                    else if (tile == 'W' && !player.levelComplete)
                    {
                        bomb.positionX = l;
                        bomb.positionY = k;
                        layout[k, l] = '-';
                    }
                    else if (tile == 'P' && !player.levelComplete)
                    {
                        foreach (var powerPickup in powerPickups)
                        {
                            if (powerPickup.X == l && powerPickup.Y == k)
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.Write("P");
                                Console.ResetColor();
                            }
                        }
                    }
                    else if (tile == 'H' && !player.levelComplete)
                    {
                        foreach (var healthPickup in healthPickups)
                        {
                            if (healthPickup.X == l && healthPickup.Y == k)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("H");
                                Console.ResetColor();
                            }
                        }
                    }
                    else if (tile == '@' && !player.levelComplete)
                    {
                        foreach (var diamond in diamonds)
                        {
                            if (diamond.X == l && diamond.Y == k)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.Write("@");
                                Console.ResetColor();
                            }
                        }
                    }
                    //bool pickupDrawn = false;
                    //if (!pickupDrawn)
                    //{
                    //    Console.Write(tile);
                    //}
                    //else if (tile == '@' && !player.levelComplete)
                    //{
                    //    bool diamondDrawn = false;
                    //    foreach (var diamond in diamonds)
                    //    {
                    //        if (diamond.X == l && diamond.Y == k)
                    //        {
                    //            Console.ForegroundColor = ConsoleColor.White;
                    //            diamondDrawn = true;
                    //            break;
                    //        }
                    //    }
                    //    if (!diamondDrawn)
                    //    {
                    //        Console.Write('-');
                    //    }
                    //}
                    else
                    {
                        Console.Write(tile);
                    }
                }
                Console.WriteLine();
            }
            player.DrawPlayer();
            foreach (var commonEnemy in CommonEnemy)
            {
                commonEnemy.DrawEnemy();
            }
            boss.DrawBoss();
            bomb.DrawBomb();

            Console.SetCursorPosition(0, 0);
        }
    }
}