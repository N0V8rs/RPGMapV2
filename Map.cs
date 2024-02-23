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
        private string path;
        private string[] floor;
        public List<ItemPickup> diamonds;
        public List<ItemPickup> powerPickups;
        public List<ItemPickup> healthPickups;

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
                        initialEnemyPositionX = j;
                        initialEnemyPositionY = i;
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


        public void DrawMap(Player player, Enemy enemy, Enemy boss, Enemy bomb)
        {
            Console.Clear();

            for (int k = 0; k < mapHeight; k++)
            {
                for (int l = 0; l < mapWidth; l++)
                {
                    char tile = layout[k, l];

                    if (tile == '=' && !player.levelComplete)
                    {
                        player.positionX = l;
                        player.positionY = k - 1;
                        player.levelComplete = true;
                        layout[k, l] = '#';
                    }
                    else if (tile == 'E' && !player.levelComplete)
                    {
                        enemy.positionX = l;
                        enemy.positionY = k;
                        layout[k, l] = '-';
                    }
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
                    bool pickupDrawn = false;
                   //foreach (var pickup in damagePickups)
                   //{
                   //    if (pickup.X == l && pickup.Y == k && !pickup.IsCollected)
                   //    {
                   //        pickup.DrawPower();
                   //        pickupDrawn = true;
                   //        break;
                   //    }
                   //}
                   if (!pickupDrawn)
                   {
                       Console.Write(tile);
                   }

                    else if (tile == '@' && !player.levelComplete)
                    {
                        bool diamondDrawn = false;
                        foreach (var diamond in diamonds)
                        {
                            if (diamond.X == l && diamond.Y == k)
                            {
                                diamond.Draw();
                                diamondDrawn = true;
                                break;
                            }
                        }
                        if (!diamondDrawn)
                        {
                            Console.Write('-');
                        }
                    }
                    else
                    {
                        Console.Write(tile);
                    }
                }
                Console.WriteLine();
            }
            player.DrawPlayer();
            enemy.DrawEnemy();
            boss.DrawBoss();
            bomb.DrawBomb();

            Console.SetCursorPosition(0, 0);
        }
    }
}

