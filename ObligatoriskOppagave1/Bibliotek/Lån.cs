using ObligatoriskOppagave1.Bruker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Bibliotek
{
    internal class Lån
    {
        public BrukerClass Låner {  get; set; }
        public Bok Bok { get; set; }
        public DateTime UtlånsDato { get; set; }

        public Lån(BrukerClass låner, Bok bok)
        {
            Låner = låner;
            Bok = bok;
            UtlånsDato = DateTime.Now;
        }

    }
}
