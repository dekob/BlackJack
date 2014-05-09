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
    public partial class BrojNaIgraci : Form
    {
        private Igrac ig;
        private Point pred;
        private bool pritisnato=false;
        private Pocetna p1;
        private String[] iminja;
        private int[] vlogovi;
        public BrojNaIgraci(Pocetna p)
        {
            InitializeComponent();
            ig = new Igrac();
            pred = Point.Empty;
            iminja = new String[5];
            vlogovi = new int[5];
            p1 = p;
        }
      private bool proverka(Point pred)
        {
            if (((pred.X)+80) > (panel1.Width))
                return false;
            return true;
        }
        private void IscrtajSlika()
        {
            Point tocka = Point.Empty;
            if ((ig.Broj >= 1) && (ig.Broj <= 5))
            {
                if (ig.Broj == 1)
                {

                    pictureBox1.Image = Properties.Resources.player;
                    textBox3.Visible = true;
                    label2.Visible = true;
                    label1.Visible = label3.Visible = label4.Visible = label5.Visible = false;
                    numericUpDown1.Visible = numericUpDown2.Visible = numericUpDown4.Visible = false;
                    numericUpDown3.Visible = true;
                    textBox4.Visible = textBox5.Visible = textBox6.Visible = textBox7.Visible = false;
                    pictureBox2.Image = pictureBox22.BackgroundImage;
                    pictureBox3.Image = pictureBox33.BackgroundImage;
                    pictureBox4.Image = pictureBox44.BackgroundImage;
                    pictureBox5.Image = pictureBox55.BackgroundImage;
                    textBox3.Enabled = true;
                }
                else if (ig.Broj == 2)
                {
                    label2.Visible = true;
                    numericUpDown3.Visible = true;
                    label3.Visible = true;
                    numericUpDown4.Visible = true;
                    label1.Visible = label4.Visible = label5.Visible = false;
                    numericUpDown1.Visible = numericUpDown2.Visible = numericUpDown5.Visible = false;
                    textBox5.Visible = textBox6.Visible = textBox7.Visible = false;
                    textBox3.Visible = textBox4.Visible = true;
                    pictureBox2.Image = pictureBox1.Image = Properties.Resources.player;
                    pictureBox3.Image = pictureBox33.BackgroundImage;
                    pictureBox4.Image = pictureBox44.BackgroundImage;
                    pictureBox5.Image = pictureBox55.BackgroundImage;
                }
                else if (ig.Broj == 3)
                {
                    label4.Visible = true;
                    numericUpDown5.Visible = true;
                    label2.Visible = true;
                    numericUpDown3.Visible = true;
                    label3.Visible = true;
                    numericUpDown4.Visible = true;
                    label1.Visible = label5.Visible = false;
                    numericUpDown1.Visible = numericUpDown2.Visible = false;
                    textBox6.Visible = textBox7.Visible = false;
                    textBox5.Visible = textBox3.Visible = textBox4.Visible = true;
                    pictureBox3.Image = pictureBox2.Image = pictureBox1.Image = Properties.Resources.player;
                    pictureBox4.Image = pictureBox44.BackgroundImage;
                    pictureBox5.Image = pictureBox55.BackgroundImage;
                }
                else if (ig.Broj == 4)
                {
                    label1.Visible = true;
                    numericUpDown2.Visible = true;
                    label4.Visible = true;
                    numericUpDown5.Visible = true;
                    label2.Visible = true;
                    numericUpDown3.Visible = true;
                    label3.Visible = true;
                    numericUpDown4.Visible = true;
                    label5.Visible = false;
                    numericUpDown1.Visible = false;
                    textBox7.Visible = false;
                    textBox6.Visible = textBox5.Visible = textBox3.Visible = textBox4.Visible = true; 
                    pictureBox4.Image = pictureBox3.Image = pictureBox2.Image = pictureBox1.Image = Properties.Resources.player;
                    pictureBox5.Image = pictureBox55.BackgroundImage;
                }
                else if (ig.Broj == 5)
                {
                    label5.Visible = true;
                    numericUpDown1.Visible = true;
                    label1.Visible = true;
                    numericUpDown2.Visible = true;
                    label4.Visible = true;
                    numericUpDown5.Visible = true;
                    label2.Visible = true;
                    numericUpDown3.Visible = true;
                    label3.Visible = true;
                    numericUpDown4.Visible = true;
                    textBox7.Visible = textBox6.Visible = textBox5.Visible = textBox3.Visible = textBox4.Visible = true;
                    textBox5.Visible = textBox3.Visible = textBox4.Visible = true; 
                    pictureBox5.Image = pictureBox4.Image = pictureBox3.Image = pictureBox2.Image = pictureBox1.Image = Properties.Resources.player;
                }
            }
            else if (ig.Broj >= 5)
            {
                pictureBox5.Image = pictureBox4.Image = pictureBox3.Image = pictureBox2.Image = pictureBox1.Image = Properties.Resources.player;
                textBox7.Visible = textBox6.Visible = textBox5.Visible = textBox3.Visible = textBox4.Visible = true;
            }
            else
            {
                pictureBox5.Image = pictureBox55.BackgroundImage;
                pictureBox1.Image = pictureBox11.BackgroundImage;
                pictureBox2.Image = pictureBox22.BackgroundImage;
                pictureBox3.Image = pictureBox33.BackgroundImage;
                pictureBox4.Image = pictureBox44.BackgroundImage;
                textBox7.Visible = textBox6.Visible = textBox5.Visible = textBox3.Visible = textBox4.Visible = false;
                label1.Visible = label2.Visible = label3.Visible = label4.Visible = label5.Visible = false;
                numericUpDown1.Visible = numericUpDown2.Visible = numericUpDown3.Visible = numericUpDown4.Visible = numericUpDown5.Visible = false;
            }
            if ((ig.Broj >= 1) && (ig.Broj <= 5))
            {
                button1.Text = "";
                button1.Text = "Започни Игра (Вкупен Број на Играчи: " + ig.Broj.ToString() + ")";
                button1.Enabled = true;

            }
            else { button1.Enabled = false; button1.Text = "Започни Игра (Вкупен Број на Играчи:) "; }
        }
        private void button2_Click(object sender, EventArgs e)
        {
           
            if (ig.Broj >= 5)
            {
                textBox2.Visible = true;
                IscrtajSlika();
                
            }
            else 
            {
                textBox2.Visible =false;
                ig.AddOne();
                String[] p = { " ", ig.Broj.ToString() };
                textBox1.Lines = p;
                IscrtajSlika();
            }
            }

        private void button3_Click(object sender, EventArgs e)
        {
            
                ig.RemoveOne();
                if (ig.Broj > 0)
                {
                    textBox2.Visible = false;
                    String[] p = { " ", ig.Broj.ToString() };
                    textBox1.Lines = p;
                    IscrtajSlika();
                }
                else
                {
                    textBox2.Visible = true;
                    String[] p = { " ", ig.Broj.ToString() };
                    textBox1.Lines = p;
                    IscrtajSlika();
                }
            
        }

        private void button1_Paint(object sender, PaintEventArgs e)
        {

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pritisnato = true;
            this.Close();
            
            
        }

        private void BrojNaIgraci_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if ((ig.Broj > 0) && (pritisnato))
            {
                if (ig.Broj == 1) { iminja[0] = textBox3.Text; vlogovi[0] = Convert.ToInt16(numericUpDown3.Value); }
                else if (ig.Broj == 2) { iminja[0] = textBox3.Text; vlogovi[1]=Convert.ToInt16(numericUpDown4.Value);iminja[1] = textBox4.Text; vlogovi[0] = Convert.ToInt16(numericUpDown3.Value); }
                else if (ig.Broj == 3) { iminja[0] = textBox3.Text; vlogovi[2] = Convert.ToInt16(numericUpDown5.Value); vlogovi[1] = Convert.ToInt16(numericUpDown4.Value); vlogovi[0] = Convert.ToInt16(numericUpDown3.Value); iminja[1] = textBox4.Text; iminja[2] = textBox5.Text; }
                else if (ig.Broj == 4) { iminja[0] = textBox3.Text; vlogovi[3] = Convert.ToInt16(numericUpDown2.Value); vlogovi[2] = Convert.ToInt16(numericUpDown5.Value); vlogovi[1] = Convert.ToInt16(numericUpDown4.Value); vlogovi[0] = Convert.ToInt16(numericUpDown3.Value); iminja[1] = textBox4.Text; iminja[2] = textBox5.Text; iminja[3] = textBox6.Text; }
                else if (ig.Broj == 5) { iminja[0] = textBox3.Text; vlogovi[4] = Convert.ToInt16(numericUpDown1.Value); vlogovi[3] = Convert.ToInt16(numericUpDown2.Value); vlogovi[2] = Convert.ToInt16(numericUpDown5.Value); vlogovi[1] = Convert.ToInt16(numericUpDown4.Value); vlogovi[0] = Convert.ToInt16(numericUpDown3.Value); iminja[1] = textBox4.Text; iminja[2] = textBox5.Text; iminja[3] = textBox6.Text; iminja[4] = textBox7.Text; }
                p1.br = ig.Broj;
                p1.iminj = iminja;
                p1.vlog = vlogovi;
            }
        }


    }
}
