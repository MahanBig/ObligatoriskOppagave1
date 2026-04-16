using ObligatoriskOppagave1;
using System;

Unviersitet uni = new Unviersitet();
Bruker? aktivBruker = null;
bool kjørerSystem = true;

while (kjørerSystem)
{
    // --- 1. LOGIN / REGISTRATION FLOW ---
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
            string bn = Console.ReadLine() ?? "";
            Console.Write("Passord: ");
            string ps = Console.ReadLine() ?? "";

            aktivBruker = uni.LoggInn(bn, ps);
            if (aktivBruker == null) Console.WriteLine(">>> Feil brukernavn eller passord!");
        }
        else if (startValg == "2")
        {
            try
            {
                Console.Write("Fullt navn: "); string navn = Console.ReadLine() ?? "";
                Console.Write("Epost: "); string epost = Console.ReadLine() ?? "";
                Console.Write("Velg brukernavn: "); string bn = Console.ReadLine() ?? "";
                Console.Write("Velg passord: "); string ps = Console.ReadLine() ?? "";

                uni.RegistrerNyStudent(navn, epost, bn, ps);
            }
            catch (Exception ex)
            {
                Console.WriteLine($">>> FEIL: {ex.Message}");
            }
        }
        else if (startValg == "0") kjørerSystem = false;
        continue;
    }

    // --- 2. MAIN SYSTEM (LOGGED IN) ---
    Console.WriteLine($"\n--- Logget inn som: {aktivBruker.Navn} ({aktivBruker.Rolle}) ---");

    // Switch menu based on Role
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

    // Logout option available for all
    Console.WriteLine("[0] Logg ut");
    Console.Write("Velg handling: ");
    string? menyValg = Console.ReadLine();

    if (menyValg == "0") aktivBruker = null;
    else HåndterValg(menyValg, uni, aktivBruker);
}

// --- MENU HANDLERS ---

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

void HåndterValg(string? valg, Unviersitet uni, Bruker bruker)
{
    switch (bruker.Rolle)
    {
        case BrukerRolle.Faglærer:
            if (valg == "1")
            {
                Console.Write("Kurskode: ");
                string k = Console.ReadLine() ?? "";
                Console.Write("Navn: ");
                string n = Console.ReadLine() ?? "";
                uni.OprettKurs(k, n, 10, 30, (Ansatt)bruker);
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
            if (valg == "5")
            {
                HåndterLån(uni, bruker);
            }
            break;

        case BrukerRolle.Student:
            if (valg == "1")
            {
                Console.Write("Kurskode: "); string kurs = Console.ReadLine() ?? "";
                uni.MeldStudentPåKurs(bruker.Id, kurs);
            }
            if (valg == "3") uni.VisStudentensKursOgKarakterer(bruker.Id);
            if (valg == "5") HåndterLån(uni, bruker);
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
                uni.RegistrerBok(id, tittel, forfatter, 2024, 5);
            }
            if (valg == "2")
            {
                uni.VisAktiveLån();
            }
            break;
    }
}

void HåndterLån(Unviersitet uni, Bruker bruker)
{
    Console.WriteLine("[1] Lån [2] Returner");
    string? valg = Console.ReadLine();
    Console.Write("Bok-ID: ");
    int.TryParse(Console.ReadLine(), out int bid);

    if (valg == "1") uni.LånBok(bruker.Id, bid);
    else uni.ReturnerBok(bid, bruker.Id);
}