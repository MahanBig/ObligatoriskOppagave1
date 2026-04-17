using ObligatoriskOppagave1.Interfacer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Lån
    {
        public ILåner Låner { get; set; }
        public Bok Bok { get; set; }
        public DateTime UtlånsDato { get; set; }
        public DateTime? InnleveringsDato { get; set; }

        public Lån(ILåner låner, Bok bok)
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