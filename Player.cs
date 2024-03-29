﻿using FirstPlayable;
using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static RPGMap.Entity;

namespace RPGMap
{
    public class Player
    {
        // variables | encapsulation


        // Health System
        public HPManager healthSystem;
        public int playerDamage { get; set; }

        // Player Position
        public int positionX { get; set; }
        public int positionY { get; set; }

        // Seeds
        public int currentDiamonds { get; set; }

        // Game States
        public bool youWin { get; set; }
        public bool gameOver { get; set; }
        public bool levelComplete { get; set; }

        private char currentTile;

        // Settings
        public Settings settings = new Settings();

        // Enemy 
        public EnemyManager currentEnemy { get; set; }

        // Log list
        private List<string> liveLog;

        // Item Manager
        public ItemManager itemManager;

        public GameManager gameManager;

        public Map map;

        public List<EnemyManager> enemies;

        public Player(int maxHealth, int health, int damage, int startX, int startY, char[,] mapLayout, GameManager gameManager)
        {
            healthSystem = new HPManager(maxHealth);
            healthSystem.Heal(health);
            playerDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            itemManager = new ItemManager(this);
            this.gameManager = gameManager;
            liveLog = new List<string>();
        }


        // Receives player input
        public void PlayerInput(Map map, List<EnemyManager> enemies)
        {
            ConsoleKeyInfo playerController;
            bool moved = false;

            int movementX = positionX;
            int movementY = positionY;

            int newPlayerPositionX = positionX;
            int newPlayerPositionY = positionY;

            moved = false;

            playerController = Console.ReadKey(true);



            // moves up
            if (playerController.Key == ConsoleKey.UpArrow || playerController.Key == ConsoleKey.W)
            {
                movementY = Math.Max(positionY - 1, 0);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves down
            if (playerController.Key == ConsoleKey.DownArrow || playerController.Key == ConsoleKey.S)
            {
                movementY = Math.Min(positionY + 1, map.mapHeight - 1);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves left
            if (playerController.Key == ConsoleKey.LeftArrow || playerController.Key == ConsoleKey.A)
            {
                movementX = Math.Max(positionX - 1, 0);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }

            // moves right
            if (playerController.Key == ConsoleKey.RightArrow || playerController.Key == ConsoleKey.D)
            {
                movementX = Math.Min(positionX + 1, map.mapWidth - 1);
                HandleMovement(map, enemies, ref moved, ref newPlayerPositionX, ref newPlayerPositionY, movementX, movementY);
            }



            // exit game
            if (playerController.Key == ConsoleKey.Escape)
            {
                Environment.Exit(1);
            }
        }

        // handles things like collision checks and what the player is moving towards
        private void HandleMovement(Map map, List<EnemyManager> enemies, ref bool moved, ref int newPlayerPositionX, ref int newPlayerPositionY, int movementX, int movementY)
        {
            if (moved == false && map.layout[movementY, movementX] != '#')
            {

                foreach (var enemy in enemies)
                {
                    if (movementY == enemy.positionY && movementX == enemy.positionX)
                    {
                        currentEnemy = enemy;
                        enemy.healthSystem.Damage(playerDamage);
                        UpdateLiveLog($"Dealt {playerDamage} damage to {enemy.Name}");
                        if (healthSystem.IsDead())
                        {
                            gameOver = true;
                        }



                        if (enemy.healthSystem.IsDead())
                        {
                            enemy.enemyAlive = false;
                            Console.SetCursorPosition(enemy.positionX, enemy.positionY);
                            Console.BackgroundColor = ConsoleColor.DarkGray;
                            //Console.ForegroundColor = ConsoleColor.Gray;
                            Console.Write('-');

                            enemy.positionX = 0;
                            enemy.positionY = 0;

                            enemy.enemyAlive = false;
                            UpdateLiveLog($"You Killed The {enemy.Name}");

                        }
                        else if (enemy is BossEnemy) // Check if the enemy is a Boss
                        {

                            healthSystem.Damage(enemy.enemyDamage);
                            UpdateLiveLog($"Boss dealt {enemy.enemyDamage} damage to you!");
                            if (healthSystem.IsDead())
                            {
                                gameOver = true;
                            }
                        }

                        return;
                    }
                }

                // Spikes
                if (map.layout[movementY, movementX] == '^')
                {
                    //currentTile = '^';
                    healthSystem.Damage(1);
                    UpdateLiveLog("-1 Health");
                    if (healthSystem.IsDead())
                    {
                        gameOver = true;
                    }
                    else
                    {
                        // Draw the current tile at the player's previous position
                        Console.SetCursorPosition(positionX, positionY);
                        Console.ResetColor();
                        Console.Write(currentTile);
                        Console.ResetColor();

                        // Update the current tile
                        currentTile = map.layout[movementY, movementX];

                        // Move the player
                        positionY = movementY;
                        positionX = movementX;
                        moved = true;
                    }
                    return;
                }

                if (map.layout[movementY, movementX] == 'X')
                {
                    youWin = true;
                    gameOver = true;
                }

                // collectable Diamonds
                if (map.layout[movementY, movementX] == '@')
                {
                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    itemManager.PickupItem("Diamonds");

                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];

                    // move the player
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;
                }

                if (map.layout[movementY, movementX] == 'H')
                {

                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    itemManager.PickupItem("HealthPotion");

                    Console.SetCursorPosition(positionX, positionY);

                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;
                }

                if (map.layout[movementY, movementX] == 'P')
                {

                    map.layout[movementY, movementX] = '-';
                    Console.ForegroundColor = ConsoleColor.Gray;
                    itemManager.PickupItem("DamageBoost");
                    Console.SetCursorPosition(positionX, positionY);

                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);
                    currentTile = map.layout[movementY, movementX];
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;

                    return;
                }

                if (map.layout[movementY, movementX] == 'E')
                {

                    movementY = positionY;
                    movementX = positionX;
                    return;
                }

                else
                {
                    Console.SetCursorPosition(positionX, positionY);
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Write(currentTile);
                    if (map.layout[movementY, movementX] == '-')
                    {
                        currentTile = map.layout[movementY, movementX];
                    }
                    positionY = movementY;
                    positionX = movementX;
                    moved = true;
                }
            }
        }

        public void Draw()
        {
            // Draw the current tile at the player's previous position
            Console.SetCursorPosition(positionX, positionY);
            Console.ResetColor();
            Console.Write(currentTile);

            // Draw the player at the new position
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Blue;
            //Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write("!");
            Console.ResetColor();
        }

        public void UpdateLiveLog(string message)
        {
            liveLog.Add(message);

        }

        public List<string> GetLiveLog()
        {
            return liveLog;
        }
    }
}