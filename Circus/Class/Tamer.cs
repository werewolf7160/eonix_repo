using Circus.Interface;

namespace Circus.Class;

public class Tamer
{

    #region Variable
    private string _name;
    private ITamedAnimal _tamedAnimal;

    #endregion

    #region Contructor

    public Tamer(string name, ITamedAnimal tamedAnimal)
    {
        _name = name;
        _tamedAnimal = tamedAnimal;
    }

    public void PerformForSpectator(Spectator spectator)
    {
        var str = $"Le dresseur {_name} demande à {_tamedAnimal.Name} de montrer tout ses tours !";
        Console.WriteLine(str);
        Console.WriteLine(new string('-',str.Length));
        Console.WriteLine();

        foreach (var trick in _tamedAnimal.GetTricks())
        {
            _tamedAnimal.PerformTrick(trick);
            switch (trick)
            {
                case AcrobaticTrick acrobatic:
                    spectator.ClapHand(acrobatic,_tamedAnimal);
                    break;
                case MusicTrick music:
                    spectator.Whistle(music, _tamedAnimal);
                    break;
                default:
                    spectator.DontKnowWhatToDo();
                    break;
            }
            Console.WriteLine();
        }
    }

    #endregion
}