using ObligatoriskOppagave1;

namespace ObligatoriskOppgaveTest
{
    [TestClass]
    public class UniversitetTests
    {
        [TestMethod]
        public void LoggInn_MedGyldigInfo_SkalReturnereBruker()
        {
            // Arrange
            Universitet uni = new Universitet();
            string brukernavn = "ola1";
            string passord = "pass123";

            // Act
            Bruker? bruker = uni.LoggInn(brukernavn, passord);

            // Assert
            Assert.IsNotNull(bruker, "Innlogging feilet med gyldig legitimasjon.");
            Assert.AreEqual("Ola Nordmann", bruker.Navn);
        }

        [TestMethod]
        public void MeldStudentPåKurs_GyldigKurs_SkalLeggeTilStudentIKurset()
        {
            // Arrange
            Universitet uni = new Universitet();
            uni.OprettKurs("IT101", "Programmering 1", 10, 30);
            int studentId = 1;

            // Act
            uni.MeldStudentPåKurs(studentId, "IT101");
            Kurs? kurs = uni.GetKursFraListe("IT101");
            Student? student = uni.GetStudentFraListe(studentId);

            // Assert
            Assert.IsTrue(kurs!.Deltagere.Any(s => s.Id == studentId), "Studenten ble ikke lagt til i kurslisten.");
            Assert.IsTrue(student!.PåmeldteKurs.Any(k => k.KursKode == "IT101"), "Kurset ble ikke lagt til i studentens liste.");
        }

        [TestMethod]
        public void RegistrerNyStudent_SjekkOmBrukerenBleLuggetTilIListe_SkalKasteException()
        {
            // Arrange
            Universitet uni = new Universitet();

            // Act
            uni.RegistrerNyStudent("Test Navn", "test@uni.no", "ola32", "passord123");

            // Assert
            Assert.IsTrue(uni.Studenter.Any(s => s.Brukernavn == "ola32"), "Brukeren ble ikke lagt til i listen.");
        }
    }
}