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
        Player player;
        HUD hud;

        public Map(string filepath)
        {
            path = filepath;
            floorMap = File.ReadAllLines(filepath);
            maxX = floorMap[0].Length;
            maxY = floorMap.Length;
            CreateMap();
        }

        private void CreateMap()
        {
            mapLayout = new char[maxY, maxX];

            for (int i = 0; i < maxY; i++)
            {
                for (int j = 0; j < maxX; j++)
                {
                    mapLayout[i, j] = floorMap[i][j];

                    if (mapLayout[i,j] == '+')
                    {
                        player.posX = i;
                        player.posY = j;
                    }
                    if (mapLayout[i,j] == 'E')
                    {
                        enemy.posX = j;
                        enemy.posY = i;
                    }
                }
            }
        }

        public void DrawMap(Player player, Enemy enemy)
        {
            for (int l = 0; l < maxY; l++)
            {
                for (int k = 0; k < maxX; k++)
                {
                    char currentTile = mapLayout[l,k];

                    if (currentTile == '=' && !player.YouWin)
                    {
                        player.posX = l; player.posY = k;
                        player.YouWin = true;
                        mapLayout[k,l] = '#';
                    }
                    if (currentTile == '*' && !player.YouWin)
                    {
                        enemy.posX = l; enemy.posY = k;
                    }
                    Console.Write(currentTile);
                }
                Console.WriteLine();
            }
            Console.SetCursorPosition(0, 0);
            //hud.DisplayHUD();
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
                case '^': 
                    Console.ForegroundColor = ConsoleColor.DarkRed; // Spikes color
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
