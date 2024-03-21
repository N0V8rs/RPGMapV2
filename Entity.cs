using RPGMap;

public class Entity
{
    public int positionX { get; set; }
    public int positionY { get; set; }
    public HPManager HPManager { get; set; }

    public Entity(int positionX, int positionY, int initialHP, int startX, int startY)
    {
        this.positionX = positionX;
        this.positionY = positionY;
        this.HPManager = new HPManager(initialHP);
    }
}
