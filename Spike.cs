using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public class Spike
    {
        public int posX;
        public int posY;

        public Spike(int initialPosX, int initialPosY)
        {
            posX = initialPosX;
            posY = initialPosY;
        }

        public void Draw()
        {
            Console.SetCursorPosition(posX, posY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("^");
            Console.ResetColor();
        }

        public void SetPosition(int newX, int newY)
        {
            posX = newX;
            posY = newY;
        }
    }
}
