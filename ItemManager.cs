using RPGMap;
using System;
using System.Collections.Generic;

namespace RPGMap
{
    internal class ItemManager
    {
        private Player player;

        public ItemManager(Player player)
        {
            this.player = player;
        }

        public void PickupItem(string item)
        {
            switch (item)
            {
                case "HealthPotion":
                    player.healthSystem.Heal(1);
                    player.UpdateLiveLog("Gained +1 health");
                    break;

                case "DamageBoost":
                    player.playerDamage += 1;
                    player.UpdateLiveLog("Player Damage increased +1");
                    break;

                case "Diamonds":
                    player.currentDiamonds += 1;
                    player.UpdateLiveLog("Picked up a Diamond");
                    break;
            }
        }
    }
}