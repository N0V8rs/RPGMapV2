using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    internal class HPManager
    {
        private int maxHP;
        private int currentHP;

        public HPManager(int SetHP) 
        {
            maxHP = SetHP;
            currentHP = SetHP;
        }
        public int ReturnsCurrentHP()
        {
            return currentHP;
        }

        public int ReturnsMaxHP()
        {
            return maxHP;
        }

        // Modify Health
        public void Damage(int amount)
        {
            currentHP -= amount;
            if (currentHP < 0)
                currentHP = 0;
        }

        public void Heal(int amount)
        {
            currentHP += amount;
            if (currentHP > maxHP)
                currentHP = maxHP;
        }

        public bool IsDead()
        {
            return currentHP <= 0;
        }
    }
}
