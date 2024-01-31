using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class Player
    {
        public int Health;
        public int Shield;
        public int positionX;
        public int positionY;

        public void Heal(int hp)
        {
            Health = hp;
            Shield = hp;

            if (hp  < 0)
            {
                hp = 0;
            }
        }

        public void Attack(int Damage)
        {
            if (Health > 0)
            {
                Health -= Damage;
            }
        }
    }
}
