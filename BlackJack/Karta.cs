using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace BlackJack
{
    class Karta
    {
        public int broj { set; get; }
        public int boja { set; get; }
        public int brojk { set; get; }
        Point position;
        public int pored { set; get; }
        usedCards upotrebeniKarti;
        Random re;
        Image slika;
        Player igrac;
        Dealer de;
        public Karta(Player ig, int red, usedCards Karti, Random r)
        {
            
            igrac = ig;
            pored=red;
            re = r;
            upotrebeniKarti = Karti;
        }
        public Karta(Player ig, int red, usedCards karti, int boj, int broj1)
        {
            igrac = ig;
            pored = red;
            upotrebeniKarti = karti;
            boja = boj;
            if ((broj1 == 12) || (broj1 == 13) || (broj1 == 14))
            {
                broj = 10;
            }
            else broj = broj1;
            brojk = broj1;
        }
        public Karta(Dealer d, int bo, int br)
        {
            de = d;
            boja = bo;
            if ((br == 12) || (br == 13) || (br == 14))
            {
                broj = 10;
            }
            else broj = br;
            brojk = br;
        }
        public Image zemiSlika()
        {
            String pateka = "karti/";
            pateka += brojk.ToString() + "-" + boja.ToString()+".png";
            return Image.FromFile(pateka);
            
        }
        public void generirajKarta()
        {
            int broj1;
            int broj2;
            while (true)
            {
                broj1 = re.Next(1, 4);
                broj2 = re.Next(2, 14);
                if (!upotrebeniKarti.zafatenaKarta(broj2-1, broj1-1))
                {
                    upotrebeniKarti.dodajKarta(broj2-1, broj1-1);
                    this.brojk = broj2;
                    if ((broj2 == 12)||(broj2==13)||(broj2==14))
                    { broj2 = 10; }
                    this.broj = broj2;
                    this.boja = broj1;
                    break;
                }
            }
            
        }
     
        public void generirajPozicija()
        {
            position = new Point(igrac.zemiGolemina().X - (10 * pored), igrac.zemiGolemina().Y);
        }
        public Point pozicija()
        {
            return position;
        }


    }
}
