using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class Form1 : Form
    {
        Graphics paper;
        Klocek klocek = new Klocek();
        Podstawa podstawa = new Podstawa();
        int score = 0;



        public Form1()
        {
            InitializeComponent();


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (timer1.Enabled)
            {
                paper = e.Graphics;
                klocek.drawKlocek(paper);
                podstawa.drawDół(paper);

            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            klocek.KlocekWDół(klocek.klocek);

            Łączenie();


            this.Invalidate();
        }
        private void Łączenie()
        {
            bool pasujesz = DziejeSię(klocek.klocek, podstawa.wszystko);


            if (pasujesz && podstawa.wszystko.Length == 0)
            {
                klocek.KlocekWGórę(klocek.klocek);
                podstawa.wszystko = new Rectangle[4];
                foreach (var i in Enumerable.Range(0, 4))
                {
                    podstawa.wszystko[i] = klocek.klocek[i];
                }


            }
            else if (pasujesz)
            {
                klocek.KlocekWGórę(klocek.klocek);
                Rectangle[] po = new Rectangle[podstawa.wszystko.Length + 4];
                var poor = podstawa.wszystko.ToList();
                var poor_count = poor.Count;
                foreach (var i in Enumerable.Range(0, podstawa.wszystko.Length))
                {
                    po[i] = podstawa.wszystko[i];
                }

                foreach (var i in klocek.klocek)
                {
                    poor.Add(i);
                }
                foreach (var k in poor)
                {
                    Console.WriteLine(k);
                }
                podstawa.wszystko = poor.ToArray();
                Klocek klocek1 = new Klocek();
                klocek = klocek1;
                Czyszczenie(podstawa.wszystko);


            }
        }
        private int[] Maksima(Rectangle[] klocki)
        {
            int[] maxs = new int[10];
            if (klocki == null)
            {
                foreach (var j in Enumerable.Range(0, 10))
                {
                    maxs[j] = 290;
                }

            }
            else
            {
                foreach (var j in Enumerable.Range(0, 10))
                {
                    List<Int32> interesuje = new List<Int32> { };
                    int x = 0;
                    foreach (var i in klocki)
                    {
                        if (i.X != 0)
                        {
                            if (i.X == j * 10)
                            {
                                interesuje.Add(Math.Min(i.Y - 10, 290));
                                x++;
                            }
                        }
                    }
                    if (x == 0)
                    {

                        maxs[j] = 290;
                    }
                    else
                    {
                        maxs[j] = interesuje.Min();
                    }

                }
            }

            return maxs;
        }
        private int[] Minima(Rectangle[] klocki)
        {
            int[] maxs = new int[10];


            foreach (var j in Enumerable.Range(0, 10))
            {
                List<Int32> interesuje = new List<Int32> { };
                int x = 0;
                foreach (var i in klocki)
                {
                    if (i.X == j * 10)
                    {
                        interesuje.Add(i.Y + 10);
                        x++;
                    }

                }
                if (x == 0)
                {

                    maxs[j] = 0;
                }
                else
                {
                    maxs[j] = interesuje.Max();
                }
            }
            return maxs;
        }

        private bool pasuje(int[] maxy, int[] minima)
        {
            bool ant = true;
            var x = 0;
            while (x < maxy.Length)
            {
                if (maxy[x] < minima[x])
                {
                    ant = false;

                }
                x++;
            }


            return ant;
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyData == Keys.Right)
            {
                klocek.KlocekWPrawo(klocek.klocek);
                if (DziejeSię(klocek.klocek, podstawa.wszystko))
                {
                    klocek.KlocekWLewo(klocek.klocek);
                }



            }
            if (e.KeyData == Keys.Left)
            {
                klocek.KlocekWLewo(klocek.klocek);
                if (DziejeSię(klocek.klocek, podstawa.wszystko))
                {
                    klocek.KlocekWPrawo(klocek.klocek);
                }
            }
            if (e.KeyData == Keys.Down)
            {
                timer1.Interval = 25;
            }
            if (e.KeyData == Keys.Up)
            {
                Obracanie(klocek.klocek);

                if (DziejeSię(klocek.klocek, podstawa.wszystko))
                {
                    AntyObracanie(klocek.klocek);
                    AntyObracanie(klocek.klocek);
                    if (DziejeSię(klocek.klocek, podstawa.wszystko))
                    {
                        Obracanie(klocek.klocek);
                    }
                }

            }
            if (e.KeyData == Keys.Space && timer1.Enabled == false)
            {

                klocek = new Klocek();
                podstawa.wszystko = new Rectangle[1];
                score = 0;
                timer1.Enabled = true;
                label2.Text = "0";



            }

        }
        private bool DziejeSię(Rectangle[] Klocki, Rectangle[] Podstawa)
        {
            var arr = new Rectangle(0, 300, 100, 10);
            var lewo = new Rectangle(-10, 0, 10, 300);
            var prawo = new Rectangle(100, 0, 10, 300);
            var góra = new Rectangle(0, -20, 100, 20);
            var ando = false;
            foreach (var klocek in Klocki)
            {
                foreach (var klocki in Podstawa)
                {
                    if (klocek.IntersectsWith(klocki) || klocek.IntersectsWith(arr) || klocek.IntersectsWith(lewo) || klocek.IntersectsWith(prawo))
                    {
                        ando = true;
                    }

                }

            }



            return ando;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                timer1.Interval = 500;
            }

        }
        private void Obracanie(Rectangle[] Klocki)
        {
            var X = Klocki[0].X;
            var Y = Klocki[0].Y;
            for (int i = 1; i < Klocki.Length; i++)
            {
                var X2 = Klocki[i].X - X;
                var Y2 = Klocki[i].Y - Y;

                Klocki[i].X = X + Y2;
                Klocki[i].Y = Y - X2;
            }
        }
        private void AntyObracanie(Rectangle[] Klocki)
        {
            var X = Klocki[0].X;
            var Y = Klocki[0].Y;
            for (int i = 1; i < Klocki.Length; i++)
            {
                var X2 = Klocki[i].X - X;
                var Y2 = Klocki[i].Y - Y;

                Klocki[i].X = X - Y2;
                Klocki[i].Y = Y + X2;
            }
        }
        private Rectangle[] Obracanie2(Rectangle[] Klocki)
        {
            var X = Klocki[0].X;
            var Y = Klocki[0].Y;
            for (int i = 1; i < Klocki.Length; i++)
            {
                var X2 = Klocki[i].X - X;
                var Y2 = Klocki[i].Y - Y;

                Klocki[i].X = X + Y2;
                Klocki[i].Y = Y - X2;
            }
            return Klocki;
        }
        private void Czyszczenie1(Rectangle[] Podstawa)
        {

            if (Podstawa.Length >= 10)
            {
                List<Rectangle> po = new List<Rectangle> { };
                List<Int32> pod = new List<Int32> { };
                Rectangle[] konst = new Rectangle[Podstawa.Length - 10];
                foreach (var k in Podstawa)
                {
                    pod.Add(k.Y);
                }
                var i = 29;
                while (i > 20)
                {
                    int x = 0;
                    foreach (var j in Enumerable.Range(0, Podstawa.Length))
                    {

                        if (Podstawa[j].Y == i * 10)
                        {
                            x++;

                        }
                        if (j == Podstawa.Length - 1)
                        {
                            Console.WriteLine(x);
                        }
                        if (x == 10)
                        {
                            foreach (var item in Enumerable.Range(0, Podstawa.Length))
                            {
                                if (Podstawa[item].X != i * 10)
                                {

                                    po.Add(Podstawa[item]);

                                }

                                if (Podstawa[item].Y < i * 10)
                                {
                                    Podstawa[item].Y += 10;

                                }

                            }
                            Podstawa = po.ToArray();
                            break;
                        }




                    }

                    Console.WriteLine(Podstawa.Length);
                    i--;

                }




            }
        }
        private void Czyszczenie(Rectangle[] Podstawka)
        {
            if (Podstawka.Length >= 10)
            {


                List<Int32> pod = new List<Int32> { };
                List<Rectangle> indeksy = new List<Rectangle> { };
                List<Int32> podliczby = new List<Int32> { };
                foreach (var j in Enumerable.Range(0, 32))
                {
                    pod.Add(0);

                }
                foreach (var i in Podstawka)
                {
                    if (i.Y < 0)
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Game Over! Your score is " + score.ToString());
                        break;
                    }
                    else
                    {
                        pod[i.Y / 10]++;
                        Console.WriteLine(pod[i.Y / 10]);
                    }


                }
                foreach (var item in Enumerable.Range(0, 32))
                {
                    if (pod[item] == 10)
                    {
                        foreach (var k in Podstawka)
                        {
                            if (k.Y == item * 10)
                            {
                                indeksy.Add(k);
                            }


                        }
                    }
                }
                if (indeksy.Count >= 10)
                {

                    var nowa = Podstawka.ToList();


                    foreach (var l in indeksy)
                    {

                        if (Podstawka.Contains(l))
                        {
                            nowa.Remove(l);
                            Console.WriteLine(l);
                        }
                        if (!podliczby.Contains(l.Y))
                        {
                            podliczby.Add(l.Y);
                        }


                    }


                    Podstawka = nowa.ToArray();
                    podstawa.wszystko = Podstawka;

                    foreach (var i in Enumerable.Range(0, Podstawka.Length))
                    {
                        foreach (var item in podliczby)
                        {
                            if (Podstawka[i].Y < item)
                            {
                                Podstawka[i].Y += 10;
                            }
                        }
                    }
                    if (indeksy.Count == 40)
                    {
                        score += 1000;
                    }
                    else
                    {
                        score += indeksy.Count * 10;
                    }
                    Console.WriteLine(podstawa.wszystko.Length);
                    label2.Text = score.ToString();
                }


            }

        }
    }
}

