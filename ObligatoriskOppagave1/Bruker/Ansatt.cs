using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Bruker
{
    internal class Ansatt : BrukerClass
    {
        public int AnsattID { get; set; }
        public string Stilling { get; set; }
        public string Avdeling { get; set; }
        public Ansatt(int id, string navn, string epost, string stilling, string avdeling)
            : base(navn, epost)
        {
            AnsattID = id;
            Stilling = stilling;
            Avdeling = avdeling;
        }

    }
}
