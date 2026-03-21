using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal abstract class Bruker
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Epost { get; set; }

        protected Bruker(int id, string navn, string epost)
        {
            this.Id = id;
            this.Navn = navn;
            this.Epost = epost;
        }
    }
}