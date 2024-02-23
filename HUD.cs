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
            this.timeBeforeNextAttack = 3;
        }
        public void UpdateTimeBeforeNextAttack(int time)
        {
            timeBeforeNextAttack = time;
        }

        public void DisplayHUD(Player player, Enemy enemy, Enemy boss, Enemy bomb)
        {
            Console.SetCursorPosition(0, mapHeight + 1);
            Console.WriteLine($"Player HP: {player.HPManager.ReturnsCurrentHP()}/{player.HPManager.ReturnsMaxHP()} | Collected Diamond: {player.currentDiamond} | Enemy HP: {enemy.HPManager.ReturnsCurrentHP()}/{enemy.HPManager.ReturnsMaxHP()}| Boss HP: {boss.HPManager.ReturnsCurrentHP()}/{boss.HPManager.ReturnsMaxHP()}| Bomb HP: {bomb.HPManager.ReturnsCurrentHP()}/{bomb.HPManager.ReturnsMaxHP()} Time before the attack: {timeBeforeNextAttack}");
        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, mapHeight + 2);
            Console.WriteLine("Player = !");
            Console.WriteLine("EnemyRandom = E" + " | Sniper Enemy = W" + " | Boss = B");
            Console.WriteLine("Walls = # " + "| Floor = - " + "Exit = X");
            Console.WriteLine("Power UPs: " + "P = Increase Player Damage |" + " H = Full Health");
        }
    } 
}
