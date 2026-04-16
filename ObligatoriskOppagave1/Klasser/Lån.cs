using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Lån
    {
        // new commment
        public Bruker Låner { get; set; }
        public Bok Bok { get; set; }
        public DateTime UtlånsDato { get; set; }
        public DateTime? InnleveringsDato { get; set; }

        public Lån(Bruker låner, Bok bok)
        {
            Låner = låner;
            Bok = bok;
            UtlånsDato = DateTime.Now;
        }

        public bool ErAktiv()
        {
            return InnleveringsDato == null;
        }

        public void LeverBok()
        {
            InnleveringsDato = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Lån: {Bok.Tittel} utlånt til {Låner.Navn} (Aktivt: {ErAktiv()})";
        }
    }
}