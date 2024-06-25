namespace Circus.Class;

public class AcrobaticTrick : Trick
{
    #region Constructor

    public AcrobaticTrick(string name) : base(name)
    {
    }

    #endregion

    public override string GetTrickTypeName()
    {
        return "acrobatique";
    }
}