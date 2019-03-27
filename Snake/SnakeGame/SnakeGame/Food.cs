using System;
namespace SnakeGame
{
    [Serializable]
    public class Food:GameObject
    {
        public Food() { }
        public Food(int x, int y, char sign, ConsoleColor color): base(x, y, sign, color)
        {

        }
        public void Generate()
        {
            Random random = new Random();
            int x = random.Next(1, Console.WindowWidth);
            int y = random.Next(1, Console.WindowHeight);
            body[0].x = x;
            body[0].y = y;
        }
    }

}
