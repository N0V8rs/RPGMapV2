using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public class Settings
    {
        // Player settings
        public int PlayerInitialHealth { get; set; } = 10;
        public int PlayerInitialDamage { get; set; } = 10;
        public int PlayerInitialLevel { get; set; } = 1;
        public string MapFileName { get; set; } = "NorthWoods.txt";



        // Common settings

        public int CommonEnemyInitialHealth { get; set; } = 3;
        public int CommonEnemyInitialDamage { get; set; } = 1;



        // Boss settings
        public int BossInitialHealth { get; set; } = 12;
        public int BossInitialDamage { get; set; } = 2;


        // Sniper settings
        public int SniperInitialHealth { get; set; } = 1;
        public int SniperInitialDamage { get; set; } = 2;
    }
}