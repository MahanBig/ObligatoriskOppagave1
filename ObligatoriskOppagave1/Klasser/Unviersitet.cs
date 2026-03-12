using System;
using System.Collections.Generic;
using System.Text;

namespace ObligatoriskOppagave1
{
    internal class Unviersitet
    {
        // Lister
        public List<Student> Studenter { get; set; } = new List<Student>();
        public List<Kurs> Kurs { get; set; } = new List<Kurs>();
        public List<Bok> Bibliotek { get; set; } = new List<Bok>();
        public List<Lån> LånHistorikk { get; set; } = new List<Lån>();
        public Unviersitet() {
            // litt test data
            Studenter.Add(new Student(1, "Ola Nordmann", "ola@universitet.no"));
            Studenter.Add(new Student(2, "Kari Nordmann", "kari@universitet.no"));
            Studenter.Add(new Student(3, "Per Post", "per@universitet.no"));
        }

        // Get fra lister
        public Student? GetStudentFraListe(int studentId)
        {
            var student = (from studenten in Studenter
                           where studenten.StudentID == studentId
                           select studenten).FirstOrDefault();
            return student;
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
            Kurs NyttKurs = new Kurs(kode,navn,poeng,maks);
            Kurs.Add(NyttKurs);
            Console.WriteLine($"kurset {kode} har blitt regisrert");
        }
        public void MeldStudentPåKurs(int studentId, string kursKode)
        {
            var student = GetStudentFraListe(studentId);
            var kurs = GetKursFraListe(kursKode);
            if (student == null) { return; }
            if (kurs == null) { return; }

            if (!kurs.ErPlass())
            {
                Console.WriteLine("Kurset er fullt");
                return;
            }
            kurs.Deltagere.Add(student);
            Console.WriteLine($"{student.Navn} ble meldt på {kurs.KursNavn}.");
        }
        public void MeldStudentAvKurs(int studentId, string kursKode)
        {
            var kurs = GetKursFraListe(kursKode);
            var student = GetStudentFraListe(studentId);
            if (student == null) { return; }
            if (kurs == null) { return; }

            kurs.Deltagere.Remove(student);
        }

        public void RegistrerBok(int id, string tittel, string forfatter, int år, int antall)
        {
            Bok bok = new Bok(id, tittel, forfatter, år, antall);
            Bibliotek.Add(bok);
            Console.WriteLine($"boken {tittel} har blitt registrert");
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

        public void LånBok(int studentId, int bokId)
        {
            var student = GetStudentFraListe(studentId);
            var bok = GetBokFraListe(bokId);

            if (student == null) { return; }
            if (bok == null) { return; }

            int aktiveLånPåBok = (from lånet in LånHistorikk
                                  where lånet.Bok.Id == bok.Id && lånet.ErAktiv() == false
                                  select lånet).Count();
            if (aktiveLånPåBok >= bok.Antall)
            {
                Console.WriteLine("Ingen ledige eksemplarer av denne boken akkurat nå.");
                return;
            }

            Lån nyttLån = new Lån(student, bok);
            LånHistorikk.Add(nyttLån);

            Console.WriteLine($"{student.Navn} lånte '{bok.Tittel}'.");
        }
        public void ReturnerBok(int bokId, int studentId)
        {
            Lån? aktivLån = (from lånet in LånHistorikk
                           where lånet.Bok.Id == bokId && lånet.Låner.StudentID == studentId
                           select lånet).FirstOrDefault();
            if (aktivLån == null) { return; }
            aktivLån.LeverBok();
            Console.WriteLine($"'{aktivLån.Bok.Tittel}' er nå levert tilbake.");
        }
    }
}
