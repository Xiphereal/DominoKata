using FluentAssertions;
using NUnit.Framework;

namespace DominoKata.Tests;

public class Tests
{
    // Circular chain not possible -> Output it.
    // Order set.
    //  - [1|2] [2|1] [2|2] -> [1|2] [2|2] [2|1]

    [Test]
    public void SingleDoubletDomino_IsCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 1))
            .FormsCircularChain()
            .Should().BeTrue();
    }

    [Test]
    public void SingleNonDoubletDomino_IsNotCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2))
            .FormsCircularChain()
            .Should().BeFalse();
    }

    [Test]
    public void StartingAndEndingWithSameDots_IsConsideredCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2)).With(new Domino(left: 2, right: 1))
            .FormsCircularChain()
            .Should().BeTrue(); 
    }
    
    [Test]
    public void NotStartingAndEndingWithSameDots_IsNotCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2)).With(new Domino(left: 1, right: 2))
            .FormsCircularChain()
            .Should().BeFalse(); 
    }

    [Test]
    public void InBetweenDominosAreAlsoLinked_IsCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2))
            .With(new Domino(left: 2, right: 2))
            .With(new Domino(left: 2, right: 1))
            .FormsCircularChain()
            .Should().BeTrue(); 
    }
    
    [Test]
    public void MoreThanTwoDominoSetNotStartingAndEndingWithSameDots_IsNotCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2))
            .With(new Domino(left: 2, right: 2))
            .With(new Domino(left: 2, right: 2))
            .FormsCircularChain()
            .Should().BeFalse(); 
    }

    [Test]
    public void AttemptOrderingAlreadyCircularDominoSet_KeepsSetCircular()
    {
        var dominoSet = DominoSet.Empty()
            .With(new Domino(1,2)).With(new Domino(2,1));

        dominoSet.OrderToFormCircularChain();

        dominoSet.FormsCircularChain().Should().BeTrue();
    }
}