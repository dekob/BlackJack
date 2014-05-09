using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJack
{
    class Scores
    {
        public List<Score> list;
        public Scores(int i=0)
        {
            list = new List<Score>();
        }
        public void AddOne(Score score)
        {
            list.Add(score);
        }
        public void RemoveOne(Score score)
        {
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].player == score.player)
                    {
                        list.RemoveAt(i);
                    }
                }

            }
        }
    }
}
