using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Enemy1
    {
        public int enemyPosX; // Position X
        public int enemyPosY; // Position Y
        public int maxEnemyHP; // Health can't go pass
        public int enemyHP; // Health of the enemy
        public bool enemyAlive; // Check to see if the enemy is alive
        public int enemyHealth;

        public void EnemyMovement()
        {
            Enemy1 enemy1 = new Enemy1();

            int enemyMovementX = enemyPosX;
            int enemyMovementY = enemyPosY;

            // random roll to move
            Random randomRoll = new Random();

            //  1 of 4 options to move
            int rollResult = randomRoll.Next(1, 5);

            int newEnemyPositionX = enemyMovementX;
            int newEnemyPositionY = enemyMovementY;

            // Retry movement if the position is the same as the player or the new position
            while ((enemyMovementX == playerPosX && enemyMovementY == playerPosY) || (enemyMovementX == newEnemyPositionX && enemyMovementY == newEnemyPositionY))
            {
                rollResult = randomRoll.Next(1, 5);
                if (rollResult == 1)
                {
                    enemyMovementY = Math.Min(enemyPosY + 1, maxY);
                }
                else if (rollResult == 2)
                {
                    enemyMovementY = Math.Max(enemyPosY - 1, 0);
                }
                else if (rollResult == 3)
                {
                    enemyMovementX = Math.Max(enemyPosX - 1, 0);
                }
                else // The 4 move the enemy can move
                {
                    enemyMovementX = Math.Min(enemyPosX + 1, maxX);
                }
            }

            // Check for collisions and update the enemy position
            if (mapLayout[enemyMovementY, enemyMovementX] != '#' && mapLayout[enemyMovementY, enemyMovementX] != '^' && mapLayout[enemyMovementY, enemyMovementX] != 'X')
            {
                // Reset the old position
                mapLayout[newEnemyPositionY, newEnemyPositionX] = '-';
                // Check if the enemy is still alive before updating the new position
                if (enemyAlive)
                {
                    mapLayout[enemyMovementY, enemyMovementX] = '-'; // Set the enemy symbol
                    enemyPosX = enemyMovementX;
                    enemyPosY = enemyMovementY;
                    // Update the new position
                    newEnemyPositionX = enemyPosX;
                    newEnemyPositionY = enemyPosY;
                }
            }

            // Checks to see if the enemy is in the attack position
            if (Math.Abs(enemyPosX - playerPosX) <= 1 && Math.Abs(enemyPosY - playerPosY) <= 1)
            {
                playerHP -= 1;
                if (playerHP <= 0)
                {
                    gameOver = true;
                }
            }
        }
    }
}
