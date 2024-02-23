using FirstPlayable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class HUD
    {
        private int mapHeight;
        private int timeBeforeNextAttack;

        public HUD(int mapHeight)
        {
            this.mapHeight = mapHeight;
            this.timeBeforeNextAttack = 0;
        }
        public void UpdateTimeBeforeNextAttack(int time)
        {
            timeBeforeNextAttack = time;
        }

        public void DisplayHUD(Player player, Enemy enemy, Enemy boss, Enemy bomb)
        {
            Console.SetCursorPosition(0, mapHeight + 1);
            Console.WriteLine($"Player HP: {player.HPManager.ReturnsCurrentHP()}/{player.HPManager.ReturnsMaxHP()} | Player Damage: {player.playerDamage} | Collected Diamond: {player.currentDiamond}");

            if (enemy.enemyAlive)
            {
                Console.WriteLine($"Enemy HP: {enemy.HPManager.ReturnsCurrentHP()}/{enemy.HPManager.ReturnsMaxHP()}");
            }

            if (bomb.enemyAlive)
            {
                Console.WriteLine($"Sniper HP: {bomb.HPManager.ReturnsCurrentHP()}/{bomb.HPManager.ReturnsMaxHP()} Time before the attack: {timeBeforeNextAttack}|2");
            }
            
            if (boss.enemyAlive)
            {
                Console.WriteLine($"Boss HP: {boss.HPManager.ReturnsCurrentHP()}/{boss.HPManager.ReturnsMaxHP()}");
            }

        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, mapHeight + 6);
            Console.Write("Player = ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("!");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("EnemyRandom = ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("E ");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write("Sniper Enemy = ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("W ");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write("Boss = ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("B");
            Console.ResetColor();
            Console.WriteLine();

            Console.Write("Walls = ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("# ");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write("Floor = ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("- ");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write("Exit = ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("X ");
            Console.ResetColor();
            Console.Write("| ");
            Console.Write("Spikes = ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("^ ");
            Console.ResetColor();
            Console.Write("dealing one damage to the Player\n");

            Console.Write("Power UPs: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("P ");
            Console.ResetColor();
            Console.Write("= Increase Player Damage | ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("H ");
            Console.ResetColor();
            Console.WriteLine("= Full Health");
        }

    }
}
