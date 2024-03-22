using FirstPlayable;

namespace RPGMap
{
    internal class Program : GameManager
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();
            game.Start();
        }
    }
}