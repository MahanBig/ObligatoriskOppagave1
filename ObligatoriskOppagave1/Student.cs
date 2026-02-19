using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Student : Bruker
    {
        public int StudentID { get; set; }
        public Student(string navn, string epost)
            : base(navn,epost)
        {

        }

    }
}
