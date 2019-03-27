using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using System.Threading;

namespace SnakeGame
{
    [Serializable]
    public class Game
    {
        List<GameObject> game_objects;
        public bool isAlive;
        public Snake snake;
        public Food food;
        public Wall wall;

        public int score = 0;
        public int level = 1;

        public Game()
        {
            game_objects = new List<GameObject>();
            snake = new Snake(15, 10, '*', ConsoleColor.Black);
            food = new Food(0, 0, 'o', ConsoleColor.Magenta);
            wall = new Wall('|', ConsoleColor.DarkBlue);

            wall.LoadLevel();
            if (food.CollisionWith(snake) || food.CollisionWith(wall))
                food.Generate();
            else food.Generate();
            game_objects.Add(snake);
            game_objects.Add(food);
            game_objects.Add(wall);

            isAlive = true;
        }
        public void Start()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Thread thread = new Thread(SnakeMove);
            thread.Start();

            while (isAlive && keyInfo.Key != ConsoleKey.Escape)
            {
                keyInfo = Console.ReadKey();

                 if (keyInfo.Key == ConsoleKey.S)
                 {
                    Save();
                 }

                 if (keyInfo.Key == ConsoleKey.L)
                 {
                    Load();
                 } 

                snake.ChangeDirection(keyInfo);
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(35, 10);
            Console.WriteLine("GAME OVER <3");
            Console.WriteLine("Your score is:" + score);
            Console.WriteLine("Your level:" + level);

        }

        public void SnakeMove()
        {
            while (isAlive)
            {
                snake.Move();
                if (snake.CollisionWith(food)) //если возвращается true, значит snake столкнулся с food
                {
                    snake.body.Add(new Point(0, 0));
                    score++;
                    if (food.CollisionWith(snake) || food.CollisionWith(wall))
                        food.Generate();
                    else food.Generate();

                    if (snake.body.Count % 1 == 0)
                    {
                        wall.NextLevel();
                        level++;
                    }
                }

                if (snake.CollisionWith(wall) == true || snake.CollisionWithBody() == true || snake.CollisionWithWindow())
                {
                    isAlive = false;
                }

                Draw();
                Thread.Sleep(300);
            }
        }

        public void Draw()
        {
            Console.Clear();
            foreach (GameObject g in game_objects)
                g.Draw();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Score:" + score);
            Console.WriteLine("Level:" + level);
        }

         public void Save()
        {
            snake.Save("snake.xml");
            food.Save("food.xml");
            wall.Save("wall.xml");
            
        }

        public void Load()
        {
            snake = snake.Load("snake.xml") as Snake;
            food = food.Load("food.xml") as Food;
            wall = wall.Load("wall.xml") as Wall;
            game_objects = new List<GameObject>();
            game_objects.Add(snake);
            game_objects.Add(food);
            game_objects.Add(wall);
        }
    }
}
