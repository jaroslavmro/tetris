using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace Tetris
{
    class Klocek
    {
        public Rectangle[] klocek;
        public SolidBrush brush;
        public Pen pen;
         private  int x = 50;
         private  int y = 10;
          private int width = 10;
         private  int height = 10;

        public void drawKlocek(Graphics paper)
        {
            foreach (Rectangle rec in klocek)
            {
                paper.FillRectangle(brush, rec);
                paper.DrawRectangle(pen, rec);
            }
        }
        public Klocek()
        {
            klocek = new Rectangle[4];
            var n = new Random().Next(0,7);
            Console.WriteLine(n);

            

                if (n == 0)
                {
                    Prosta();
                }
                if (n == 1)
                {
                    Kwadrat();
                } if (n == 2)
                {
                    Te();
                } if (n == 3)
                {
                    Elka();
                } if (n == 4)
                {
                    ElkaOdwr();
                } if (n == 5)
                {
                    Ha();
                } if (n == 6)
                {
                    Zet();
                }
            
        }
        public void Prosta()
        {
            
            brush = new SolidBrush(Color.Red);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < klocek.Length; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);
                y -= 10;
            }


        }
        public void Kwadrat()
        {
            
            brush = new SolidBrush(Color.Yellow);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 2; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);
                klocek[2 + i] = new Rectangle(x + 10, y, width, height);
                y -= 10;
            }
        }
        public void Te()
        {

            brush = new SolidBrush(Color.Green);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 3; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);
                
                y -= 10;
            }
            klocek[3] = new Rectangle(x + 10, y+20, width, height);
        }
        public void Elka()
        {

            brush = new SolidBrush(Color.Brown);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 3; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);

                y -= 10;
            }
            klocek[3] = new Rectangle(x + 10, y + 30, width, height);
        }
        public void ElkaOdwr()
        {

            brush = new SolidBrush(Color.Gold);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 3; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);

                y -= 10;
            }
            klocek[3] = new Rectangle(x + 10, y + 10, width, height);
        }
        public void Ha()
        {

            brush = new SolidBrush(Color.Purple);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 2; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);
                klocek[2 + i] = new Rectangle(x + 10, y+10, width, height);
                y -= 10;
            }
        }
        public void Zet()
        {

            brush = new SolidBrush(Color.Blue);
            pen = new Pen(Color.Black, 2);
            for (int i = 0; i < 2; i++)
            {
                klocek[i] = new Rectangle(x, y, width, height);
                klocek[2 + i] = new Rectangle(x + 10, y - 10, width, height);
                y -= 10;
            }
        }
        
        
        public void KlocekWDół(Rectangle[] klocek)
        {
            for (var i = 0; i < klocek.Length; i++ )
            {
                klocek[i].Y += 10;
            }
            
        }
        public void KlocekWPrawo(Rectangle[] klocek)
        {
            for (var i = 0; i < klocek.Length; i++)
            {
                klocek[i].X += 10;
            }
        }
        public void KlocekWLewo(Rectangle[] klocek)
        {
            for (var i = 0; i < klocek.Length; i++)
            {
                klocek[i].X -= 10;
            }
        }
        public void KlocekWGórę(Rectangle[] klocek)
        {
            for (var i = 0; i < klocek.Length; i++)
            {
                klocek[i].Y -= 10;
            }
        }
        

    }
}
