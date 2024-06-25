using Circus.Interface;

namespace Circus.Class;

public class Spectator
{
    #region Method

    public void ClapHand(AcrobaticTrick trick, ITamedAnimal tamedAnimal)
    {
        Console.WriteLine($"\tSpectateur applaudit pendant le tour '{trick.Name}' de {tamedAnimal.Name}");
    }

    public void Whistle(MusicTrick trick, ITamedAnimal tamedAnimal)
    {
        Console.WriteLine($"\tSpectateur siffle pendant le tour '{trick.Name}' de {tamedAnimal.Name}");
    }

    public void DontKnowWhatToDo()
    {
        Console.WriteLine($"\tSpectateur est perplexe et ne sait pas quoi faire");
    }

    #endregion
}