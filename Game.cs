using FirstPlayable;
using RPGMap;
using System;

internal class GameManager
{
    private Map map;
    private Player player;
    private Enemy enemy;
    private Enemy boss;
    private Enemy bomb;
    private HUD HUD;

    public GameManager()
    {
        map = new Map("NorthWoods.txt");
        player = new Player(10, 10, 1, map.initialPlayerPositionX, map.initialPlayerPositionY);
        enemy = new Enemy(5, 1, 8, 8, true);
        bomb = new Enemy(2,1, map.initialEnemyPositionX, map.initialEnemyPositionY);
        boss = new Enemy(10, 1, map.initialEnemyPositionX, map.initialEnemyPositionY);
        HUD = new HUD(map.mapHeight);
    }

    // Start up
    public void Start()
    {
        Console.WriteLine("Entering NorthWoods");
        Console.WriteLine("-------------------------------");
        Console.WriteLine("\nYou as a Hunter must find Diamonds and kill anything in your way.");
        Console.WriteLine("\nCollect all the diamonds or else");
        Console.WriteLine("--------------------------------");
        Console.WriteLine("You can attack by either running into the enemy.");
        Console.WriteLine("enemy have different attacks so watch out.");
        Console.WriteLine("E enemies attacks randomly when you next to them.");
        Console.WriteLine("0 is a bomb ticks down when near the player until it explosions.");
        Console.WriteLine("B is a massive enemy only can move or attack that takes a while to damage the Player.");
        Console.WriteLine("");
        Console.WriteLine("Good luck on your advanture Hunter");
        Console.WriteLine("");
        Console.WriteLine("Press any key to start...");
        Console.ReadKey(true);
        Console.Clear();

        while (!player.gameOver && !player.youWin)
        {
            map.DrawMap(player, enemy, boss, bomb);
            HUD.DisplayHUD(player, enemy, boss, bomb);
            HUD.DisplayLegend();
            PlayerInput();

            EnemyAction();
            bomb.AttackEveryTwoMoves(player);
            boss.MoveTowardsPlayer(player, map.layout);
        }

        Console.Clear();

        // player wins
        if (player.youWin)
        {
            Console.WriteLine("You win!");
            Console.WriteLine($"\nYou collected: {player.currentDiamond} Diamond!");
            Console.WriteLine("There are more diamonds to find in the world");
            Console.ReadKey(true);
        }
        // players dead
        else
        {
            Console.WriteLine("You died...");
            Console.WriteLine("Try Again!");
            Console.ReadKey(true);
        }
    }

    private void PlayerInput()
    {
        player.PlayerInput(map, enemy, boss, bomb);
        foreach (var pickup in map.powerPickups)
        {
            if (pickup.X == player.positionX && pickup.Y == player.positionY && !pickup.IsCollected)
            {
                player.IncreaseDamage();
                if (pickup.IsCollected)
                {
                    map.layout[player.positionX, player.positionY] = '-';
                }
            }
        }
        foreach (var pickup in map.healthPickups)
        {
            if (pickup.X == player.positionX && pickup.Y == player.positionY && !pickup.IsCollected)
            {
                pickup.IsCollected = true;
                player.HealFullHealth();
            }
        }
    }

    private void EnemyAction()
    {
        Random random = new Random();
        int action = random.Next(0, 2);

        if (action == 0)
        {
            enemy.EnemyMovement(player.positionX, player.positionY, map.mapWidth, map.mapHeight, map.layout);
        }
        else
        {
            enemy.AttackPlayer(player);
        }
    }
}