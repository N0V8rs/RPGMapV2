using FirstPlayable;
using RPGMap;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RPGMap
{

    public class GameManager
    {

        private Stopwatch levelTimer = new Stopwatch();
        private Map map;
        private Player player;

        BossEnemy boss;
        CommonEnemy enemy;
        SniperEnemy sniper;

        private Settings settings = new Settings();
        private List<EnemyManager> enemies = new List<EnemyManager>();
        private HUD hud;

        public string currentLevel { get; set; }


        public void Init()
        {

            // Initialization
            if (currentLevel == null)
            {
                currentLevel = settings.MapFileName;
            }
            map = new Map(currentLevel, enemies);
            player = new Player(settings.PlayerInitialHealth, settings.PlayerInitialDamage, settings.PlayerInitialLevel, map.initialPlayerPositionX, map.initialPlayerPositionY, map.layout, this);
            hud = new HUD(player, map);



        }

        public void Update()
        {
            map.UpdateMap(player, sniper, boss, enemy);
            hud.UpdateLegend();
            hud.UpdateHUD();
        }


        public void Draw()
        {
            Display();
            foreach (var enemy in enemies)
            {
                enemy.DrawMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout, player, enemies);
            }
        }

        // Start up
        public void Start()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Entering NorthWoods");
            Console.WriteLine("-------------------------------");
            Console.WriteLine("\nYou as a Hunter must find Diamonds and kill anything in your way.");
            Console.WriteLine("\nCollect all the diamonds or else");
            Console.WriteLine("--------------------------------");
            Console.WriteLine("You can attack by either running into the enemy.");
            Console.WriteLine("enemy have different attacks so watch out.");
            Console.WriteLine("E enemies attacks randomly when you next to them.");
            Console.WriteLine("W is a sniper that deals damage you with infinite range.");
            Console.WriteLine("B is a massive enemy only can move or attack that takes a while to damage the Player.");
            Console.WriteLine("");
            Console.WriteLine("Good luck on your advanture Hunter");
            Console.WriteLine("");
            Console.WriteLine("Press any key to start...");
            Console.ReadKey(true);
            Console.Clear();
            Console.ResetColor();

            // game loop uses Init/Update/Draw methods

            Init();
            while (!player.gameOver)
            {
                Console.CursorVisible = false;
                StartLevel();

                Update();

                Draw();
            }

            Console.Clear();

            // player wins
            if (player.youWin)
            {
                Console.WriteLine("You win!");
                Console.WriteLine($"\nYou collected: {player.currentDiamonds} Diamond!");
                Console.WriteLine("There are more diamonds to find in the world");
                Console.ReadKey(true);
            }
            // players dead
            else
            {
                Console.WriteLine("You died...");
                Console.WriteLine("Try Again!");
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ReadKey(true);
            }
        }


        private void StartLevel()
        {
            levelTimer.Start(); // Timer will start at beginning of level
        }

        private void EndLevel()
        {
            levelTimer.Stop(); // Timer stops at the end
            TimeSpan elapsedTime = levelTimer.Elapsed;


            string elapsedTimeString = String.Format("{0:00}:{1:00}:{2:00}", elapsedTime.Hours, elapsedTime.Minutes, elapsedTime.Seconds);
            Console.WriteLine($"Level completed in: {elapsedTimeString}");
        }


        private void Display()
        {
            player.PlayerInput(map, enemies);

        }




    }

}