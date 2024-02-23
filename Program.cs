using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstPlayable;

namespace RPGMap
{
    internal class Program : GameManager
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();
            game.Start();
        }
    }
}