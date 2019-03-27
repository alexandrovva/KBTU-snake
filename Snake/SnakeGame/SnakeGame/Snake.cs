using System;

namespace SnakeGame
{
    [Serializable]
    public class Snake:GameObject
    {
        enum Direction
        {
            NONE,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }
        Direction direction = Direction.NONE;

        public Snake() { }
        public Snake(int x, int y, char sign, ConsoleColor color): base(x,y,sign,color)
        {
        }

        public void Move()
        {
            if (direction != Direction.UP && direction != Direction.DOWN && direction != Direction.LEFT && direction != Direction.RIGHT)
                return;
            if (direction == Direction.NONE)
                return;

            for (int i = body.Count - 1; i > 0; i--)
            {
                body[i].x = body[i - 1].x; //передвигаем все эдементы body с конца, чтобы голова перемещалась отдельно
                body[i].y = body[i - 1].y;
            }
            if (direction == Direction.UP)
                body[0].y--;
            if (direction == Direction.DOWN)
                body[0].y++;
            if (direction == Direction.LEFT)
                body[0].x--;
            if (direction == Direction.RIGHT)
                body[0].x++;
        }

        public void ChangeDirection(ConsoleKeyInfo KeyInfo)
        {
            if (KeyInfo.Key == ConsoleKey.UpArrow)
              direction = Direction.UP;
            if (KeyInfo.Key == ConsoleKey.DownArrow)
                direction = Direction.DOWN;
            if (KeyInfo.Key == ConsoleKey.LeftArrow)
                direction = Direction.LEFT;
            if (KeyInfo.Key == ConsoleKey.RightArrow)
                direction = Direction.RIGHT;
        }

    }
}
