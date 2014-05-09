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
    public partial class GlavnaIgra : Form
    {
        private int sekund;
        private int minut;
        private int casot;
        public int brojac{set;get;}
        List<Player> igraci;
        List<Karta> karti;
        Dealer dealer;
        Random r;
        Player aktivenIgrac;
        private int brojigraci;
        private Pocetna p;
        usedCards upotrebeniKarti;
        String[] iminja;
        int[] vlogo;
        public Timer timerIgrac { set; get; }
        public GlavnaIgra(int brojig, Pocetna p1, String[] imi, int[] vlog)
        {
            InitializeComponent();
            igraci = new List<Player>();
            timerIgrac = new Timer();
            timerIgrac.Interval = 500;
            timerIgrac.Tick += new EventHandler(timerIgrac_Tick);
            upotrebeniKarti = new usedCards();
            karti = new List<Karta>();
            generirajSpil();
            r = new Random();
            aktivenIgrac = null;
            iminja = imi;
            vlogo = vlog;
            brojac = 0;
            sekund = 0;
            minut = 0;
            casot=0;
            p = p1;
            brojigraci = brojig;
            textBox1.Enabled = false;
            timer1.Enabled = true;
            timer2.Enabled = true;
            GenerirajSlikiIgraci();
            popolniList();
            zapocniIgra();
        }

        void timerIgrac_Tick(object sender, EventArgs e)
        {
            brojac++;
            if (brojac == 120)
            {
                timerIgrac.Stop();
                otkaziIgrac();
                MessageBox.Show("Играчот се откажа.");
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = true;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = true;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = true;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = true;
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr] != null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    otvoriKartaDealer();
                }

            }
            ProgressBar pro = (ProgressBar)aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr];
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Text = (120 - brojac).ToString();
            pro.Value = brojac;
        }

        private void popolniList()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(String.Format("{0, -20}", "Име на играч")+String.Format("{0, -16}", "Збир на карти")+"Вкупен влог");
            listBox1.Items.Add("----------------------------------------------------------------------------");
            
            foreach (Player p in igraci)
            {
                listBox1.Items.Add(String.Format("{0,-30}", p.ime)+ String.Format("{0, -20}", p.presmetajZbir().ToString()) + p.vlog.ToString());
                
            }
            listBox1.Items.Add("---------------------------------------------------------------------------");
            listBox1.Items.Add(String.Format("{0, -32}", "Делач")+dealer.PresmetajZbirKarti() );

        }

        private void generirajSpil()
        {
          
        }

        private void zapocniIgra()
        {
            aktivenIgrac = null;
            foreach (Player p in igraci)
            {

                if (p.igra)
                {
                    aktivenIgrac = p;
                    break;
                }
            }
            if (aktivenIgrac != null)
            {
                aktivenIgrac.PostaviAktiven();
                
            }

            
           
        }

        private void GenerirajSlikiIgraci()
        {

            Random r = new Random();
            int boja = r.Next(1, 4);
            int broj = r.Next(2, 14);
            dealer = new Dealer(upotrebeniKarti);
            Karta k = new Karta(dealer, boja, broj);
            dealer.dodajKarta(k);
            String pateka = "karti/";
            pateka += broj.ToString() + "-" + boja.ToString() + ".png";
            upotrebeniKarti.dodajKarta(broj, boja);
            pictureBox8.Image = Image.FromFile(pateka);
            pictureBox7.Image = Properties.Resources.zadna;
            label7.Text = "Збир на картите: " + dealer.PresmetajZbirKarti().ToString();
            if (brojigraci == 1)
            {
                Player player = new Player(upotrebeniKarti, panel1, 1, this);
                player.ime = iminja[0];
                player.vlog = vlogo[0];
                textBox2.Text = iminja[0];
                textBox8.Text = vlogo[0].ToString();
                textBox2.Visible = true;
                textBox8.Visible = true;
                label111.Visible = true;
                label112.Visible = true;
                label111.Text = "Збир на карти: " + player.presmetajZbir().ToString();
                igraci.Add(player);
                pictureBox1.Image = Properties.Resources.player;
                panel1.Controls.Add(pictureBox1);
                player.postaviSlika();

            }
            else if (brojigraci == 2)
            {
                Player player = new Player(upotrebeniKarti, panel1,1, this);
                player.ime = iminja[0];
                player.vlog = vlogo[0];
                textBox8.Text = vlogo[0].ToString();
                label112.Visible = true;
                Player player1 = new Player(upotrebeniKarti, panel2, 2, this);
                player1.ime = iminja[1];
                player1.vlog = vlogo[1];
                textBox9.Text = vlogo[1].ToString();
                label212.Visible = true;
                pictureBox1.Image = Properties.Resources.player;
                pictureBox2.Image = Properties.Resources.player;
                textBox8.Visible = textBox9.Visible=true;
                label111.Visible = true;
                label111.Text = "Збир на карти: " + player.presmetajZbir().ToString();
                label222.Visible = true;
                label222.Text = "Збир на карти: " + player1.presmetajZbir().ToString();
               
                textBox2.Visible =textBox3.Visible= true;
                textBox2.Text = iminja[0];
                textBox3.Text = iminja[1];
                panel1.Controls.Add(pictureBox1);
                panel2.Controls.Add(pictureBox2);
                player.postaviSlika();
                player1.postaviSlika();
                igraci.Add(player);
                igraci.Add(player1);

            }
            else if (brojigraci == 3)
            {
                Player player = new Player(upotrebeniKarti, panel1, 1, this);
                player.ime = iminja[0];
                player.vlog = vlogo[0];
                textBox8.Text = vlogo[0].ToString() ;
                label112.Visible = true;
                Player player2 = new Player(upotrebeniKarti, panel2, 2, this);
                player2.ime = iminja[1];
                player2.vlog = vlogo[1];
                textBox9.Text = vlogo[1].ToString();
                label212.Visible = true;
                Player player3 = new Player(upotrebeniKarti, panel3, 3, this);
                player3.ime = iminja[2];
                player3.vlog = vlogo[2];
                textBox10.Text = vlogo[2].ToString();
                label312.Visible = true;
                textBox8.Visible = textBox9.Visible =textBox10.Visible= true;
                textBox2.Visible = textBox3.Visible = textBox4.Visible=true;
                pictureBox1.Image = Properties.Resources.player;
                pictureBox2.Image = Properties.Resources.player;
                pictureBox3.Image = Properties.Resources.player;
                label111.Visible = true;
                label111.Text = "Збир на карти: " + player.presmetajZbir().ToString();
                label222.Visible = true;
                label222.Text = "Збир на карти: " + player2.presmetajZbir().ToString();
                label333.Visible = true;
                label333.Text = "Збир на карти: " + player3.presmetajZbir().ToString();
              
                textBox2.Text = iminja[0];
                textBox3.Text = iminja[1];
                textBox4.Text = iminja[2];
                panel1.Controls.Add(pictureBox1);
                panel2.Controls.Add(pictureBox2);
                player.postaviSlika();
                player2.postaviSlika();
                player3.postaviSlika();
                igraci.Add(player);
                igraci.Add(player2);
                igraci.Add(player3);
            }
            else if (brojigraci == 4)
            {
                Player player = new Player(upotrebeniKarti, panel1,1, this);
                player.ime = iminja[0];
                player.vlog = vlogo[0];
                textBox8.Text = vlogo[0].ToString();
                label112.Visible = true;
                Player player1 = new Player(upotrebeniKarti, panel2, 2, this);
                player1.ime = iminja[1];
                player1.vlog = vlogo[1];
                textBox9.Text = vlogo[1].ToString();
                label212.Visible = true;
                Player player2 = new Player(upotrebeniKarti, panel3, 3, this);
                player2.ime = iminja[2];
                player2.vlog = vlogo[2];
                textBox10.Text = vlogo[2].ToString();
                label312.Visible = true;
                Player player3 = new Player(upotrebeniKarti, panel4, 4, this);
                player3.ime = iminja[3];
                player3.vlog = vlogo[3];
                textBox11.Text = vlogo[3].ToString();
                label412.Visible = true;
                textBox8.Visible = textBox9.Visible = textBox10.Visible = textBox11.Visible=true;
                textBox2.Visible = textBox3.Visible = textBox4.Visible =textBox5.Visible= true;
                panel1.Controls.Add(pictureBox1);
                panel2.Controls.Add(pictureBox2);
                pictureBox1.Image = Properties.Resources.player;
                pictureBox2.Image = Properties.Resources.player;
                pictureBox3.Image = Properties.Resources.player;
                pictureBox4.Image = Properties.Resources.player;
                label111.Visible = true;
                label111.Text = "Збир на карти: " + player.presmetajZbir().ToString();
                label222.Visible = true;
                label222.Text = "Збир на карти: " + player1.presmetajZbir().ToString();
                label333.Visible = true;
                label333.Text = "Збир на карти: " + player2.presmetajZbir().ToString();
                label444.Visible = true;
                label444.Text = "Збир на карти: " + player3.presmetajZbir().ToString();
             
                textBox2.Text = iminja[0];
                textBox3.Text = iminja[1];
                textBox4.Text = iminja[2];
                textBox5.Text = iminja[3];
                player.postaviSlika();
                player1.postaviSlika();
                player2.postaviSlika();
                player3.postaviSlika();
                igraci.Add(player);
                igraci.Add(player1);
                igraci.Add(player2);
                igraci.Add(player3);


            }
            else if (brojigraci == 5)
            {
                Player player = new Player(upotrebeniKarti, panel1, 1, this);
                player.ime = iminja[0];
                player.vlog = vlogo[0];
                textBox8.Text = vlogo[0].ToString();
                label112.Visible = true;
                Player player1 = new Player(upotrebeniKarti, panel2, 2, this);
                player1.ime = iminja[1];
                player1.vlog = vlogo[1];
                textBox9.Text = vlogo[1].ToString();
                label212.Visible = true;
                Player player2 = new Player(upotrebeniKarti, panel3, 3, this);
                player2.ime = iminja[2];
                player2.vlog = vlogo[2];
                textBox10.Text = vlogo[2].ToString();
                label312.Visible = true;
                Player player3 = new Player(upotrebeniKarti, panel4, 4,this);
                player3.ime = iminja[3];
                player3.vlog = vlogo[3];
                textBox11.Text = vlogo[3].ToString();
                label412.Visible = true;
                Player player4 = new Player(upotrebeniKarti, panel5, 5, this);
                player4.ime = iminja[4];
                player4.vlog = vlogo[4];
                textBox12.Text = vlogo[4].ToString();
                label512.Visible = true;
                textBox8.Visible = textBox9.Visible = textBox10.Visible = textBox11.Visible =textBox12.Visible= true;
                textBox2.Visible = textBox3.Visible = textBox4.Visible = textBox5.Visible =textBox6.Visible= true;
                pictureBox1.Image = Properties.Resources.player;
                pictureBox2.Image = Properties.Resources.player;
                pictureBox3.Image = Properties.Resources.player;
                pictureBox4.Image = Properties.Resources.player;
                pictureBox5.Image = Properties.Resources.player;
                label111.Visible = true;
                label111.Text = "Збир на карти: " + player.presmetajZbir().ToString();
                label222.Visible = true;
                label222.Text = "Збир на карти: " + player1.presmetajZbir().ToString();
                label333.Visible = true;
                label333.Text = "Збир на карти: " + player2.presmetajZbir().ToString();
                label444.Visible = true;
                label444.Text = "Збир на карти: " + player3.presmetajZbir().ToString();
                label555.Visible = true;
                label555.Text = "Збир на карти: " + player4.presmetajZbir().ToString();  
                textBox2.Text = iminja[0];
                textBox3.Text = iminja[1];
                textBox4.Text = iminja[2];
                textBox5.Text = iminja[3];
                textBox6.Text = iminja[4];
                panel1.Controls.Add(pictureBox1);
                panel2.Controls.Add(pictureBox2);
                player.postaviSlika();
                player1.postaviSlika();
                player2.postaviSlika();
                player3.postaviSlika();
                player4.postaviSlika();
                igraci.Add(player);
                igraci.Add(player1);
                igraci.Add(player2);
                igraci.Add(player3);
                igraci.Add(player4);

            }
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
           
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sekund++;
            if (sekund >= 60)
            {
                minut++;
                sekund = 0;
            }
            if (minut >= 60)
            {
                casot++;
                minut = 0;
            }
            if (minut == 0) textBox1.Text ="Време: "+ String.Format("00:{0:00.#}", sekund);
            else if (casot == 0) textBox1.Text = "Време: "+String.Format("{0:00.#}:{1:00.#}", minut, sekund);
            else textBox1.Text = "Време: "+string.Format("{0:00.#}:{1:00.#}:{2:00.#}", casot, minut, sekund);
            }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToString();
        }

        public void IzmesajKarti()
        { 
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            int broja = 0;
            int boja1=0;
            brojac = 0;
            while (true)
            { 
                 broja=r.Next(2, 14);
            boja1=r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja-1, boja1-1))
                {
                    upotrebeniKarti.dodajKarta(broja-1, boja1-1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, aktivenIgrac.brojNaKarti+1, upotrebeniKarti, boja1, broja);
            
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString()+".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + (aktivenIgrac.brojNaKarti+1).ToString()];
            aktivenIgrac.dodadiKarta(k);
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label111.Text ="Збир на карти: "+aktivenIgrac.presmetajZbir().ToString();
            aktivenIgrac.aktiven = true;
            aktivenIgrac.igra = true;
            int p = aktivenIgrac.id_igr;
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                izbrisiIgrac(aktivenIgrac);
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();
            
            }
            else aktivenIgrac.PostaviAktiven();
            
        }

        public void pobednikNaIgrata()
        {
            timerIgrac.Stop();
            DialogResult d = MessageBox.Show("Играчот: "+String.Format("{0}", aktivenIgrac.ime)+" доби BlackJack. Неговата добивка изнесува"+String.Format("{0}", (aktivenIgrac.vlog+(int)(aktivenIgrac.vlog*1.50))), "BlackJack.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            aktivenIgrac.vlog= aktivenIgrac.vlog+(int)(aktivenIgrac.vlog*1.50);
            aktivenIgrac.pobednik = true;
            aktivenIgrac.igra = false;
            ispolnilistapobednik();
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr] != null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void ispolnilistapobednik()
        {
           
            listBox2.Items.Add(aktivenIgrac.ime + "            " + (aktivenIgrac.vlog+(int)(aktivenIgrac.vlog*1.5)));
        }

        private void izbrisiIgrac(Player aktivenIgrac)
        {
            MessageBox.Show("Играчот со имe:" + aktivenIgrac.ime + " изгуби бидејќи има збир на карти " + aktivenIgrac.presmetajZbir().ToString(), "Изгубена партија", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            aktivenIgrac.igra = false;
            listBox1.Items.Clear();
            listBox1.Items.Add("Име на Играч     Збир на карти     Вкупен Влог");
            listBox1.Items.Add("----------------------------------------------------------------------------");

            foreach (Player p in igraci)
            {
                if (p.id_igr == aktivenIgrac.id_igr)
                {

                    listBox1.Items.Add(String.Format("{0,-10}", p.ime) + " (изгуби)     " + p.presmetajZbir().ToString() + "                 " + p.vlog.ToString());

                }
                else listBox1.Items.Add(String.Format("{0,-30}", p.ime) + p.presmetajZbir().ToString() + "                   " + p.vlog.ToString());

            }
            listBox1.Items.Add("---------------------------------------------------------------------------");
            listBox1.Items.Add("Делач:" + "                        " + "                                              " + dealer.PresmetajZbirKarti().ToString());

 
            brojac = 0;
            aktivenIgrac.ikona.Dispose();
        }
        public void otkaziIgrac()
        {
            PictureBox pic = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr];
            pic.Image = Properties.Resources.slika;
            popolniListaOtkazan();
        }

        private void popolniListaOtkazan()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add("Име на Играч     Збир на карти     Вкупен Влог");
            listBox1.Items.Add("----------------------------------------------------------------------------");

            foreach (Player p in igraci)
            {
                if (p.id_igr == aktivenIgrac.id_igr)
                {
                   
                    listBox1.Items.Add(String.Format("{0,-10}", p.ime) + " (откажан)   "+p.presmetajZbir().ToString() + "                 " + p.vlog.ToString());

                }
                else  listBox1.Items.Add(String.Format("{0,-30}", p.ime) + p.presmetajZbir().ToString() + "                   " + p.vlog.ToString());

            }
            listBox1.Items.Add("---------------------------------------------------------------------------");
            listBox1.Items.Add("Делач:" + "                        " + dealer.PresmetajZbirKarti().ToString() + "                        " + Dealer.vlog.ToString());

    
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            if (aktivenIgrac.brojNaKarti <= 5)
            {
                int broja = 0;
                int boja1 = 0;
                brojac = 0;
                while (true)
                {
                    broja = r.Next(2, 14);
                    boja1 = r.Next(1, 4);
                    if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                    {
                        upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                        break;
                    }
                }
                Karta k = new Karta(aktivenIgrac, aktivenIgrac.brojNaKarti + 1, upotrebeniKarti, boja1, broja);

                String pateka = "karti/";
                pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
                PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + (aktivenIgrac.brojNaKarti + 1).ToString()];
                aktivenIgrac.dodadiKarta(k);
                box.Visible = true;
                box.Image = Image.FromFile(pateka);
                this.popolniList();
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                label222.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
                aktivenIgrac.aktiven = true;
                aktivenIgrac.igra = true;
                Player p = aktivenIgrac;
                if (aktivenIgrac.presmetajZbir() > 21)
                {
                    izbrisiIgrac(aktivenIgrac);
                    if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                    {
                        aktivenIgrac = igraci[aktivenIgrac.id_igr];
                        brojac = 0;
                        aktivenIgrac.PostaviAktiven();
                    }
                    else
                    {
                        otvoriKartaDealer();
                    }
                }
                else if (aktivenIgrac.presmetajZbir() == 21)
                {
                    pobednikNaIgrata();
                }
                else aktivenIgrac.PostaviAktiven();
            }

        }

        private void button31_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            int broja = 0;
            int boja1 = 0;
            brojac = 0;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, aktivenIgrac.brojNaKarti+1, upotrebeniKarti, boja1, broja);
            
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + (aktivenIgrac.brojNaKarti + 1).ToString()];
            aktivenIgrac.dodadiKarta(k);
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label333.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            aktivenIgrac.aktiven = true;
            Player p = aktivenIgrac;
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                izbrisiIgrac(aktivenIgrac);
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    otvoriKartaDealer();
                }
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();
            }
            else { aktivenIgrac.PostaviAktiven(); }

        }

        private void button41_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            int broja = 0;
            int boja1 = 0;
            brojac = 0;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, aktivenIgrac.brojNaKarti+1, upotrebeniKarti, boja1, broja);
            
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + (aktivenIgrac.brojNaKarti + 1).ToString()];

            aktivenIgrac.dodadiKarta(k); 
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label444.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            aktivenIgrac.aktiven = true;
            Player p = aktivenIgrac;
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                izbrisiIgrac(aktivenIgrac);
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();
            }
            else aktivenIgrac.PostaviAktiven();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            int broja = 0;
            int boja1 = 0;
            brojac = 0;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, aktivenIgrac.brojNaKarti+1, upotrebeniKarti, boja1, broja);
            
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + (aktivenIgrac.brojNaKarti + 1).ToString()];
            aktivenIgrac.dodadiKarta(k);
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label555.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            aktivenIgrac.aktiven = true;
            Player p = aktivenIgrac;
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                
                izbrisiIgrac(aktivenIgrac);
                otvoriKartaDealer();
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();
            }
            else { aktivenIgrac.PostaviAktiven(); }
           
        }

        private void button12_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            brojac = 0;
            if (aktivenIgrac.brojNaKarti < 21)
            {
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.igra = true;
                if ((igraci.Count > aktivenIgrac.id_igr) && ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null)))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            }
            else izbrisiIgrac(aktivenIgrac);
        }

        private void otvoriKartaDealer()
        {
            int bo = 0;
            int br = 0;
            timerIgrac.Stop();
            brojac = 0;
            while (true)
            {
                bo = r.Next(1, 4);
                br = r.Next(2, 14);
                if (!upotrebeniKarti.zafatenaKarta(br, bo))
                {
                    upotrebeniKarti.dodajKarta(br, bo);
                    break;
                }
            }
            Karta k = new Karta(dealer, bo, br);
            pictureBox7.Visible = true;
            pictureBox7.Image = k.zemiSlika();
            dealer.dodajKarta(k);
            while (true)
            {
                if (dealer.PresmetajZbirKarti() < 17)
                {
                    while (true)
                    {
                        bo = r.Next(1, 4);
                        br = r.Next(2, 14);
                        if (!upotrebeniKarti.zafatenaKarta(br, bo))
                        {
                            upotrebeniKarti.dodajKarta(br, bo);
                            break;
                        }
                    }
                    Karta k1 = new Karta(dealer, bo, br);
                    pictureBoxdealer.Visible = true;
                    pictureBoxdealer.Image = k1.zemiSlika();
                    dealer.dodajKarta(k1);
                }
                else break;
                
            }
            label7.Text = "Збир на карти: " + dealer.PresmetajZbirKarti().ToString();
            
            
            if (dealer.PresmetajZbirKarti() > 21)
            {
                int brojac1 = 0;
                foreach (Player p in igraci)
                {
                    if (p.igra)
                    {
                        brojac1++;
                    }
                }
                DialogResult d ;
                
                if (brojac1 > 0)
                {
                    String g = " ";
                    int pom = 0;
                    foreach (Player p in igraci)
                    {
                        if (p.igra)
                        {
                            g += p.ime;
                            g += " ";
                            pom = p.vlog / 2;
                             listBox2.Items.Add(p.ime+ "            " + p.vlog/2);
                        }
                    }
                   
                    d = MessageBox.Show("Играта е завршена. Делачот изгуби. Добитници се"+g+" со добивка"+pom+ " . Дали сакате уште една партија BlackJack?", "Завршена игра", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               
                }
                else {
                    int f = 0;
                    foreach (Player p in igraci)
                    {
                        if (p.igra)
                        {
                            f += p.vlog;
                        }
                    }
                    listBox2.Items.Add("dealer "+ "            " + f);

                    d = MessageBox.Show("Играта е завршена. Делачот победи. Неговата добивка изнесува "+f, "Дали сакате уште една партија BlackJack?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    
                }
                    if (d == DialogResult.Yes)
                {
                    this.Close();
                    p.NovaIgra();

                }
                else this.Close();
            }
            else if (dealer.PresmetajZbirKarti() == 21)
            {
                pobednikDelac();
            }
            else
            { pobednikAkoDelacIzgubi(); }

        }

        private string presmetajDelacDobivka()
        {
            int i=0;
            foreach (Player p in igraci)
            {
                if (p.igra)
                {
                    i += p.vlog;
                }

            }
            return i.ToString();
        }
        private void pobednikDelac()
        {

            timerIgrac.Stop();
            brojac = 0;
            listBox2.Items.Add("Делач" + "            " + this.presmetajDelacDobivka());
            DialogResult d = MessageBox.Show("Играта е завршена. Делачот доби добивка. Тој има добивка:" + presmetajDelacDobivka() + ". Дали сакате уште една партија?", "Завршена партија.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d == DialogResult.Yes)
            {
                this.Close();
                p.NovaIgra();

            }
            else this.Close();
        }
        private void pobednikAkoDelacIzgubi()
        {
            timerIgrac.Stop();
            brojac = 0;
            int m=dealer.PresmetajZbirKarti();
            Player p1 = null;
            foreach (Player p in igraci)
            {
                if ((p.igra) && (p.presmetajZbir() > m) && (p.presmetajZbir() < 21))
                {
                    m = p.presmetajZbir();
                    p1 = p;
                }
            }
            if ((p1 != null)&&(p1.presmetajZbir()!=dealer.PresmetajZbirKarti()))
            {
                listBox2.Items.Add(p1.ime + "            " + p1.vlog / 2);
                DialogResult d = MessageBox.Show("Делачот изгуби, добитник во играта е играчот" + p1.ime + " со збир на карти: " + p1.presmetajZbir() + ". Дали сакате уште една партија BlackJack?", "Завршена партија.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {

                    this.Close();
                    p.NovaIgra();
                }
                else this.Close();

            }
            else if (p1 == null)
            {
                listBox2.Items.Add("dealer" + "                " + this.presmetajDelacDobivka());
                DialogResult d = MessageBox.Show("Победник на играта е делачот, со збир на карти: " + dealer.PresmetajZbirKarti() + ". Дали сакате уште една партија BlackJack?", "Завршена партија.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {

                    this.Close();
                    p.NovaIgra();
                }
                else this.Close();
            }
            else if ((p1!=null)&&(dealer.PresmetajZbirKarti()==p1.presmetajZbir()))
            {
                listBox2.Items.Add("dealer" + "                " + this.presmetajDelacDobivka());
                DialogResult d = MessageBox.Show("Играта заврши нерешено. Дали сакате уште една партија BlackJack?", "Завршена партија.", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.Yes)
                {

                    this.Close();
                    p.NovaIgra();
                }
                else this.Close();
            }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            if (aktivenIgrac.brojNaKarti < 21)
            {
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.igra = true;
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            if (aktivenIgrac.brojNaKarti < 21)
            {
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.igra = true;

                {
                    if ((igraci.Count > aktivenIgrac.id_igr) && ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null)))
                    {
                        aktivenIgrac = igraci[aktivenIgrac.id_igr];
                        brojac = 0;
                        aktivenIgrac.PostaviAktiven();
                    }

                    else
                    {
                        brojac = 0;
                        otvoriKartaDealer();
                    }
                }
            }
        }
        private void button42_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            if (aktivenIgrac.brojNaKarti < 21)
            {
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            brojac = 0;
            if (aktivenIgrac.brojNaKarti < 21)
            {
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;

                otvoriKartaDealer();
            }
           else if (aktivenIgrac.presmetajZbir()>21)
            {
                izbrisiIgrac(aktivenIgrac); 
                otvoriKartaDealer();
                
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            
                timerIgrac.Stop();
                MessageBox.Show("Влогот ќе се зголеми сега ќе изнесува: " + aktivenIgrac.vlog * 2, "Зголемен влог!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int broja = 0;
                int boja1 = 0;
                aktivenIgrac.vlog = aktivenIgrac.vlog * 2;
                textBox8.Text = aktivenIgrac.ToString();
                aktivenIgrac.doubledown = true;
                while (true)
                {
                    broja = r.Next(2, 14);
                    boja1 = r.Next(1, 4);
                    if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                    {
                        upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                        break;
                    }
                }
                Karta k = new Karta(aktivenIgrac, 3, upotrebeniKarti, boja1, broja);
                aktivenIgrac.dodadiKarta(k);
                String pateka = "karti/";
                pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
                PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + 3];
                box.Visible = true;
                box.Image = Image.FromFile(pateka);
                this.popolniList();
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
                aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
                aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
                label111.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
                
                aktivenIgrac.igra = true;
                if (aktivenIgrac.presmetajZbir() > 21)
                {
                    brojac = 0;
                    izbrisiIgrac(aktivenIgrac);
                }
                else if (aktivenIgrac.presmetajZbir() == 21)
                {
                    pobednikNaIgrata();

                }
                if ((igraci.Count > aktivenIgrac.id_igr) && ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null)))
                {
                    aktivenIgrac = igraci[aktivenIgrac.id_igr];
                    brojac = 0;
                    aktivenIgrac.PostaviAktiven();
                }
                else
                {
                    brojac = 0;
                    otvoriKartaDealer();
                }
            
        }

        private void button14_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Играчот се откажа." + "Неговата добивка изнесува " + aktivenIgrac.vlog / 2);
            textBox8.Text = (aktivenIgrac.vlog / 2).ToString();
            aktivenIgrac.vlog = aktivenIgrac.vlog / 2;
            listBox2.Items.Add(aktivenIgrac.ime + "            " + aktivenIgrac.vlog/2);

            aktivenIgrac.igra = false;

            
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr.ToString() + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString()].Visible = false;
            otkaziIgrac();
            if ((igraci.Count > aktivenIgrac.id_igr) && ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null)))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        public void PocetokPogolemiCifri()
        {
            brojac = 0;
            izbrisiIgrac(aktivenIgrac);
            if (brojigraci > aktivenIgrac.id_igr)
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                otvoriKartaDealer();
            }
        }
        private void button23_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Влогот ќе се зголеми сега ќе изнесува: " + aktivenIgrac.vlog * 2, "Зголемен влог!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            int broja = 0;
            int boja1 = 0;
            aktivenIgrac.vlog = aktivenIgrac.vlog * 2;
            aktivenIgrac.doubledown = true;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, 3, upotrebeniKarti, boja1, broja);
            aktivenIgrac.dodadiKarta(k);
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + 3];
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label222.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            textBox9.Text = aktivenIgrac.vlog.ToString();
            aktivenIgrac.igra = true;
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                brojac = 0;
                izbrisiIgrac(aktivenIgrac);
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();

            }
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Играчот се откажа." + "Неговиот влог изнесува: " + aktivenIgrac.vlog / 2);
            textBox9.Text = (aktivenIgrac.vlog / 2).ToString();
            aktivenIgrac.vlog = aktivenIgrac.vlog / 2;
            listBox2.Items.Add(aktivenIgrac.ime + "            " + aktivenIgrac.vlog/2);

            brojac = 0;
            aktivenIgrac.igra = false;

            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr.ToString() + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString()].Visible = false;
            otkaziIgrac();
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Играчот се откажа." + "Неговиот влог изнесува: " + aktivenIgrac.vlog / 2);
            textBox10.Text = (aktivenIgrac.vlog / 2).ToString();
            aktivenIgrac.vlog = aktivenIgrac.vlog / 2;
            listBox2.Items.Add(aktivenIgrac.ime + "            " + aktivenIgrac.vlog/2);

            brojac = 0;
            aktivenIgrac.igra = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr.ToString() + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString()].Visible = false;
            otkaziIgrac();
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Играчот се откажа." + "Неговата добивка изнесува: " + aktivenIgrac.vlog / 2);
            textBox11.Text = (aktivenIgrac.vlog / 2).ToString();
            aktivenIgrac.vlog = aktivenIgrac.vlog / 2;
            listBox2.Items.Add(aktivenIgrac.ime + "            " + aktivenIgrac.vlog/2);

            brojac = 0;
            aktivenIgrac.igra = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr.ToString() + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString()].Visible = false;
            otkaziIgrac();
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button54_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Играчот се откажа." + "Неговата добивка изнесува " + aktivenIgrac.vlog / 2);
            textBox12.Text = (aktivenIgrac.vlog / 2).ToString();
            listBox2.Items.Add(aktivenIgrac.ime + "            " + aktivenIgrac.vlog/2);

            aktivenIgrac.vlog = aktivenIgrac.vlog / 2;
            brojac = 0;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString() + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr.ToString() + aktivenIgrac.id_igr.ToString()].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr.ToString()].Visible = false;
            otkaziIgrac();
            aktivenIgrac.igra = false;
            
                brojac = 0;
                otvoriKartaDealer();
            
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Влогот ќе се зголеми сега ќе изнесува: " + aktivenIgrac.vlog * 2, "Зголемен влог!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            int broja = 0;
            int boja1 = 0;
            aktivenIgrac.vlog = aktivenIgrac.vlog * 2;
            aktivenIgrac.doubledown = true;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, 3, upotrebeniKarti, boja1, broja);
            aktivenIgrac.dodadiKarta(k);
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + 3];
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label333.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            textBox10.Text = aktivenIgrac.vlog.ToString();
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                brojac = 0;
                izbrisiIgrac(aktivenIgrac);
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();

            }
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Влогот ќе се зголеми сега ќе изнесува: " + aktivenIgrac.vlog * 2, "Зголемен влог!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            int broja = 0;
            int boja1 = 0;
            aktivenIgrac.vlog = aktivenIgrac.vlog * 2;
            aktivenIgrac.doubledown = true;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, 3, upotrebeniKarti, boja1, broja);
            aktivenIgrac.dodadiKarta(k);
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + 3];
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label444.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            textBox11.Text = aktivenIgrac.vlog.ToString();
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                brojac = 0;
                izbrisiIgrac(aktivenIgrac);
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                pobednikNaIgrata();

            }
            if ((igraci.Count > aktivenIgrac.id_igr) && (igraci[aktivenIgrac.id_igr]!=null))
            {
                aktivenIgrac = igraci[aktivenIgrac.id_igr];
                brojac = 0;
                aktivenIgrac.PostaviAktiven();
            }
            else
            {
                brojac = 0;
                otvoriKartaDealer();
            }
        }

        private void button53_Click(object sender, EventArgs e)
        {
            timerIgrac.Stop();
            MessageBox.Show("Влогот ќе се зголеми сега ќе изнесува: " + aktivenIgrac.vlog * 2, "Зголемен влог!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            int broja = 0;
            int boja1 = 0;
            aktivenIgrac.vlog = aktivenIgrac.vlog * 2;
            aktivenIgrac.doubledown = true;
            while (true)
            {
                broja = r.Next(2, 14);
                boja1 = r.Next(1, 4);
                if (!upotrebeniKarti.zafatenaKarta(broja - 1, boja1 - 1))
                {
                    upotrebeniKarti.dodajKarta(broja - 1, boja1 - 1);
                    break;
                }
            }
            Karta k = new Karta(aktivenIgrac, 3, upotrebeniKarti, boja1, broja);
            aktivenIgrac.dodadiKarta(k);
            String pateka = "karti/";
            pateka += broja.ToString() + "-" + boja1.ToString() + ".png";
            PictureBox box = (PictureBox)aktivenIgrac.ikona.Controls["pictureBox" + aktivenIgrac.id_igr + 3];
            box.Visible = true;
            box.Image = Image.FromFile(pateka);
            this.popolniList();
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "1"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "2"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "3"].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr + "4"].Visible = false;
            aktivenIgrac.ikona.Controls["progressBar" + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["label" + aktivenIgrac.id_igr + aktivenIgrac.id_igr].Visible = false;
            aktivenIgrac.ikona.Controls["button" + aktivenIgrac.id_igr].Visible = false;
            label555.Text = "Збир на карти: " + aktivenIgrac.presmetajZbir().ToString();
            textBox12.Text = aktivenIgrac.vlog.ToString();
            if (aktivenIgrac.presmetajZbir() > 21)
            {
                brojac = 0;
                izbrisiIgrac(aktivenIgrac);
            }
            else if (aktivenIgrac.presmetajZbir() == 21)
            {
                
                pobednikNaIgrata();

            }
            
                brojac = 0;
                otvoriKartaDealer();
            
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (timerIgrac.Enabled)
            {
                timerIgrac.Stop();
            }
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
            timerIgrac.Start();
            timer1.Start();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            brojac = 0;
            if (timerIgrac.Enabled)
            {
                timerIgrac.Stop();
            }
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            if (timer2.Enabled)
            {
                timer2.Stop();
            }

            DialogResult d=MessageBox.Show("Дали сакате нова игра?", "Нова Игра", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                this.Close();
                p.NovaIgra();
            }
            else { timerIgrac.Start(); }
        }

        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }

        private void GlavnaIgra_Click(object sender, EventArgs e)
        {

        }

        private void GlavnaIgra_MouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void GlavnaIgra_FormClosing(object sender, FormClosingEventArgs e)
        {
            brojac = 0;
            timerIgrac.Stop();
        }


        
    }
}
