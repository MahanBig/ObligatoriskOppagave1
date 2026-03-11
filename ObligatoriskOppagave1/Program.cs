using ObligatoriskOppagave1;

Unviersitet uni = new Unviersitet();
bool kjører = true;

while (kjører)
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
            bool poengParsed = int.TryParse(Console.ReadLine(), out int poeng);

            Console.WriteLine("Maks antall plasser: ");
            bool maksParsed = int.TryParse(Console.ReadLine(), out int maks);

            if (!string.IsNullOrWhiteSpace(kode) && !string.IsNullOrWhiteSpace(navn) && poengParsed && maksParsed)
            {
                uni.OprettKurs(kode, navn, poeng, maks);
                Console.WriteLine("Kurs opprettet!");
            }
            else
            {
                Console.WriteLine("Feil: Vennligst fyll ut alle felt med gyldige verdier.");
            }

            break;

        case "2":
            Console.WriteLine("Student-ID (tall): ");
            int studentIdKurs;
            int.TryParse(Console.ReadLine(), out studentIdKurs);

            Console.WriteLine("Kurskode: ");
            string? kursKode = Console.ReadLine() ?? string.Empty;

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
            string kursSok = Console.ReadLine()?.ToLower() ?? string.Empty;

            var kursTreff = from kurs in uni.Kurs
                            where kurs.KursKode.ToLower().Contains(kursSok) ||
                                  kurs.KursNavn.ToLower().Contains(kursSok)
                            select kurs;

            foreach (var kurs in kursTreff)
            {
                Console.WriteLine($"- {kurs.KursKode}: {kurs.KursNavn}");
            }
            break;

        case "5":
            Console.WriteLine("Søk (tittel eller forfatter): ");
            string bokSok = Console.ReadLine()?.ToLower() ?? string.Empty;

            var bokTreff = from bok in uni.Bibliotek
                           where bok.Tittel.ToLower().Contains(bokSok) ||
                                 bok.Forfatter.ToLower().Contains(bokSok)
                           select bok;

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
            int.TryParse(Console.ReadLine() ?? string.Empty, out nyBokId);

            Console.WriteLine("Tittel: ");
            string? tittel = Console.ReadLine() ?? string.Empty;
            Console.WriteLine("Forfatter: ");
            string? forfatter = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("År: ");
            int år; 
            int.TryParse(Console.ReadLine() ?? string.Empty, out år);

            Console.WriteLine("Antall eksemplarer: ");
            int antall;
            int.TryParse(Console.ReadLine() ?? string.Empty, out antall);

            uni.RegistrerBok(nyBokId, tittel, forfatter, år, antall);
            break;

        case "0":
            kjører = false;
            Console.WriteLine("Avslutter programmet...");
            break;

        default:
            Console.WriteLine("Ugyldig valg. Trykk et tall mellom 0 og 8.");
            break;
    }
}