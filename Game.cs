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
        private HUD hud;
        private Enemy enemy;
        private Player player;
        private Exit exit;
        private List<Enemy> enemies;
        public int posX {  get; set; }
        public int posY { get; set; }

        public Game()
        {
            map = new Map("NorthWoods.txt");
            player = new Player(10, 10, 2, player.posX, player.posY);
            exit = new Exit(35, 8);
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
                Console.Clear();
                map.DrawMap(player,enemy);
                //hud.DisplayHUD();

                // Handle player input
                player.PlayerInput(map, exit);

                if (exit.IsPlayerOnExit(player))
                {
                    player.YouWin = true;
                }

                if (player.GameOver)
                {
                    Console.Clear();
                    Console.WriteLine("Game Over!");
                    Console.ReadKey(true);
                }

                if (player.currentHP > 0)
                {

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
