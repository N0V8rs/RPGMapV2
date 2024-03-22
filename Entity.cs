using RPGMap;

public class Entity
{
    public HPManager HPManager { get; set; }
    public int PositionX { get; set; }
    public int PositionY { get; set; }
    public bool IsAlive { get; set; }
    public int Damage { get; set; }

    public Entity(int maxHealth, int startX, int startY, int damage)
    {
        HPManager = new HPManager(maxHealth);
        PositionX = startX;
        PositionY = startY;
        Damage = damage;
        IsAlive = true;
    }
}
