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

    private List<Trick> _tricks;

    #endregion

    #region Contructor

    public Monkey(string name, List<Trick> tricks)
    {
        _name = name;
        _tricks = tricks;
    }

    public Monkey(string name)
    {
        _name = name;
        _tricks = new List<Trick>();
    }

    #endregion

    #region Method

    //CRUD

    public bool AddTrick(Trick trick)
    {
        if (_tricks.IndexOf(trick) != -1)
        {
            Console.WriteLine($"Le singe connait déjà le tour {trick.Name}");
            return false;
        }
        _tricks.Add(trick);
        return true;
    }

    public List<Trick> GetTricks()
    {
        return _tricks;
    }

    public Trick GetOne(int index)
    {
        if (index > _tricks.Count)
            throw new IndexOutOfRangeException("GetOne()::No trick at this index");

        return _tricks[index];
    }

    public void UpdateTrick(Trick oldTrick, Trick newTrick)
    {
        var index = _tricks.IndexOf(oldTrick);
        _tricks.RemoveAt(index);
        _tricks.Insert(index, newTrick);
    }

    public void RemoveTrick(Trick trick)
    {
        _tricks.Remove(trick);
    }

    //end CRUD

    public void PerformTrick(Trick trick)
    {
        Console.WriteLine($"Le {Species} {Name} effectue le tour {trick.GetTrickTypeName()} \"{trick.Name}\"");
    }

    public int GetTrickCount()
    {
        return _tricks.Count;
    }

    public bool IsTrickExist(Trick trick)
    {
        return  _tricks.Contains(trick);
    }

    #endregion
}