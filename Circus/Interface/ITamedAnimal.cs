using Circus.Class;

namespace Circus.Interface;

public interface ITamedAnimal
{
    public string Species { get; }
    public string Name { get; }
    List<Trick> GetTricks();
    int GetTrickCount();
    Trick GetOne(int index);
    bool AddTrick(Trick trick);

    void UpdateTrick(Trick oldTrick, Trick newTrick);
    void RemoveTrick(Trick trick);
    void PerformTrick(Trick trick);

    bool IsTrickExist(Trick trick);
}