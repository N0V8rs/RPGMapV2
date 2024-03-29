﻿using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public abstract class EnemyManager
    {
        // variables | encapsulation

        public HPManager healthSystem;
        public int enemyDamage { get; set; }
        public int positionX { get; set; }
        public int positionY { get; set; }
        public bool enemyAlive { get; set; }

        public char currentTile;

        public char icon { get; set; }
        public string Name { get; set; }
        public abstract void DrawMovement(int playerX, int playerY, int mapWidth, int mapHeight, char[,] mapLayout, Player player, List<EnemyManager> enemies);


        public EnemyManager(int maxHealth, int damage, int startX, int startY, string name, char[,] mapLayout)
        {
            healthSystem = new HPManager(maxHealth);
            enemyDamage = damage;
            positionX = startX;
            positionY = startY;
            currentTile = mapLayout[startY, startX];
            enemyAlive = true;
            Name = name;
        }


        public virtual void Draw()
        {

        }



    }
}

