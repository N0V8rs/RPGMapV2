using FirstPlayable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class BossEnemy : Entity
    {
        public BossEnemy(int maxHealth, int health, int damage, int startX, int startY) : base(maxHealth, health, damage, startX, startY)
        {
            positionX = 0;
            positionY = 0;
        }
    }
}
