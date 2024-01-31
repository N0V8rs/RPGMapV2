using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;

namespace RPGMap
{
    internal class Map
    {
        public string path; // Path for the Map
        public string DataMapText = @"DataMap.txt"; // Text File Name of the Map
        public string[] floorMap; // Map Draw
        public char[][] mapLayout; // Map Gentrated
        static int Diamonds; // Pickups
        static bool youWin; // You Win Check
        static bool gameOver; // Game Over Check
        static bool FloorComplete; // Floor Complete Check
        static int mapX; // Map Layout X
        static int mapY; // Map Layout Y
        static int maxX; // Map Maximun
        static int maxY; // Map Maximun

       //struct MapC
       //{
       //    public string path;
       //    public string[] floorMap;
       //    public char[][] mapLayout;
       //}

        public void DisplayHUD()
        {
            Console.SetCursorPosition(0, mapY + 1);
            //Console.WriteLine($"||Health: {playerHP}/{maxPlayerHP} | Diamonds Raided: {Diamonds} | Enemy Health: {enemyHP}/{maxEnemyHP} | Enemy 2 Health: {enemy2HP}/{maxEnemy2HP}||");
        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, mapY + 3);
            Console.WriteLine("| Map Legend");
            Console.SetCursorPosition(0, mapY + 4);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| Player = + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Enemy 1 = B ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" Enemy 2 =   |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("| Walls = # |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("| Floor = - |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("| Diamonds = @ ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(" SpikeTrap = ^ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" Exit = X |");
            Console.ResetColor();
            Console.WriteLine();
        }

        Map map = new Map();

        public void MapTxt()
        {
            map.path = @"DataMap.txt";
            map.floorMap = File.ReadAllLines(map.path);

            int MapX = map.floorMap.Length;
            int MapY = map.floorMap[0].Length;

            map.mapLayout = new char[MapX][];

            for (int i = 0; i < MapX; i++)
            {
                map.mapLayout[i] = new char[MapY];

                for (int j = 0; j < MapY; j++)
                {
                    map.mapLayout[i][j] = map.floorMap[i][j];
                }
            }

            DrawMap();
        }

        public void DrawMap()
        {
            Console.Clear();

            for (int k = 0; k < mapY; k++)
            {
                for (int l = 0; l < mapX; l++)
                {
                    Console.SetCursorPosition(k, l);
                    switch (mapLayout[k][l])
                    {
                        case '#':
                            Console.BackgroundColor = ConsoleColor.Green;
                            Console.Write(mapLayout[k][l]); break;
                        case 'B':
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(mapLayout[k][l]); break;
                        case '?':
                            Console.BackgroundColor = ConsoleColor.Magenta;
                            Console.Write(mapLayout[k][l]); break;
                        case '=':
                            Console.BackgroundColor = ConsoleColor.Blue;
                            Console.Write(mapLayout[k][l]); break;
                        case '^':
                            Console.BackgroundColor= ConsoleColor.DarkGray;
                            Console.Write(mapLayout[k][l]); break;
                        case '-':
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            Console.Write(mapLayout[k][l]); break;
                        case 'P':
                            Console.BackgroundColor= ConsoleColor.Red;
                            Console.Write(mapLayout[k][l]); break;
                    }
                }
            }
        }
    }
}