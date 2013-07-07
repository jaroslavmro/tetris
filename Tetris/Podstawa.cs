using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tetris
{
    class Podstawa
    {   
        public Rectangle[] wszystko = new Rectangle[1];
    
        
        public SolidBrush brush = new SolidBrush(Color.Green);
        public Pen pen = new Pen(Color.Blue, 2);
        public void drawDół(Graphics paper)
        {
            foreach (Rectangle rec in wszystko)
            {
                paper.FillRectangle(brush, rec);
                paper.DrawRectangle(pen, rec);
                paper.DrawLine(pen, 100, 0, 100, 300);
            }
        }
       
    }
}
