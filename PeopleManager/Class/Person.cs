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

    public Person(string name, string surname)
    {
        Name = name;
        Surname = surname;
    }

    #endregion

    #region Method


    #endregion

    #region Inherited

    #endregion


}