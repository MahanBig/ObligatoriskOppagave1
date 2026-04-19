using ObligatoriskOppagave1.Interfacer;

namespace ObligatoriskOppagave1
{
    public enum BrukerRolle { 
        Student, 
        Faglærer, 
        Bibliotekar 
    }
   

    public abstract class Bruker : ILåner
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public string Epost { get; set; }
        public string Brukernavn { get; set; }
        public string Passord { get; set; }
        public BrukerRolle Rolle { get; set; }

        protected Bruker(int id, string navn, string epost, string brukernavn, string passord, BrukerRolle rolle)
        {
            Id = id;
            Navn = navn;
            Epost = epost;
            Brukernavn = brukernavn;
            Passord = passord;
            Rolle = rolle;
        }
    }
}