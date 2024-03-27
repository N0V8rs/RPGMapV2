using RPGMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstPlayable
{
    internal class HUD
    {
        private Player player;
        private Map map;

        public HUD(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        public void UpdateHUD()
        {
            Console.SetCursorPosition(0, map.mapHeight + 1);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, map.mapHeight + 1); 
            Console.WriteLine($"Player HP: {player.healthSystem.GetCurrentHealth()} | Player Damage: {player.playerDamage} | Diamonds: {player.currentDiamonds}");

            string currentEnemyInfo = player.currentEnemy != null ? $"{player.currentEnemy.Name} | HP: {player.currentEnemy.healthSystem.GetCurrentHealth()}" : "None";
            Console.SetCursorPosition(0, map.mapHeight + 2);
            Console.Write(new string(' ', Console.WindowWidth)); 
            Console.SetCursorPosition(0, map.mapHeight + 2); 
            Console.WriteLine($"Enemy: {currentEnemyInfo}");

            RedrawLiveLog();
        }

        public void UpdateLegend()
        {
            //Console.SetCursorPosition(0, map.mapHeight + 2);
            //Console.WriteLine($"\nPlayer Damage: {player.playerDamage}");
        }

        public void DisplayLiveLog(List<string> liveLog)
        {
            Console.SetCursorPosition(0, map.mapHeight + 5);
            Console.WriteLine("Live Log:");
            for (int i = liveLog.Count - 1; i >= 0 && i >= liveLog.Count - 2; i--)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.WriteLine(liveLog[i]);
            }
            Console.ResetColor();
        }

        public void RedrawLiveLog()
        {
            int startLine = map.mapHeight + 5;
            List<string> liveLog = player.GetLiveLog();
            for (int i = 2; i >= 0; i--)
            {
                Console.SetCursorPosition(0, startLine + i);
                Console.Write(new string(' ', Console.WindowWidth));
                if (i < liveLog.Count)
                {
                    Console.SetCursorPosition(0, startLine + i);
                    Console.WriteLine(liveLog[liveLog.Count - 1 - i]);
                }
            }
        }
    }
}