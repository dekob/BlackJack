using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class Score
    {
        public String player { set; get; }
        public int score { set; get; }
        public Score(String pl, int sco)
        {
            player = pl;
            score = sco;
        }
        public void AddScore(int k)
        {
            score += k;
        }
        public void RemoveScore(int k1)
        {
            if ((score - k1) > 0)
            {
                score -= k1;
            }
        }
    }
}
