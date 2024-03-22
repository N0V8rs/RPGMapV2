using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPGMap;

namespace FirstPlayable
{
    internal class Map
    {
        // variables | encapsulation

        private string path;
        private string[] floor;
        public char[,] layout;
        private bool mapDrawn = false;
        public int mapWidth { get; set; }
        public int mapHeight { get; set; }
        public int initialPlayerPositionX { get; set; }
        public int initialPlayerPositionY { get; set; }
        public int initialEnemyPositionX { get; set; }
        public int initialEnemyPositionY { get; set; }

        private List<EnemyManager> enemies;
        private Settings settings = new Settings();

        public Map(string mapFileName, List<EnemyManager> enemies)
        {
            this.enemies = enemies;
            path = mapFileName;
            floor = File.ReadAllLines(path);
            CreateMap();
        }


        // creates map
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

                    if (layout[i, j] == '-')
                    {
                        initialPlayerPositionX = j;
                        initialPlayerPositionY = i;
                    }
                    if (layout[i, j] == '=')
                    {
                        initialPlayerPositionX = j;
                        initialPlayerPositionY = i;
                        layout[i, j] = '-';
                    }
                    else if (layout[i, j] == 'E')
                    {
                        layout[i, j] = '-';

                        initialEnemyPositionX = j;
                        initialEnemyPositionY = i;
                        var enemy = new CommonEnemy(settings.CommonEnemyInitialHealth, settings.CommonEnemyInitialDamage, j, i, "CommonEnemy", layout);
                        enemies.Add(enemy);
                    }
                    else if (layout[i, j] == 'W')
                    {
                        layout[i, j] = '-';
                        var sniper = new SniperEnemy(settings.SniperInitialHealth, settings.SniperInitialDamage, j, i, "Sniper", layout);
                        enemies.Add(sniper);
                    }
                    else if (layout[i, j] == 'B')
                    {
                        layout[i, j] = '-';
                        var boss = new BossEnemy(settings.BossInitialHealth, settings.BossInitialDamage, j, i, "Boss", layout);
                        enemies.Add(boss);
                    }
                }
            }
        }

        // draws out map on screen
        public void UpdateMap(Player player, SniperEnemy sniper, BossEnemy boss, CommonEnemy enemy)
        {
            if (!mapDrawn)
            {
                Console.Clear();

                Console.BackgroundColor = ConsoleColor.DarkGray;

                for (int k = 0; k < mapHeight; k++)
                {
                    for (int l = 0; l < mapWidth; l++)
                    {
                        char tile = layout[k, l];
                        switch (tile)
                        {
                            case '=':
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case '-':
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                            case '@':
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case '^':
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case '!':
                                Console.ForegroundColor = ConsoleColor.Blue;
                                break;
                            case 'E':
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case 'B':
                                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                                break;
                            case '#':
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                break;
                            case 'X':
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                break;
                            case 'H':
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case 'P':
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                break;
                            default:
                                Console.ForegroundColor = ConsoleColor.Gray;
                                break;
                        }


                        if (tile == '=' && !player.levelComplete)
                        {
                            player.positionX = l;
                            player.positionY = k - 1;
                            player.levelComplete = true;
                            layout[k, l] = '-';
                        }

                        if (tile == 'E' && !player.levelComplete)
                        {
                            enemy.positionX = l;
                            enemy.positionY = k;
                            layout[k, l] = '-';
                        }
                        if (tile == 'B' && !player.levelComplete)
                        {
                            boss.positionX = l;
                            boss.positionY = k;

                            layout[k, l] = '-';
                        }
                        if (tile == 'W' && !player.levelComplete)
                        {
                            sniper.positionX = l;
                            sniper.positionY = k;
                            layout[k, l] = '-';
                        }


                        Console.Write(tile);
                    }
                    Console.WriteLine();
                    mapDrawn = true;
                }


            }
            player.Draw();


            foreach (var enemies in enemies)
            {
                enemies.Draw();
            }

            Console.SetCursorPosition(0, 0);


        }


    }
}