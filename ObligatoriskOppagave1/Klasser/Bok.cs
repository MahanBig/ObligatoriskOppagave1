using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Bok
    {
        public int Id { get; set; }
        public string Tittel { get; set; }
        public string Forfatter { get; set; }
        public int År { get; set; }
        public int Antall { get; set; }

        public Bok(int id, string tittel, string forfatter, int år, int antall)
        {
            Id = id;
            Tittel = tittel;
            Forfatter = forfatter;
            Antall = antall;
            
        }
    }
}
