using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Kurs
    {
        public string KursNavn { get; set; }
        public string KursKode {  get; set; }
        public int MaksAntallPlasser { get; set; }
        public int StudiePoeng { get; set; }
        public List<Student> Deltagere { get; set; } = new List<Student>();

        public Kurs(string kode, string navn, int poeng, int maks)
        {
            KursNavn = navn;
            KursKode = kode;
            MaksAntallPlasser = maks;
            StudiePoeng = poeng;
        }
        public bool ErPlass()
        {
            return Deltagere.Count < MaksAntallPlasser;
        }
    }
}
