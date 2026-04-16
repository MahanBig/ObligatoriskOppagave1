namespace ObligatoriskOppagave1
{
    enum BrukerRolle { 
        Student, 
        Faglærer, 
        Bibliotekar
    }
   

    internal abstract class Bruker
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