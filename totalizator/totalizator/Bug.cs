using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace totalizator
{
    //участник
    public class Bug
    {
        public string name;
        public int number;
        public Image image;
        public int shag1,shag2;
        public Point position;


        //имя таракана
        public string Name
        {
            get { return name; }
            private set { name = value; }
        }

        //номер таракана
        public int Number
        {
            get { return number + 1; }
            private set { number = value; }
        }


        // Изображение таракана
        public Image Picture
        {
            get { return image; }
            private set { image = value; }
        }

        //положение таракана на дорожке
        public Point Position
        {
            get { return position; }
            set { position = value; }
        }

        //конструктор таракана
        public Bug(string name, int numb, Image img,Point curPos)
        {
            Name = name;
            Number = numb;
            Picture = img;
            Position = curPos;
        }



        //движение фишки
        public void Move(Random n)
        {
            int result = 0;
            shag1 = n.Next(1, 6);
            shag2 = n.Next(1, 6);

            result = shag1 + shag2;

            Position = new Point(Position.X + result, Position.Y);
        }

        //отрисовка таракашки на форме
        public void DrawRoach(Graphics g, Size s)
        {
            g.DrawImage(Picture, Position.X, Position.Y, s.Width, s.Height);
        }
        
    }
}
