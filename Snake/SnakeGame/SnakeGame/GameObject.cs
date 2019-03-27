using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace SnakeGame
{
    public class GameObject
    {
        public List<Point> body; //элементы тела змейки
        public char sign; //отображение элементов каким-то знаком
        public ConsoleColor color;

        public GameObject() { }
        public GameObject(int x,int y,char sign,ConsoleColor color)
        {
            body = new List<Point>
            {
                new Point(x, y)
            };
            this.sign = sign;
            this.color = color;
        }

        public void Draw() //пробегается по body и рисует все элементы 
        {
            Console.ForegroundColor = color;
            foreach (Point p in body)
            {
                Console.SetCursorPosition(p.x, p.y);
                Console.Write(sign);
            }
        }

        public bool CollisionWith(GameObject obj)
        {
            foreach (Point p in obj.body)
            {
                if (body[0].x == p.x && body[0].y == p.y) //так как мы находимся в классе Snake, body ссылается на тело змейки в листе
                    return true;
            }
            return false;
        }
        public bool CollisionWithBody()
        {
            for (int i = 1; i < body.Count; i++)
            {
                if (body[0].x == body[i].x && body[0].y == body[i].y) return true;
            }
            return false;
        }
        public bool CollisionWithWindow()
        {
                if (body[0].y == Console.WindowTop || body[0].x == Console.WindowLeft || body[0].x == Console.WindowWidth || body[0].y == Console.WindowHeight) return true;
            return false;
        }

        public void Save(string fileName)
        {
            if (File.Exists(fileName) == true)
            {
                File.Delete(fileName);
            }

            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            XmlSerializer save = new XmlSerializer(this.GetType());
            save.Serialize(fs, this);
            fs.Close();
        }
        public GameObject Load(string fileName)
        {
            GameObject obj = null;
            XmlSerializer load = new XmlSerializer(this.GetType());
            FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            obj = load.Deserialize(fs) as GameObject;
            fs.Close();
            return obj;
        }
    }
}
