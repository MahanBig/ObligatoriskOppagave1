using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    public class Student : Bruker
    {
        public List<Kurs> PåmeldteKurs { get; set; } = new List<Kurs>();
        public Dictionary<string, string> Karakterer { get; set; } = new Dictionary<string, string>();

        public Student(int id, string navn, string epost, string brukernavn, string passord)
            : base(id, navn, epost, brukernavn, passord, BrukerRolle.Student)
        {
        }
    }
}