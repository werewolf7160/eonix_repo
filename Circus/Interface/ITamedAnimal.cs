using Circus.Class;

namespace Circus.Interface;

public interface ITamedAnimal
{
    public string Species { get; }
    public string Name { get; }
    List<Trick> Tricks { get; set; }
    void AddTrick(Trick trick);
    Trick GetOne(int index);
    void UpdateTrick(Trick oldTrick, Trick newTrick);
    void RemoveTrick(Trick trick);
    void PerformTrick(Trick trick);
}