using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;

namespace BlackJack
{
    class Player
    {
        List<Karta> karti;
        usedCards upotrebe;
        public Panel ikona { set; get; }
        public bool aktiven {set;get;}
        public bool otkazi {set;get;}
        public bool doubledown { set; get; }
        public bool igra { set; get; }
        public bool pobednik { set; get; }
        public int id_igr { set; get; }
        public String ime{set;get;}
        public int brojNaKarti { get { return karti.Count; } }
        public Random r{set;get;}
        public int vlog { set; get; }
        public Image slika { set; get; }
        public GlavnaIgra gl { set; get; }
        public Player(usedCards used, Panel panela, int id, GlavnaIgra g)
        {
            upotrebe = used;
            ikona = panela;
            doubledown = false;
            aktiven = false;
            otkazi = false;
            pobednik = false;
            r = new Random();
            id_igr = id;
            igra = true;
            gl = g;
            karti = new List<Karta>();
            Karta k = new Karta(this, 1, used, r);
            Karta k1 = new Karta(this, 2, used, r);
            slika = Properties.Resources.player;
           
            k.generirajKarta();
            k.generirajPozicija();
            k.zemiSlika();
            k1.generirajKarta();
            k1.generirajPozicija();
            k1.zemiSlika();
            
            karti.Add(k);
            karti.Add(k1);
        }
        public Point zemiGolemina()
        {
            return ikona.Location;
        }
        public Size zemiLokacija()
        {
            return ikona.Size;
        }
        public void PostaviAktiven()
        {
            if (presmetajZbir() < 21)
            {
                ikona.Controls["button" + id_igr + "1"].Visible = true;
                ikona.Controls["button" + id_igr + "2"].Visible = true;
                ikona.Controls["button" + id_igr + "3"].Visible = true;
                ikona.Controls["button" + id_igr + "4"].Visible = true;
                if ((karti.Count > 2))
                {
                    ikona.Controls["button" + id_igr + "3"].Enabled = false;
                    ikona.Controls["button" + id_igr + "4"].Enabled = false;
                }
                ikona.Controls["progressBar" + id_igr].Visible = true;
                ikona.Controls["label" + id_igr + id_igr].Visible = true;
                ikona.Controls["button" + id_igr].Visible = true;
                Timer t = gl.timerIgrac;
                t.Start();
            }
            else if (presmetajZbir() == 21)
            {
                gl.pobednikNaIgrata();
                this.pobednik = true;
            }
            else {
                
                gl.PocetokPogolemiCifri();
            }
           

            

        }
        
        public int presmetajZbir()
        {
            int i = 0;
            foreach (Karta k in karti)
            {
                i += k.broj;
                }
            return i;
        }
        public void dodadiKarta(Karta k)
        {
            if (karti.Count<5)
            karti.Add(k);
        }
        public void postaviSlika()
        {

            ikona.Controls["pictureBox"+this.id_igr.ToString()+karti[0].pored.ToString()].BackgroundImage = karti[0].zemiSlika();
            ikona.Controls["pictureBox"+this.id_igr.ToString()+karti[1].pored.ToString()].BackgroundImage = karti[1].zemiSlika();
            ikona.Refresh();
        }
    }
}
