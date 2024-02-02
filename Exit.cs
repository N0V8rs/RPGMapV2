using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    class Exit
    {
        public int ExitX;
        public int ExitY;

        Player player;

        public Exit(int exitX, int exitY)
        {
            ExitX = exitX;
            ExitY = exitY;
        }

        public bool IsPlayerOnExit(Player player)
        {
            return player.posX == ExitX && player.posY == ExitY;
        }

    }
}
