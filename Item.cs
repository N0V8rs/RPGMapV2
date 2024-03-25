using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public abstract class Item
    {
        public abstract string Name { get; }

        public abstract void Use(Player player);
    }
}
