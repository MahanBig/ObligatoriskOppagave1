using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Ansatt : Bruker
    {
        public int AnsattID { get; set; }
        public string Stilling { get; set; }
        public string Avdeling { get; set; }
        public Ansatt(string navn, string epost)
            : base(navn, epost)
        {
            
        }



    }
}
