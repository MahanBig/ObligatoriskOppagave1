using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Utvekslingsstudent : Student
    {

        public string Land { get; set; }
        public string HjemUniversitet { get; set; }
        public DateTime FraDato { get; set; }
        public DateTime TilDato { get; set; }

        public Utvekslingsstudent(int id, string navn, string epost, string land, string hjem, DateTime fra, DateTime til)
            : base(id, navn, epost)
        {
            Land = land;
            HjemUniversitet = hjem;
            FraDato = fra;
            TilDato = til;
        }
    }
}
