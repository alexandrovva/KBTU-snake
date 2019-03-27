using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

namespace SnakeGame
{
    class Program
    {
        public int x = 0;
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();            
            Console.SetCursorPosition(25, 10);
            Console.WriteLine("Press any key to start");
            Console.ReadKey();

            Game game = new Game();
                game.Start();

        }
    }
}
