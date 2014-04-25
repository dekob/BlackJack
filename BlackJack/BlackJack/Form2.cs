using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BlackJack
{
    public partial class Form2 : Form
    {
        private int sekund;
        private int minut;
        private int milisekund;
        private Random r;
        public Form2()
        {
            InitializeComponent();
            timer1.Enabled = true;
            sekund = 0;
            minut = 0;
            milisekund = 0;
            timer2.Enabled = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r = new Random();
            r.Next(1, 12);
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sekund++;
            if (milisekund > 60)
            {
                sekund++;
            }
            if (sekund > 60)
            {
                minut++;
            }
            if (minut ==0) label1.Text = String.Format("{0,1}", sekund, milisekund);
            else label1.Text = String.Format("{0, 1}", minut, sekund);
            }

        private void timer2_Tick(object sender, EventArgs e)
        {
            Point p = pictureBox2.Location;
            int x = p.X+1;
            int y = p.Y+1;
            p = new Point(x, y);
            pictureBox2.Location = p;
            toolTip1.Active = true;
            Point p2 = pictureBox2.Location;
            Point p1 = pictureBox1.Location;
            if ((p2.Y == p1.Y))
            {
                timer2.Stop();
            }
        }

        private void pictureBox2_LocationChanged(object sender, EventArgs e)
        {
            Point p = pictureBox2.Location;
            Point p1 = pictureBox1.Location;
            if ((p.Y==p1.Y))
            {
                timer2.Stop();
            }
        }
    }
}
