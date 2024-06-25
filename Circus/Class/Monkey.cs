using Circus.Interface;

namespace Circus.Class;

public class Monkey : ITamedAnimal
{
    #region Variable

    private string _name;

    #endregion

    #region Property

    public string Species => "singe";
    public string Name => _name;

    public List<Trick> Tricks { get; set; }

    #endregion

    #region Contructor

    public Monkey(string name, List<Trick> tricks)
    {
        _name = name;
        Tricks = tricks;
    }

    public Monkey(string name)
    {
        _name = name;
        Tricks = new List<Trick>();
    }

    #endregion

    #region Method

    //CRUD

    public void AddTrick(Trick trick)
    {
        if (Tricks.IndexOf(trick) == -1)
        {
            Console.WriteLine($"Le singe connait déjà le tour {trick.Name}");
            return;
        }
        Tricks.Add(trick);
    }

    public Trick GetOne(int index)
    {
        if (index < Tricks.Count)
            throw new IndexOutOfRangeException("GetOne()::No trick at this index");

        return Tricks[index];
    }

    public void UpdateTrick(Trick oldTrick, Trick newTrick)
    {
        var index = Tricks.IndexOf(oldTrick);
        Tricks.RemoveAt(index);
        Tricks.Insert(index, newTrick);
    }

    public void RemoveTrick(Trick trick)
    {
        Tricks.Remove(trick);
    }

    //end CRUD

    public void PerformTrick(Trick trick)
    {
        Console.WriteLine($"Le {Species} {Name} effectue le tour {trick.GetTrickTypeName()} \"{trick.Name}\"");
    }


    #endregion
}