using System.Runtime.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace PeopleManager.Class;

public class Person
{
    #region Property
    [SwaggerIgnore]
    public Guid Id { get; set; }
    [SwaggerSchema(Nullable = false)]
    public string Name{ get; set; }
    [SwaggerSchema(Nullable = false)]
    public string Surname{ get; set; }

    #endregion

    #region Constructor

    public Person()
    {
    }

    public Person(string name, string surname)
    {
        if (Id == Guid.Empty)
        {
            Id = Guid.NewGuid();
        }
        Name = name;
        Surname = surname;
    }

    public Person(Guid id, string name, string surname)
    {
        Id = id;
        Name = name;
        Surname = surname;
    }

    #endregion

}