namespace RPGMap
{
    public class HPManager
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
