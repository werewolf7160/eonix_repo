using Microsoft.EntityFrameworkCore;

namespace PeopleManager.Class;

public class PeopleDataContext : DbContext
{

    #region Variable

    #endregion

    #region Property

    public DbSet<Person> Persons { get; set; }

    #endregion

    #region Constructor

    public PeopleDataContext(DbContextOptions<PeopleDataContext> options) : base(options)
    {

    }

    #endregion

    #region Method

    #endregion

    #region Inherited

    #endregion


}