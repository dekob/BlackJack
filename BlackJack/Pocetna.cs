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
    public partial class Pocetna : Form
    {
        public int br;
        public String[] iminj;
        public int[] vlog;
        public Pocetna()
        {
            InitializeComponent();
            br = 0;
            
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Pomos p = new Pomos();
            p.ShowDialog();
            
        }
        public void NovaIgra()
        {
            if (br > 0)
            {
                GlavnaIgra f = new GlavnaIgra(br, this, iminj, vlog);
                f.ShowDialog();
               
                
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (br > 0)
            {
                GlavnaIgra f = new GlavnaIgra(br, this, iminj, vlog);
                f.ShowDialog();
                
               
            }
     else MessageBox.Show("Ве молиме изберете број на играчи кои ќе учествуваат во играта.", "Изберете играчи", 
         MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            br = 0;
            BrojNaIgraci b = new BrojNaIgraci(this);
            b.ShowDialog();
            if (br > 0)
            {
                GlavnaIgra gl = new GlavnaIgra(br, this, iminj, vlog);
                gl.ShowDialog();
            }
        }

        private void Pocetna_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser("https://instagram.com/black_jack_macedonia/#");

            web.Text = "Instagram- Black Jack Macedonia Game";
            web.ShowDialog();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser("https://twitter.com/BlackJackMacedo");
            web.Text = "Twitter-Black Jack Macedonia Game";
            web.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            WebBrowser web = new WebBrowser("https://www.facebook.com/BlackJackMacedoniaGame?fref=ts");
            web.Text = "Facebook- Black Jack Macedonia Game";
            web.ShowDialog();
        }

        
       
        

       
    }
    
}
