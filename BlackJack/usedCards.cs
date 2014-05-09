using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class usedCards
    {
        private bool[,] upotrebeniKarti;

        public usedCards()
        {
            upotrebeniKarti=new bool[4, 14];
        }

        public void dodajKarta(int broj, int boja)
        {
            upotrebeniKarti[boja, broj] = true;
        }
        public bool zafatenaKarta(int broj, int boja)
        {
            return upotrebeniKarti[boja, broj];
        }
    }
}
