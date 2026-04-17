using ObligatoriskOppagave1;
using System;

Universitet uni = new Universitet();
Bruker? aktivBruker = null;
bool kjørerSystem = true;

while (kjørerSystem)
{
    // Hvis ingen bruker så logg in logikk
    if (aktivBruker == null)
    {
        Console.WriteLine("\n=== VELKOMMEN TIL UNIVERSITETET ===");
        Console.WriteLine("[1] Logg inn");
        Console.WriteLine("[2] Registrer ny student");
        Console.WriteLine("[0] Avslutt");
        Console.Write("Velg handling: ");
        string? startValg = Console.ReadLine();

        if (startValg == "1")
        {
            Console.Write("Brukernavn: ");
            string brukernavn = Console.ReadLine() ?? "";
            Console.Write("Passord: ");
            string passord = Console.ReadLine() ?? "";

            aktivBruker = uni.LoggInn(brukernavn, passord );
            if (aktivBruker == null) { 
                Console.WriteLine(">>> Feil brukernavn eller passord!");
            }
        }
        else if (startValg == "2")
        {
            try
            {
                Console.Write("Fullt navn: "); 
                string navn = Console.ReadLine() ?? "";
                Console.Write("Epost: "); 
                string epost = Console.ReadLine() ?? "";
                Console.Write("Velg brukernavn: "); 
                string brukernavn = Console.ReadLine() ?? "";
                Console.Write("Velg passord: ");
                string passord = Console.ReadLine() ?? "";

                uni.RegistrerNyStudent(navn, epost, brukernavn, passord);
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> FEIL: {ex.Message}");
            }
        }
        else if (startValg == "0")
        {
            kjørerSystem = false;
        }
        continue;
    }
    // bruker er logget inn, vis meny basert på rolle
    Console.WriteLine($"\n--- Logget inn som: {aktivBruker.Navn} ({aktivBruker.Rolle}) ---");

    // vis riktig meny
    if (aktivBruker.Rolle == BrukerRolle.Faglærer)
    {
        VisLærerMeny();
    }
    else if (aktivBruker.Rolle == BrukerRolle.Student)
    {
        VisStudentMeny();
    }
    else if (aktivBruker.Rolle == BrukerRolle.Bibliotekar)
    {
        VisBibliotekarMeny();
    }

    // logg ut valg og håndter valg
    Console.WriteLine("[0] Logg ut");
    Console.Write("Velg handling: ");
    string? menyValg = Console.ReadLine();

    if (menyValg == "0")
    {
        aktivBruker = null;
    }
    else
    {
        HåndterValg(menyValg, aktivBruker);
    }
}

// Meny viserer for hver rolle

void VisLærerMeny()
{
    Console.WriteLine("[1] Opprett kurs");
    Console.WriteLine("[2] Sett karakter");
    Console.WriteLine("[3] Registrer pensum");
    Console.WriteLine("[4] Søk på kurs/bøker");
    Console.WriteLine("[5] Lån/Returner bok");
}

void VisStudentMeny()
{
    Console.WriteLine("[1] Meld på kurs");
    Console.WriteLine("[2] Meld av kurs");
    Console.WriteLine("[3] Se mine kurs og karakterer");
    Console.WriteLine("[4] Søk på bøker");
    Console.WriteLine("[5] Lån/Returner bok");
}

void VisBibliotekarMeny()
{
    Console.WriteLine("[1] Registrer ny bok");
    Console.WriteLine("[2] Se aktive lån");
    Console.WriteLine("[3] Se lånehistorikk");
}

