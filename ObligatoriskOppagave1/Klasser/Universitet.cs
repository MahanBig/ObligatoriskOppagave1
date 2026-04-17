using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Universitet
    {
        public List<Student> Studenter { get; set; } = new List<Student>();
        public List<Ansatt> Ansatte { get; set; } = new List<Ansatt>();
        public List<Kurs> KursListe { get; set; } = new List<Kurs>();
        public List<Bok> Bibliotek { get; set; } = new List<Bok>();
        public List<Lån> LånHistorikk { get; set; } = new List<Lån>();

        public Universitet()
        {
            Studenter.Add(new Student(1, "Ola Nordmann", "ola@uni.no", "ola1", "pass123"));
            Studenter.Add(new Student(2, "Kari Nordmann", "kari@uni.no", "kari1", "pass123"));

            Ansatte.Add(new Ansatt(3, "Kåre Lærer", "kaare@uni.no", "kaare1", "lærerpass", "Professor", "IT", BrukerRolle.Faglærer));
            Ansatte.Add(new Ansatt(4, "Bibliotek Berit", "berit@uni.no", "berit1", "bibpass", "Librarian", "Bibliotek", BrukerRolle.Bibliotekar));
        }

        #region Authentication & Search

        public Student? GetStudentFraListe(int studentId)
        {
            return (from s in Studenter
                    where s.Id == studentId
                    select s).FirstOrDefault();
        }

        public Bruker? GetBrukerFraListe(int id)
        {
            var student = (from s in Studenter
                           where s.Id == id
                           select s).FirstOrDefault();

            if (student != null) return student;

            return (from a in Ansatte
                    where a.Id == id
                    select a).FirstOrDefault();
        }

        public Kurs? GetKursFraListe(string kursKode)
        {
   
            return (from k in KursListe
                    where k.KursKode == kursKode
                    select k).FirstOrDefault();
        }

        public Bok? GetBokFraListe(int bokId)
        {
            return (from b in Bibliotek
                    where b.Id == bokId
                    select b).FirstOrDefault();
        }

        public Bruker? LoggInn(string brukernavn, string passord)
        {
            var student = (from s in Studenter
                           where s.Brukernavn == brukernavn && s.Passord == passord
                           select s).FirstOrDefault();
            if (student != null) return student;

            return Ansatte.FirstOrDefault(a => a.Brukernavn == brukernavn && a.Passord == passord);
        }

        public void RegistrerNyStudent(string navn, string epost, string brukernavn, string passord)
        {
            if (Studenter.Any(s => s.Brukernavn == brukernavn) || Ansatte.Any(a => a.Brukernavn == brukernavn))
            {
                throw new Exception("Brukernavnet er allerede i bruk.");
            }

            int nyId = (Studenter.Count + Ansatte.Count) + 1;
            Studenter.Add(new Student(nyId, navn, epost, brukernavn, passord));
            Console.WriteLine($"Bruker {navn} er registrert som student.");
        }

        #endregion

        #region Course Management (Teacher & Student)

        public void OprettKurs(string kode, string navn, int poeng, int maks)
        {
            Kurs? funnetKurs = GetKursFraListe(kode);
            if (funnetKurs != null )
            {
                Console.WriteLine("Feil: Et kurs med denne koden eller dette navnet eksisterer allerede.");
                return;
            }

            Kurs nyttKurs = new Kurs(kode, navn, poeng, maks);
            KursListe.Add(nyttKurs);
            Console.WriteLine($"Kurset {kode}: {navn} har blitt registrert.");
        }

        public void RegistrerPensumTilKurs(string kursKode, int bokId)
        {
            var kurs = GetKursFraListe(kursKode);
            var bok = GetBokFraListe(bokId);

            if (kurs == null || bok == null)
            {
                Console.WriteLine("Feil: Fant ikke kurset eller boken.");
                return;
            }

            if (!kurs.Pensum.Contains(bok))
            {
                kurs.Pensum.Add(bok);
                Console.WriteLine($"'{bok.Tittel}' er lagt til som pensum for {kurs.KursNavn}.");
            }
        }

        public void SettKarakter(string kursKode, int studentId, string karakter)
        {
            var student = GetStudentFraListe(studentId);
            if (student != null)
            {
                if (student.PåmeldteKurs.Any(k => k.KursKode == kursKode))
                {
                    student.Karakterer[kursKode] = karakter;
                    Console.WriteLine($"Karakter {karakter} satt for {student.Navn} i {kursKode}.");
                }
                else
                {
                    Console.WriteLine("Studenten er ikke påmeldt dette kurset.");
                }
            }
        }

        public void MeldStudentPåKurs(int studentId, string kursKode)
        {
            var student = GetStudentFraListe(studentId);
            var kurs = GetKursFraListe(kursKode);

            if (student == null || kurs == null) { Console.WriteLine("Fant ikke student eller kurs."); return; }

            if (kurs.Deltagere.Any(d => d.Id == studentId))
            {
                Console.WriteLine("Studenten er allerede påmeldt dette kurset.");
                return;
            }

            if (!kurs.ErPlass())
            {
                Console.WriteLine("Kurset er fullt.");
                return;
            }

            kurs.Deltagere.Add(student);
            student.PåmeldteKurs.Add(kurs);
            Console.WriteLine($"{student.Navn} ble meldt på {kurs.KursNavn}.");
        }

        public void MeldStudentAvKurs(int studentId, string kursKode)
        {
            var kurs = GetKursFraListe(kursKode);
            var student = GetStudentFraListe(studentId);

            if (student != null && kurs != null)
            {
                kurs.Deltagere.Remove(student);
                student.PåmeldteKurs.Remove(kurs);
                Console.WriteLine("Avmelding bekreftet.");
            }
        }

        public void VisStudentensKursOgKarakterer(int studentId)
        {
            var student = GetStudentFraListe(studentId);
            if (student == null) return;

            Console.WriteLine($"\nKurs og karakterer for {student.Navn}:");
            foreach (var kurs in student.PåmeldteKurs)
            {
                string karakter = student.Karakterer.ContainsKey(kurs.KursKode) ? student.Karakterer[kurs.KursKode] : "Ingen karakter satt";
                Console.WriteLine($"- {kurs.KursKode}: {kurs.KursNavn} | Karakter: {karakter}");
            }
        }

        #endregion

        #region Library Management (Librarian, Teacher & Student)

        public void RegistrerBok(int id, string tittel, string forfatter, int år, int antall)
        {
            Bok? funnetBok = GetBokFraListe(id);
            if (funnetBok != null)
            {
                Console.WriteLine("Feil: En bok med denne ID-en finnes allerede.");
                return;
            }
            Bok bok = new Bok(id, tittel, forfatter, år, antall);
            Bibliotek.Add(bok);
            Console.WriteLine($"Boken '{tittel}' har blitt registrert i biblioteket.");
        }

        public void LånBok(int brukerId, int bokId)
        {
            var bruker = GetBrukerFraListe(brukerId);
            var bok = GetBokFraListe(bokId);

            if (bruker == null || bok == null) { Console.WriteLine("Fant ikke bruker eller bok."); return; }

            int aktiveLånPåBok = LånHistorikk.Count(l => l.Bok.Id == bok.Id && l.ErAktiv());

            if (aktiveLånPåBok >= bok.Antall)
            {
                Console.WriteLine("Ingen ledige eksemplarer tilgjengelig.");
                return;
            }

            LånHistorikk.Add(new Lån(bruker, bok));
            Console.WriteLine($"{bruker.Navn} lånte '{bok.Tittel}'.");
        }

        public void ReturnerBok(int bokId, int brukerId)
        {
            var aktivLån = LånHistorikk.FirstOrDefault(l => l.Bok.Id == bokId && l.Låner.Id == brukerId && l.ErAktiv());

            if (aktivLån == null)
            {
                Console.WriteLine("Fant ikke et aktivt lån for denne brukeren på denne boken.");
                return;
            }

            aktivLån.LeverBok();
            Console.WriteLine($"'{aktivLån.Bok.Tittel}' er levert tilbake av {aktivLån.Låner.Navn}.");
        }

        public void VisAktiveLån()
        {
            var aktive = LånHistorikk.Where(l => l.ErAktiv());
            Console.WriteLine("\n=== Aktive lån i systemet ===");
            foreach (var lån in aktive)
            {
                Console.WriteLine($"- {lån.Bok.Tittel} (Lånt av: {lån.Låner.Navn})");
            }
        }

        public void VisLåneHistorikk()
        {
            Console.WriteLine("\n=== Full lånehistorikk ===");
            foreach (var lån in LånHistorikk)
            {
                Console.WriteLine(lån.ToString());
            }
        }

        #endregion
    }
}