using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Student : Bruker
    {
        public List<Kurs> PåmeldteKurs { get; set; } = new List<Kurs>();

        public int StudentID { get => Id; set => Id = value; }

        public Student(int id, string navn, string epost)
            : base(id, navn, epost)
        {
        }
    }
}