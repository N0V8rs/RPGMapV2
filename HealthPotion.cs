﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public class HealthPotion : Item
    {
        public override string Name => "HealthPotion";

        public override void Use(Player player)
        {
            player.healthSystem.Heal(1);
            player.UpdateLiveLog("Gained +1 health");
        }
    }
}
