using ObligatoriskOppagave1;

Unviersitet uni = new Unviersitet();
bool kjorer = true;

while (kjorer)
{
    Console.WriteLine("\n=== UNIVERSITETSSYSTEM ===");
    Console.WriteLine("[1] Opprett kurs");
    Console.WriteLine("[2] Meld student til kurs");
    Console.WriteLine("[3] Print kurs og deltagere");
    Console.WriteLine("[4] Søk på kurs");
    Console.WriteLine("[5] Søk på bok");
    Console.WriteLine("[6] Lån bok");
    Console.WriteLine("[7] Returner bok");
    Console.WriteLine("[8] Registrer bok");
    Console.WriteLine("[0] Avslutt");
    Console.WriteLine("Velg en handling: ");

    string? valg = Console.ReadLine();
    Console.WriteLine();

    switch (valg)
    {
        case "1":
            Console.WriteLine("Kurskode: "); 
            string? kode = Console.ReadLine();
            Console.WriteLine("Kursnavn: "); 
            string? navn = Console.ReadLine();

            Console.WriteLine("Studiepoeng (heltall): ");
            int poeng;
            int.TryParse(Console.ReadLine(), out poeng);

            Console.WriteLine("Maks antall plasser: ");
            int maks;
            int.TryParse(Console.ReadLine(), out maks);

            uni.OprettKurs(kode, navn, poeng, maks);
            break;

        case "2":
            Console.WriteLine("Student-ID (tall): ");
            int studentIdKurs;
            int.TryParse(Console.ReadLine(), out studentIdKurs);

            Console.WriteLine("Kurskode: ");
            string? kursKode = Console.ReadLine();

            uni.MeldStudentPåKurs(studentIdKurs, kursKode);
            break;

        case "3":
            foreach (var kurs in uni.Kurs)
            {
                Console.WriteLine($"\nKurs: {kurs.KursKode} - {kurs.KursNavn}");
                if (kurs.Deltagere.Count == 0)
                {
                    Console.WriteLine("  - Ingen deltagere påmeldt enda.");
                }
                else
                {
                    foreach (var deltager in kurs.Deltagere)
                                {
                        Console.WriteLine($"  - {deltager.StudentID}: {deltager.Navn}");
                    }
                }
            }
            break;

        case "4":
            Console.WriteLine("Søk (kode eller navn): ");
            string kursSok = Console.ReadLine().ToLower();

            // Bruker LINQ for å søke i din liste
            var kursTreff = uni.Kurs.Where(k =>
                k.KursKode.ToLower().Contains(kursSok) ||
                k.KursNavn.ToLower().Contains(kursSok));

            foreach (var k in kursTreff)
            {
                Console.WriteLine($"- {k.KursKode}: {k.KursNavn}");
            }
            break;

        case "5":
            Console.WriteLine("Søk (tittel eller forfatter): ");
            string bokSok = Console.ReadLine().ToLower();

            var bokTreff = uni.Bibliotek.Where(b =>
                b.Tittel.ToLower().Contains(bokSok) ||
                b.Forfatter.ToLower().Contains(bokSok));

            foreach (var b in bokTreff)
            {
                Console.WriteLine($"- {b.Id}: {b.Tittel} av {b.Forfatter}");
            }
            break;

        case "6":
            Console.WriteLine("Student-ID (tall): ");
            int laanStudentId;
            int.TryParse(Console.ReadLine(), out laanStudentId);

            Console.WriteLine("Bok-ID (tall): ");
            int laanBokId; 
            int.TryParse(Console.ReadLine(), out laanBokId);

            uni.LånBok(laanStudentId, laanBokId);
            break;

        case "7":
            Console.WriteLine("Bok-ID (tall): ");
            int returBokId;
            int.TryParse(Console.ReadLine(), out returBokId);

            Console.WriteLine("Student-ID (tall): ");
            int returStudentId; 
            int.TryParse(Console.ReadLine(), out returStudentId);

            uni.ReturnerBok(returBokId, returStudentId);
            break;

        case "8":
            Console.WriteLine("Bok-ID (tall): ");
            int nyBokId;
            int.TryParse(Console.ReadLine(), out nyBokId);

            Console.WriteLine("Tittel: ");
            string? tittel = Console.ReadLine();
            Console.WriteLine("Forfatter: ");
            string? forfatter = Console.ReadLine();

            Console.WriteLine("År: ");
            int år; 
            int.TryParse(Console.ReadLine(), out år);

            Console.WriteLine("Antall eksemplarer: ");
            int antall;
            int.TryParse(Console.ReadLine(), out antall);

            uni.RegistrerBok(nyBokId, tittel, forfatter, år, antall);
            break;

        case "0":
            kjorer = false;
            Console.WriteLine("Avslutter programmet...");
            break;

        default:
            Console.WriteLine("Ugyldig valg. Trykk et tall mellom 0 og 8.");
            kjorer = false;
            break;
    }
}