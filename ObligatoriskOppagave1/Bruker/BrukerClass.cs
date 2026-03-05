using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Bruker
{
    internal abstract class BrukerClass
    {
        public string Navn { get; set; }
        public string Epost { get; set; }
        protected BrukerClass(string navn, string epost)
        {
            this.Navn = navn;
            this.Epost = epost;
        }

    }
}
