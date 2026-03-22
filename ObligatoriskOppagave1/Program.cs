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
    Console.WriteLine("[9] Print studentens kurs");
    Console.WriteLine("[0] Avslutt");
    Console.Write("Velg en handling: ");

    string? valg = Console.ReadLine();
    Console.WriteLine();

    switch (valg)
    {
        case "1":
            Console.Write("Kurskode: ");
            string? kode = Console.ReadLine();

            Console.Write("Kursnavn: ");
            string? navn = Console.ReadLine();

            Console.Write("Studiepoeng (heltall): ");
            bool poengParsed = int.TryParse(Console.ReadLine(), out int poeng);

            Console.Write("Maks antall plasser: ");
            bool maksParsed = int.TryParse(Console.ReadLine(), out int maks);

            if (!string.IsNullOrWhiteSpace(kode) && !string.IsNullOrWhiteSpace(navn) && poengParsed && maksParsed)
            {
                uni.OprettKurs(kode, navn, poeng, maks);
            }
            else
            {
                Console.WriteLine("Feil: Vennligst fyll ut alle felt med gyldige verdier.");
            }
            break;

        case "2":
            Console.Write("Student-ID (tall): ");
            int.TryParse(Console.ReadLine(), out int studentIdKurs);

            Console.Write("Kurskode: ");
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
                        Console.WriteLine($"  - {deltager.Id}: {deltager.Navn}");
                    }
                }
            }
            break;

        case "4":
            Console.Write("Søk (kode eller navn): ");
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
            Console.Write("Søk (tittel eller forfatter): ");
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
            Console.Write("Bruker-ID (student/ansatt, tall): ");
            int.TryParse(Console.ReadLine(), out int lånBrukerId);

            Console.Write("Bok-ID (tall): ");
            int.TryParse(Console.ReadLine(), out int lånBokId);

            uni.LånBok(lånBrukerId, lånBokId);
            break;

        case "7":
            Console.Write("Bok-ID (tall): ");
            int.TryParse(Console.ReadLine(), out int returBokId);

            // Endret fra Student-ID til Bruker-ID her
            Console.Write("Bruker-ID (student/ansatt, tall): ");
            int.TryParse(Console.ReadLine(), out int returBrukerId);

            uni.ReturnerBok(returBokId, returBrukerId);
            break;

        case "8":
            Console.Write("Bok-ID (tall): ");
            int.TryParse(Console.ReadLine() ?? string.Empty, out int nyBokId);

            Console.Write("Tittel: ");
            string? tittel = Console.ReadLine() ?? string.Empty;
            Console.Write("Forfatter: ");
            string? forfatter = Console.ReadLine() ?? string.Empty;

            Console.Write("År: ");
            int.TryParse(Console.ReadLine() ?? string.Empty, out int år);

            Console.Write("Antall eksemplarer: ");
            int.TryParse(Console.ReadLine() ?? string.Empty, out int antall);

            uni.RegistrerBok(nyBokId, tittel, forfatter, år, antall);
            break;

        case "9":
            Console.Write("Student-ID (tall): ");
            if (int.TryParse(Console.ReadLine(), out int studentId))
            {
                uni.VisStudentensKurs(studentId);
            }
            else
            {
                Console.WriteLine("Ugyldig ID.");
            }
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