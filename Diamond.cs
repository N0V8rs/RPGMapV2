using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public class Diamonds : Item
    {
        public override string Name => "Diamonds";

        public override void Use(Player player)
        {
            player.currentDiamonds += 1;
            player.UpdateLiveLog("Picked up a Diamond");
        }
    }
}
