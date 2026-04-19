using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1.Interfacer
{
    public interface ILåner
    {
        string Navn { get; set; }
        int Id { get; set; }
    }
}
