using ObligatoriskOppagave1.Bruker;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Bibliotek
{
    internal class Lån
    {
        public Student Låner {  get; set; }
        public Bok Bok { get; set; }
        public DateTime UtlånsDato { get; set; }
        public DateTime? InnleveringsDato { get; set; }

        public Lån(Student låner, Bok bok)
        {
            Låner = låner;
            Bok = bok;
            UtlånsDato = DateTime.Now;
        }
        public bool ErAktiv()
        {
            return InnleveringsDato == null;
        }

    }
}
