using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Kurs
    {
        public string KursNavn { get; set; }
        public string KursKode { get; set; }
        public int MaksAntallPlasser { get; set; }
        public int StudiePoeng { get; set; }
        public List<Student> Deltagere { get; set; } = new List<Student>();
        public List<Bok> Pensum { get; set; } = new List<Bok>();
        public Ansatt AnsvarligLærer { get; set; }

        public Kurs(string kode, string navn, int poeng, int maks, Ansatt lærer)
        {
            KursKode = kode;
            KursNavn = navn;
            StudiePoeng = poeng;
            MaksAntallPlasser = maks;
            AnsvarligLærer = lærer;
        }

        public bool ErPlass()
        {
            return Deltagere.Count < MaksAntallPlasser;
        }
    }
}