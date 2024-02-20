using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class HUD
    {
        Map map;
        public void DisplayLegend()
        {
            Console.SetCursorPosition(0, map.maxY + 4);
            Console.WriteLine("| Map Legend");
            Console.SetCursorPosition(0, map.maxY + 5);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("| Player = + ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" Enemy = B ");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("| Walls = # |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("| Floor = - |");
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(" SpikeTrap = ^ ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(" Exit = X |");
            Console.ResetColor();
            Console.WriteLine();
        }

        public void DisplayHUD(Player player, Map map)
        {

            Console.SetCursorPosition(0, map.maxY + 1);
            Console.WriteLine($"|| Health: {player.currentHP}/{player.maxHP}||");
            DisplayLegend();
        }
    }
}
