using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class Dealer
    {
        List<Karta> list;
        usedCards upotrebeniKarti;
        public int broj {set;get;}
        public int boja {set;get;}
        public static int vlog = 1500;
        public int brojkarti { get { return list.Count; } }
        public Dealer(usedCards karti) 
        {
            upotrebeniKarti = karti;
            list = new List<Karta>();
        }
        public void dodajKarta(Karta k)
        {
            list.Add(k);
        }
        public int PresmetajZbirKarti()
        {
            int brojac = 0;
            foreach (Karta k in list)
            {
                brojac += k.broj;
            }
            return brojac;
        }

    }
}
