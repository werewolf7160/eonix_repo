// See https://aka.ms/new-console-template for more information

using Circus.Class;

Console.WriteLine("Bienvenue au Cirque !");
Console.WriteLine();

//initialisation of object

var spectator = new Spectator();
var trickList1 = new List<Trick>
{
    new AcrobaticTrick("Salto arrière"),
    new MusicTrick("Jouer des timbale"),
    new MusicTrick("Chanter l'opéra"),
    new AcrobaticTrick("Double salto arrière")
};

var trickList2 = new List<Trick>()
{
    new MusicTrick("Jouer du tambour"),
    new MusicTrick("Jouer des castagnettes"),
    new AcrobaticTrick("Saut en hauteur"),
    new AcrobaticTrick("Faire le poirier"),
    new AcrobaticTrick("Faire des pompes"),
    new MusicTrick("Chanter l'opéra")
};

var monkey1 = new Monkey("Milo", trickList1);
var monkey2 = new Monkey("Bobby", trickList2);

var tamer1 = new Tamer("Sacha", monkey1);
var tamer2 = new Tamer("Brock", monkey2);


//show time
tamer1.PerformForSpectator(spectator);
tamer2.PerformForSpectator(spectator);

