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

        public HUD(int mapHeight)
        {
            this.mapHeight = mapHeight;
        }

        public void DisplayHUD(Player player, Enemy enemy, Enemy boss, Enemy bomb)
        {
            Console.SetCursorPosition(0, mapHeight + 1);
            Console.WriteLine($"Player Health: {player.HPManager.ReturnsCurrentHP()}/{player.HPManager.ReturnsMaxHP()} | Collected Diamond: {player.currentDiamond} | Enemy Health: {enemy.HPManager.ReturnsCurrentHP()}/{enemy.HPManager.ReturnsMaxHP()}| Boss Health: {boss.HPManager.ReturnsCurrentHP()}/{boss.HPManager.ReturnsMaxHP()}| Bomb Health: {bomb.HPManager.ReturnsCurrentHP()}/{bomb.HPManager.ReturnsMaxHP()}");
        }

        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, mapHeight + 2);
            Console.WriteLine("Player = !" + "\nEnemy = E" + "\nWalls = #" + "\nFloor = -" + "\nDiamonds = @" + "\nSpikeTrap = ^  Door: X");
        }
    } 
}
