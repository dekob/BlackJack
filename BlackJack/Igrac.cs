using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BlackJack
{
    class Igrac
    {
       
        public int Broj
        {
            set;
            get;
        }
        public Igrac(int i = 0)
        {
            Broj = i;
        }
        public void AddOne()
        {
            Broj++;
        }
        public void RemoveOne()
        {
            if (Broj>0)
            Broj--;
        }


    }
}
