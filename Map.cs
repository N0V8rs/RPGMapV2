using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Map
    {
        private string path;
        private string[] floorMap;
        public char[,] mapLayout;
        public int maxX;
        public int maxY;
        Enemy enemy;

        public Map(string filepath)
        {
            path = filepath;
            floorMap = File.ReadAllLines(filepath);
            maxX = floorMap[0].Length;
            maxY = floorMap.Length;
            CreateMap();
        }

        private void CreateMap() // Create the arrays
        {
            mapLayout = new char[maxY, maxX];

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    mapLayout[i, j] = floorMap[i][j];
                }
            }
        }

        public void DrawMap(Player player, Exit exit)
        {
            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    char currentTile = mapLayout[i, j];

                    if (currentTile == 'B')
                    {
                        enemy.posX = i;
                        enemy.posY = j;
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("B"); // Draw the enemy
                        Console.ResetColor();
                    }
                    else
                    {
                        // Set color for other map tiles
                        SetMapTileColor(currentTile);
                    }
                }
                Console.WriteLine();
            }

            player.PlayerPosition();

            Console.SetCursorPosition(exit.ExitX, exit.ExitY);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("X");
            Console.ResetColor();

            Console.SetCursorPosition(0, 0);
        }

        private void SetMapTileColor(char tile)
        {
            // Set color for different map tiles
            switch (tile)
            {
                case '#':
                    Console.ForegroundColor = ConsoleColor.DarkGreen; // Wall color
                    break;
                case '-':
                    Console.ForegroundColor = ConsoleColor.Gray; // Floor color
                    break;
                case '^': Console.ForegroundColor = ConsoleColor.DarkRed; break;

                default:
                    Console.ResetColor(); // Reset color for other tiles
                    break;
            }
            Console.Write(tile);
            Console.ResetColor(); // Reset color after drawing the tile
        }
        public void UpdateMapTile(int y, int x, char tile)
        {
            mapLayout[y, x] = tile;
        }
    }
}