// håndterer valget du tokkt i menyen, basert på hvilken rolle du har
void HåndterValg(string? valg, Bruker bruker)
{
    switch (bruker.Rolle)
    {
        case BrukerRolle.Faglærer:
            if (valg == "1")
            {
                Console.Write("Kurskode: ");
                string kurs = Console.ReadLine() ?? "";
                Console.Write("Navn: ");
                string navn = Console.ReadLine() ?? "";
                uni.OprettKurs(kurs, navn, 10, 30);
            }
            if (valg == "2")
            {
                Console.Write("Kurskode: "); 
                string kurs = Console.ReadLine() ?? "";
                Console.Write("Student-ID: "); 
                int.TryParse(Console.ReadLine(), out int id);
                Console.Write("Karakter (A-F): "); 
                string kar = Console.ReadLine() ?? "";
                uni.SettKarakter(kurs, id, kar);
            }
            if (valg == "3")
            {
                Console.Write("Kurskode: ");
                string kurs = Console.ReadLine() ?? "";
                Console.Write("Bok-ID: "); 
                int.TryParse(Console.ReadLine(), out int id);
                uni.RegistrerPensumTilKurs(kurs, id);
            }
            if (valg == "4") 
            {
                Console.WriteLine("[1] Søk etter kurs");
                Console.WriteLine("[2] Søk etter bok");
                Console.Write("Valg: ");
                string søkValg = Console.ReadLine() ?? "";

                if (søkValg == "1")
                {
                    Console.Write("Skriv inn kurskode: ");
                    string kode = Console.ReadLine() ?? "";
                    var funnetKurs = uni.GetKursFraListe(kode);
                    if (funnetKurs != null)
                        Console.WriteLine($"Fant kurs: {funnetKurs.KursKode} - {funnetKurs.KursNavn} | Plasser: {funnetKurs.Deltagere.Count}/{funnetKurs.MaksAntallPlasser}");
                    else
                        Console.WriteLine("Fant ikke kurset.");
                }
                else if (søkValg == "2")
                {
                    Console.Write("Skriv inn bok-ID: ");
                    if (int.TryParse(Console.ReadLine(), out int bId))
                    {
                        var funnetBok = uni.GetBokFraListe(bId);
                        if (funnetBok != null)
                            Console.WriteLine($"Fant bok: '{funnetBok.Tittel}' av {funnetBok.Forfatter} ({funnetBok.År}). Antall i systemet: {funnetBok.Antall}");
                        else
                            Console.WriteLine("Fant ikke boken.");
                    }
                }
            }
            if (valg == "5")
            {
                HåndterLån(bruker);
            }
            break;

        case BrukerRolle.Student:
            if (valg == "1")
            {
                Console.Write("Kurskode: "); string kurs = Console.ReadLine() ?? "";
                uni.MeldStudentPåKurs(bruker.Id, kurs);
            }
            if (valg == "2")
            {
                Console.Write("Kurskode for avmelding: "); 
                string kurs = Console.ReadLine() ?? "";
                uni.MeldStudentAvKurs(bruker.Id, kurs);
            }
            if (valg == "3") 
            {
                uni.VisStudentensKursOgKarakterer(bruker.Id);
            }
            if (valg == "4")
            {
                Console.Write("Skriv inn bok-ID for å søke: ");
                if (int.TryParse(Console.ReadLine(), out int bId))
                {
                    var funnetBok = uni.GetBokFraListe(bId);
                    if (funnetBok != null)
                        Console.WriteLine($"Fant bok: '{funnetBok.Tittel}' av {funnetBok.Forfatter} ({funnetBok.År}).");
                    else
                        Console.WriteLine("Fant ikke boken.");
                }
            }
            if (valg == "5") 
            {
                HåndterLån(bruker);
            }
            break;

        case BrukerRolle.Bibliotekar:
            if (valg == "1")
            {
                Console.Write("ID: "); 
                int.TryParse(Console.ReadLine(), out int id);
                Console.Write("Tittel: ");
                string tittel = Console.ReadLine() ?? "";
                Console.Write("Forfatter: "); 
                string forfatter = Console.ReadLine() ?? "";
                Console.Write("Utgivelsesår: ");
                int.TryParse(Console.ReadLine(), out int år);
                Console.Write("Antall eksemplarer: ");
                int.TryParse(Console.ReadLine(), out int antall);

                uni.RegistrerBok(id, tittel, forfatter, år, antall);
            }
            if (valg == "2")
            {
                uni.VisAktiveLån();
            }
            if (valg == "3")
            {
                uni.VisLåneHistorikk();
            }
            break;
    }
}

void HåndterLån(Bruker bruker)
{
    Console.WriteLine("[1] Lån [2] Returner");
    string? valg = Console.ReadLine();
    Console.Write("Bok-ID: ");
    int.TryParse(Console.ReadLine(), out int bid);

    if (valg == "1")
    {
        uni.LånBok(bruker.Id, bid);
    }
    else
    {
        uni.ReturnerBok(bid, bruker.Id);
    }
}