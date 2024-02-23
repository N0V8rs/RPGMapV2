using FirstPlayable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class ItemPickup
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsCollected { get; set; }
        Map map;


        Player player;

        public ItemPickup(int x, int y)
        {
            X = x;
            Y = y;
            IsCollected = false;
        }

        public void UpdatePickupState(int x, int y)
        {
            // Update the layout to replace the collected pickup with a dash
            map.layout[y, x] = '-';
        }

        public void Collect()
        {
            IsCollected = true;
        }

        public void Draw()
        {
            if (!IsCollected)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(X, Y);
                Console.Write("@");
                Console.ResetColor();
            }
        }

        public void DrawPower()
        {
            if (!IsCollected)
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("P");
                Console.ResetColor();
            }
        }

        public void DrawHealth()
        {
            if (!IsCollected)
            {
                Console.SetCursorPosition(X, Y);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("H");
                Console.ResetColor();
            }
        }

        public void ItemPowerUp()
        {
            if (IsCollected)
            {
                player.IncreaseDamage();
            }
        }
    }
}
