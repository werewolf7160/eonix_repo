using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Class;
using PeopleManager.Controllers;
using Xunit;

namespace PeopleManager.PeopleManager.Tests;

public class UnitTest1
{

    private List<Person> _people = new List<Person>()
    {
        new(id:new Guid("81fae03d-84a0-4b5f-8096-5351516d7269"), name:"Sébastien", surname:"Renard"),
        new(id:new Guid("bdbd7075-ef9c-420f-bb39-ee6b6cc86483"), name:"Alice", surname:"Dupont"),
        new(id:new Guid("d43a3695-192f-43d0-b473-db5580b01167"), name:"Sylvie", surname:"Dupond"),
        new(id:new Guid("e275b7bf-014d-48a8-ad44-3988ba32f32e"), name:"luCas", surname:"Dutilleul")
    };

    private PeopleDataContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<PeopleDataContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;

        var context = new PeopleDataContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }

    private async Task PopulateInMemoryDataContext(PeopleDataContext dataContext)
    {
        dataContext.AddRange(_people);

        await dataContext.SaveChangesAsync();
    }

    // api/Peoples
    [Fact]
    public async Task GetAllPeople()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: null, filterSurname: null);

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Equal(_people.Count, returnValue.Count);
    }

    // api/Peoples {filterName}
    [Fact]
    public async Task GetAllPeopleFilteredByNameSameCase()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: "Sébastien", filterSurname: null);

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Single(returnValue);
    }

    // api/Peoples {filterName}
    [Fact]
    public async Task GetAllPeopleFilteredByNameDifferentCase()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: "sébastien", filterSurname: null);

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Single(returnValue);
    }

    // api/Peoples {filterName}
    [Fact]
    public async Task GetAllPeopleFilteredByPartialName()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: "asti", filterSurname: null);

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Single(returnValue);
    }

    // api/Peoples {filterSurname}
    [Fact]
    public async Task GetAllPeopleFilteredBySurnameSameCase()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: null, filterSurname: "Dupont");

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Single(returnValue);
    }

    // api/Peoples {filterSurname}
    [Fact]
    public async Task GetAllPeopleFilteredBySurnameDifferentCase()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        var controller = new PeopleController(context);

        var result = controller.GetPeople(filterName: null, filterSurname: "dupont");

        var returnValue = Assert.IsType<List<Person>>(result.Value);
        Assert.Single(returnValue);
    }

    // api/People
    [Fact]
    public async Task GetOnePeople()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var result = await controller.GetPerson(id:_people.First().Id);

        var returnValue = Assert.IsType<Person>(result.Value);
        Assert.Equal(_people.First(), returnValue);
    }

    [Fact]
    public async Task UpdatePerson()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var personSrc = _people.First();
        personSrc.Name = "Gérard";


        var result = await controller.PutPerson(id: personSrc.Id,person: personSrc);

        Assert.IsType<NoContentResult>(result);

        var dbProd = await context.People.FindAsync(personSrc.Id);
        Assert.NotNull(dbProd);
        Assert.Equal(personSrc.Name, dbProd.Name);
    }

    [Fact]
    public async Task UpdateNotExistingPerson()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var mockPerson = new Person(Guid.NewGuid(), "test", "test");

        var result = await controller.PutPerson(id: mockPerson.Id, person: mockPerson);

        Assert.IsType<NotFoundResult>(result);

    }

    [Fact]
    public async Task UpdatePersonEmptyName()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var personSrc = _people.First();
        personSrc.Name = "";


        var result = await controller.PutPerson(id: personSrc.Id, person: personSrc);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePersonNullName()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var personSrc = _people.First();
        personSrc.Name = null;


        var result = await controller.PutPerson(id: personSrc.Id, person: personSrc);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePersonEmptySurname()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var personSrc = _people.First();
        personSrc.Surname = "";


        var result = await controller.PutPerson(id: personSrc.Id, person: personSrc);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    [Fact]
    public async Task UpdatePersonNullSurname()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var personSrc = _people.First();
        personSrc.Surname = null;


        var result = await controller.PutPerson(id: personSrc.Id, person: personSrc);

        Assert.IsType<BadRequestObjectResult>(result);
    }


    [Fact]
    public async Task PostPerson()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var newPerson = new Person(id: Guid.NewGuid(),name: "Bobby", surname: "Melton" );


        var result = await controller.PostPerson(person: newPerson);

        var actionResult = Assert.IsType<ActionResult<Person>>(result);
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(actionResult.Result);
        var returnValue = Assert.IsType<Person>(createdAtActionResult.Value);

        Assert.Equal(newPerson.Name, returnValue.Name);
        Assert.Equal(newPerson.Surname, returnValue.Surname);

        var productInDb = await context.People.FindAsync(returnValue.Id);
        Assert.NotNull(productInDb);
        Assert.Equal(newPerson.Name, returnValue.Name);
        Assert.Equal(newPerson.Surname, returnValue.Surname);
    }

    [Fact]
    public async Task PostPersonEmptyName()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var newPerson = new Person(id: Guid.NewGuid(), name: "", surname: "Melton");


        var result = await controller.PostPerson(person: newPerson);

        var actionResult = Assert.IsType<ActionResult<Person>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task PostPersonNullName()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var newPerson = new Person(id: Guid.NewGuid(), name: null, surname: "Melton");


        var result = await controller.PostPerson(person: newPerson);

        var actionResult = Assert.IsType<ActionResult<Person>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task PostPersonEmptySurname()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var newPerson = new Person(id: Guid.NewGuid(), name: "", surname: "Melton");


        var result = await controller.PostPerson(person: newPerson);

        var actionResult = Assert.IsType<ActionResult<Person>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task PostPersonNullSurname()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var newPerson = new Person(id: Guid.NewGuid(), name: null, surname: "Melton");


        var result = await controller.PostPerson(person: newPerson);

        var actionResult = Assert.IsType<ActionResult<Person>>(result);
        Assert.IsType<BadRequestObjectResult>(actionResult.Result);
    }

    [Fact]
    public async Task DeletePerson()
    {
        //set clean object
        var context = GetInMemoryDbContext();

        //populate mock data
        await PopulateInMemoryDataContext(context);

        if (_people == null || _people.Count <= 0)
        {
            throw new Exception("_people can't be null or empty to launch GetOnePeople() test");
        }

        var controller = new PeopleController(context);

        var deletedPerson = _people.First();

        var result = await controller.DeletePerson(id: deletedPerson.Id);

        Assert.IsType<NoContentResult>(result);

        var person = await context.People.FindAsync(deletedPerson.Id);
        Assert.Null(person);
    }


}