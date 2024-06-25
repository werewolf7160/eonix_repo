namespace Circus.Class;

public abstract class Trick
{
    #region Property

    public string Name { get; }
    
    #endregion

    #region Contructor

    protected Trick(string name)
    {
        Name = name;
    }

    #endregion

    #region method

    /// <summary>
    /// return a usable display info or the type of trick performed
    /// </summary>
    /// <returns>the adjective in french to the type of trick ("acrobatique","musical",...)</returns>
    public abstract string GetTrickTypeName();


    #endregion
}