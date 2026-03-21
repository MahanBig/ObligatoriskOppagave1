using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Unviersitet
    {
        // Lister
        public List<Student> Studenter { get; set; } = new List<Student>();
        public List<Ansatt> Ansatte { get; set; } = new List<Ansatt>(); 
        public List<Kurs> Kurs { get; set; } = new List<Kurs>();
        public List<Bok> Bibliotek { get; set; } = new List<Bok>();
        public List<Lån> LånHistorikk { get; set; } = new List<Lån>();

        public Unviersitet()
        {
            Studenter.Add(new Student(1, "Ola Nordmann", "ola@universitet.no"));
            Studenter.Add(new Student(2, "Kari Nordmann", "kari@universitet.no"));
            Studenter.Add(new Student(3, "Per Post", "per@universitet.no"));

            Ansatte.Add(new Ansatt(4, "Kåre Lærer", "kaare@universitet.no", "Professor", "IT"));
        }


        public Student? GetStudentFraListe(int studentId)
        {
            var student = (from studenten in Studenter
                           where studenten.Id == studentId
                           select studenten).FirstOrDefault();
            return student;
        }
        public Bruker? GetBrukerFraListe(int id)
        {
            Bruker? bruker = (from studenten in Studenter
                              where studenten.Id == id
                              select studenten).FirstOrDefault();

            if (bruker == null)
            {
                bruker = (from ansatt in Ansatte
                          where ansatt.Id == id
                          select ansatt).FirstOrDefault();
            }
            return bruker;
        }

        public Kurs? GetKursFraListe(string kursKode)
        {
            var kurs = (from kursen in Kurs
                        where kursen.KursKode == kursKode
                        select kursen).FirstOrDefault();
            return kurs;
        }

        public Bok? GetBokFraListe(int bokId)
        {
            var bok = (from boken in Bibliotek
                       where boken.Id == bokId
                       select boken).FirstOrDefault();
            return bok;
        }

        // Selve kjøttet på koden
        public void OprettKurs(string kode, string navn, int poeng, int maks)
        {
            Kurs NyttKurs = new Kurs(kode, navn, poeng, maks);
            Kurs.Add(NyttKurs);
            Console.WriteLine($"Kurset {kode} har blitt registrert");
        }

        public void MeldStudentPåKurs(int studentId, string kursKode)
        {
            var student = GetStudentFraListe(studentId);
            var kurs = GetKursFraListe(kursKode);

            if (student == null) { return; }
            if (kurs == null) { return; }

            if (kurs.Deltagere.Contains(student))
            {
                Console.WriteLine("Studenten er allerede påmeldt dette kurset.");
                return;
            }

            if (!kurs.ErPlass())
            {
                Console.WriteLine("Kurset er fullt");
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
            if (student == null) { return; }
            if (kurs == null) { return; }

            kurs.Deltagere.Remove(student);
            student.PåmeldteKurs.Remove(kurs);
        }

        public void VisStudentensKurs(int studentId)
        {
            var student = GetStudentFraListe(studentId);

            if (student == null)
            {
                Console.WriteLine("Fant ikke studenten.");
                return;
            }

            Console.WriteLine($"\nKurs som {student.Navn} er påmeldt:");

            if (student.PåmeldteKurs.Count == 0)
            {
                Console.WriteLine("- Ingen påmeldte kurs.");
            }
            else
            {
                foreach (var kurs in student.PåmeldteKurs)
                {
                    Console.WriteLine($"- {kurs.KursKode}: {kurs.KursNavn} ({kurs.StudiePoeng} studiepoeng)");
                }
            }
        }

        public void RegistrerBok(int id, string tittel, string forfatter, int år, int antall)
        {
            Bok bok = new Bok(id, tittel, forfatter, år, antall);
            Bibliotek.Add(bok);
            Console.WriteLine($"Boken {tittel} har blitt registrert");
        }

        public void VisAktiveLån()
        {
            var aktiveLån = from lån in LånHistorikk
                            where lån.ErAktiv() == true
                            select lån;

            foreach (Lån item in aktiveLån)
            {
                Console.WriteLine($"Aktivt lån fra {item.Låner.Navn} av boken {item.Bok.Tittel}");
            }
        }

        public void VisLåneHistorikk()
        {
            foreach (Lån item in LånHistorikk)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public void LånBok(int brukerId, int bokId)
        {
            var bruker = GetBrukerFraListe(brukerId);
            var bok = GetBokFraListe(bokId);

            if (bruker == null) { Console.WriteLine("Fant ikke bruker."); return; }
            if (bok == null) { Console.WriteLine("Fant ikke bok."); return; }

            int aktiveLånPåBok = (from lånet in LånHistorikk
                                  where lånet.Bok.Id == bok.Id && lånet.ErAktiv() == true
                                  select lånet).Count();

            if (aktiveLånPåBok >= bok.Antall)
            {
                Console.WriteLine("Ingen ledige eksemplarer av denne boken akkurat nå.");
                return;
            }

            Lån nyttLån = new Lån(bruker, bok);
            LånHistorikk.Add(nyttLån);

            Console.WriteLine($"{bruker.Navn} lånte '{bok.Tittel}'.");
        }

        public void ReturnerBok(int bokId, int brukerId)
        {
            Lån? aktivLån = (from lånet in LånHistorikk
                             where lånet.Bok.Id == bokId && lånet.Låner.Id == brukerId && lånet.ErAktiv() == true
                             select lånet).FirstOrDefault();

            if (aktivLån == null)
            {
                Console.WriteLine("Fant ikke et aktivt lån for denne brukeren/boken.");
                return;
            }

            aktivLån.LeverBok();
            Console.WriteLine($"'{aktivLån.Bok.Tittel}' er nå levert tilbake.");
        }
    }
}