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

        public void DrawMap(Player player, List<Enemy> enemies, List<Spike> spikes, Exit exit)
        {
            Console.Clear();

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    // Drawing logic
                    char currentTile = mapLayout[i, j];

                    if (currentTile == 'B')
                    {
                        // Check if the enemy is alive before drawing
                        Enemy enemy = enemies.FirstOrDefault(e => e.posX == j && e.posY == i);
                        if (enemy != null && enemy.enemyAlive)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write(currentTile);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Gray; // Reset color for defeated enemies
                            Console.Write('-'); // Draw an empty space for defeated enemies
                        }
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
            foreach (var enemy in enemies)
            {
                // Draw enemies only if they are alive
                if (enemy.enemyAlive)
                {
                    enemy.EnemyPosition();
                }
            }

            foreach (var spike in spikes)
            {
                spike.Draw();
            }

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
                    Console.ForegroundColor = ConsoleColor.Cyan; // Wall color
                    break;
                case '-':
                    Console.ForegroundColor = ConsoleColor.Gray; // Floor color
                    break;

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
