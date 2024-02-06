using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Game
    {
        private Map map;
        private Enemy enemy;
        private Player player;
        private Exit exit;
        private List<Enemy> enemies;
        public List<Spike> spikes;

        public Game()
        {
            map = new Map("TextFile1.txt");
            player = new Player(10, 2, 1, 13);
            exit = new Exit(35, 8);
            enemies = new List<Enemy>
            {
                // Spawns an Enemey
                new Enemy(3,1,2,5),
                new Enemy(5,2,15,12),
                new Enemy(3,1,27,3)
            };
            spikes = new List<Spike> // Adds spikes to the game using x and y pos
            {
                 new Spike(26, 7),
                 new Spike(14, 9),
                 new Spike(28, 7),
                 new Spike(15, 9),
                 new Spike(29, 7),
                 new Spike(14, 4),
                 new Spike(27, 7),
                 new Spike(15, 4),
                 new Spike(13, 4),
            };
        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, map.maxY + 4);
            Console.WriteLine("| Map Legend");
            Console.SetCursorPosition(0, map.maxY + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| Player = + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Enemy = B ");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("| Walls = # |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("| Floor = - |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" SpikeTrap = ^ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" Exit = X |");
            Console.ResetColor();
            Console.WriteLine();
        }
        public void DisplayHUD(Player player, List<Enemy> enemies, Map map)
        {

            Console.SetCursorPosition(0, map.maxY + 1);
            Console.WriteLine($"|| Health: {player.currentHP}/{player.maxHP}||");

            foreach (var enemy in enemies)
            {
                Console.Write($"|| Enemy Health: {enemy.currentHP}/{enemy.MaxHP} ||");
            }
            DisplayLegend();
        }

        public void Start()
        {
            Console.WriteLine("Now playing Noah Vaters RPG Map");
            Console.WriteLine("\n");
            Console.WriteLine("Watch out for the enemies in the map trying to kill you");
            Console.WriteLine("\n");
            Console.WriteLine("To attack back press space near them");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(true);
            Console.Clear();

            while (!player.GameOver && !player.YouWin)
            {
                // Clear the console at the beginning of each frame
                Console.Clear();

                // Draw the map, HUD, and other elements
                map.DrawMap(player, enemies, spikes, exit);
                DisplayHUD(player, enemies, map);

                // Handle player input
                player.PlayerInput(map, enemies, exit);

                if (exit.IsPlayerOnExit(player))
                {
                    player.YouWin = true;
                }

                if (player.GameOver)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    // Display other game over messages or options...
                    Console.ReadKey(true);
                }

                // Update and draw enemies
                foreach (var enemy in enemies.ToList())
                {
                    enemy.Move(map, player);
                    enemy.Attack(player);
                    //player.CheckForSpikes(spikes);
                    //player.Draw(spikes);

                    if (!enemy.enemyAlive)
                    {
                        enemies.Remove(enemy);
                        //map.mapLayout(enemy.PosX, enemy.PosY, '-');
                    }
                    else
                    {
                        enemy.EnemyPosition();
                    }
                }
                player.CheckForSpikes(spikes);
                player.Draw(spikes);

                // Check if the player is still alive
                if (player.currentHP > 0)
                {
                    player.PlayerPosition();
                }
            }

            Console.Clear();

            if (player.YouWin)
            {
                Console.WriteLine("YOU WIN!");
                Console.WriteLine("Let's goooooooo, woooohhhhh");
            }
            else
            {
                Console.WriteLine("You Died");
                Console.WriteLine("Try better");
            }

            Console.ReadKey(true);
        }
    }
}
