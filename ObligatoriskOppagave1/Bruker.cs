using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal abstract class Bruker
    {
        public string Navn {  get; set; }
        public string Epost { get; set; }
        protected Bruker(string navn, string epost)
        {
            this.Navn = navn;
            this.Epost = epost;
        }

    }
}
