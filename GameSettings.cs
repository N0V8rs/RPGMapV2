using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPGMap
{
    public class GameSettings
    {
        public static int PlayerInitialHealth { get; set; } = 100; // player has 100 HP
        public static int PlayerDamage { get; set; } = 10; // player deals 10 damage

        public static int EnemyInitialHealth { get; set; } = 50; // enemy has 50 HP
        public static int EnemyDamage { get; set; } = 5; // enemy deals 5 damage

        public static int BossInitialHealth { get; set; } = 200; // boss has 200 HP
        public static int BossDamage { get; set; } = 20; // boss deals 20 damage

        public static int bombDamage { get; set; } = 20; // bomb deals 20 damage
        public static int bombInitialHealth { get; set; } = 1; // bomb has 1 HP

        public static int PowerUp { get; set; } = 10; // PowerUp adds 10 to the player's damage
        public static int Diamond { get; set; } = 1; // Diamond adds 1 to the player's score

        public static int HealthPack { get; set; } = 50; // HealthPack heals 50 HP
    }
}
