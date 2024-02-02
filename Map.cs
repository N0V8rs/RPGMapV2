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

                    Console.ForegroundColor = GetTileColor(currentTile);

                    if (currentTile == 'B')
                    {
                        // Check if the enemy is alive before drawing
                        Enemy enemy = enemies.FirstOrDefault(e => e.posX == j && e.posY == i);
                        if (enemy != null && enemy.enemyAlive)
                        {
                            Console.Write(currentTile);
                        }
                        else
                        {
                            Console.Write('-'); // Draw an empty space for defeated enemies
                        }
                    }
                    else
                    {
                        Console.Write(currentTile);
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
        private ConsoleColor GetTileColor(char tile)
        {
            switch (tile)
            {
                case '#':
                    return ConsoleColor.Cyan; // Walls
                case '-':
                    return ConsoleColor.Gray; // Floor
                case 'B':
                    return ConsoleColor.Red; // Enemy
                case '^':
                    return ConsoleColor.DarkGray; // SpikeTrap
                case 'X':
                    return ConsoleColor.Magenta; // Exit
                default:
                    return ConsoleColor.White; // Default color
            }
        }
        public void UpdateMapTile(int y, int x, char tile)
        {
            mapLayout[y, x] = tile;
        }
    }
}
