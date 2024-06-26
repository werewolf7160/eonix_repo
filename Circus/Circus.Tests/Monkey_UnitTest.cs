using Circus.Class;
using Xunit;

namespace Circus.Tests;

public class Monkey_UnitTest
{
    private List<Trick> _tricks = new()
    {
        new MusicTrick("Jouer du tambour"),
        new MusicTrick("Jouer des castagnettes"),
        new AcrobaticTrick("Saut en hauteur"),
        new AcrobaticTrick("Faire le poirier"),
        new AcrobaticTrick("Faire des pompes"),
        new MusicTrick("Chanter l'opéra")
    };

    [Fact]
    public void GetAllTrick()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        Assert.Equal(_tricks.Count, monkey.GetTricks().Count);
    }

    [Fact]
    public void GetOneTrick()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);
        
        Assert.Equal(_tricks[0], monkey.GetOne(0));
    }

    [Fact]
    public void GetTrickCount()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        Assert.Equal(_tricks.Count, monkey.GetTrickCount());
    }

    [Fact]
    public void AddTrick()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        var trick = new MusicTrick("Jouer de la flute à bec");
        var expected = _tricks.Count + 1;
        var res = monkey.AddTrick(trick);
        Assert.True(res);
        Assert.Equal(expected, monkey.GetTrickCount());

        var tricks = monkey.GetTricks();
        Assert.Contains(trick, tricks);
    }

    [Fact]
    public void UpdateTrick()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        var newTrick = new MusicTrick("Jouer de la flute à bec");

        var oldTrick = monkey.GetOne(0);
       
        monkey.UpdateTrick(oldTrick, newTrick);

       
        Assert.Equal(newTrick, monkey.GetOne(0));
    }

    [Fact]
    public void IsTrickExistExistingValue()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        var trickTest = _tricks[0];

        Assert.True(monkey.IsTrickExist(trickTest));
    }

    [Fact]
    public void IsTrickExistNonExistingValue()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        var trickTest = new AcrobaticTrick("new acrobatic trick");

        Assert.False(monkey.IsTrickExist(trickTest));
    }

    [Fact]
    public void RemoveTrick()
    {
        var monkey = new Monkey(name: "name", tricks: _tricks);

        var oldTrick = monkey.GetOne(0);

        monkey.RemoveTrick(oldTrick);

        Assert.False(monkey.IsTrickExist(oldTrick));

        //double check

        var tricks = monkey.GetTricks();

        Assert.DoesNotContain(oldTrick, tricks);
    }


}