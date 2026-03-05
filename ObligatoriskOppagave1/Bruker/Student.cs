using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Bruker
{
    internal class Student : BrukerClass
    {
        public int StudentID { get; set; }
        public Student(int id, string navn, string epost)
            : base(navn,epost)
        {
            StudentID = id;
        }

    }
}
