using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Ansatt : Bruker
    {
        public string Stilling { get; set; }
        public string Avdeling { get; set; }

        public Ansatt(int id, string navn, string epost, string brukernavn, string passord, string stilling, string avdeling, BrukerRolle rolle)
            : base(id, navn, epost, brukernavn, passord, rolle)
        {
            Stilling = stilling;
            Avdeling = avdeling;
        }
    }
}