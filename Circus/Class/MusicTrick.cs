namespace Circus.Class;

public class MusicTrick : Trick
{
   
    #region Contructor

    public MusicTrick(string name) : base(name)
    {
    }

    #endregion

    public override string GetTrickTypeName()
    {
        return "musical";
    }
}