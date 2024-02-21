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
        public int maxX {  get; set; }
        public int maxY {  get; set; }
        public int playerPosX { get; set; }
        public int playerPosY { get; set; }
        public int enemy1PosX { get; set; }
        public int enemy1PosY { get; set; }


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

                    if (mapLayout[i, j] == '+')
                    {
                        playerPosX = j;
                        playerPosY = i;
                    }
                    else if (mapLayout[i, j] == 'E')
                    {
                        enemy1PosX = j;
                        enemy1PosY = i;
                        
                    }
                }
            }
        }

        public void DrawMap(Player player, Enemy enemy)
        {
            for (int k = 0; k < maxY; k++)
            {
                for (int l = 0; l < maxX; l++)
                {
                    char currentTile = mapLayout[k, l];

                    if (currentTile == '=' && !player.YouWin)
                    {
                        player.posX = l;
                        player.posY = k - 1;
                        player.YouWin = true;
                        mapLayout[k, l] = '#';
                    }
                    else if (currentTile == 'X' && !player.YouWin)
                    {
                        enemy.posX = l;
                        enemy.posY = k;
                    }
                    Console.Write(currentTile);
                }
                Console.WriteLine();
            }
            player.DrawPlayer();
            enemy.DrawEnemy();
            Console.SetCursorPosition(0, 0);
            // hud.DisplayHUD();
        }
    }
}

