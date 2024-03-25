using RPGMap;
using System;
using System.Collections.Generic;

namespace RPGMap
{
    public class ItemManager
    {
        private Player player;
        private Dictionary<string, Item> items;

        public ItemManager(Player player)
        {
            this.player = player;
            items = new Dictionary<string, Item>
        {
            { "HealthPotion", new HealthPotion() },
            { "DamageBoost", new DamageBoost() },
            { "Diamonds", new Diamonds() }
        };
        }

        public void PickupItem(string itemName)
        {
            if (items.TryGetValue(itemName, out Item item))
            {
                item.Use(player);
            }
        }
    }
}