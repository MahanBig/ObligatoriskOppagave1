using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Utvekslingsstudent : Student
    {
        public string Land { get; set; }
        public string HjemUniversitet { get; set; }

        public Utvekslingsstudent(string navn, string epost)
            : base(navn,epost)
        {
               
        }
    }
}
