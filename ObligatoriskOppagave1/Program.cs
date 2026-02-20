// See https://aka.ms/new-console-template for more information
using ObligatoriskOppagave1;
using ObligatoriskOppagave1.Bruker;

DateTime fraPeriode = DateTime.Now;
DateTime tilPeriode = DateTime.Now;
Utvekslingsstudent harald = new Utvekslingsstudent(1,"harald","Harald@gmail.com","Norge","Lyngveien",fraPeriode,tilPeriode);
Console.WriteLine(harald.Epost);
