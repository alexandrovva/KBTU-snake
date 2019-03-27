using System;
using System.IO; //для чтения файла
using System.Collections.Generic; //для List<>()

namespace SnakeGame
{
    [Serializable]
    public class Wall:GameObject
    {
        enum GameLevel
        {
            FIRST,
            SECOND,
            THIRD
        }
        GameLevel gameLevel = GameLevel.FIRST;
        public Wall() { }
        public Wall(char sign, ConsoleColor color):base(0, 0, sign, color)
        {
            body = new List<Point>();
        }

        public void LoadLevel()
        {
            body = new List<Point>();
            string fileName = "Level1.txt";
            if (gameLevel == GameLevel.SECOND)
                fileName = "Level2.txt";
            if (gameLevel == GameLevel.THIRD)
                fileName = "Level3.txt";

            StreamReader sr = new StreamReader(fileName);
            string[] rows = sr.ReadToEnd().Split('\n');
            for (int i=0; i < rows.Length; i++)
                for (int j = 0; j < rows[i].Length; j++) 
                    if (rows[i][j] == '*')
                        body.Add(new Point(j, i));
        }

        public void NextLevel()
        {
            if (gameLevel == GameLevel.FIRST)
            {
                gameLevel = GameLevel.SECOND;
                LoadLevel();
            }
            else if (gameLevel == GameLevel.SECOND)
            {
                gameLevel = GameLevel.THIRD;
                LoadLevel();
            }

        }
    }
}
