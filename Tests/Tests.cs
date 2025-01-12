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
    public void InBetweenDominosAreNotLinked_IsNotCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 2))
            .With(new Domino(left: 3, right: 2))
            .With(new Domino(left: 2, right: 1))
            .FormsCircularChain()
            .Should().BeFalse();
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
            .With(new Domino(1, 2)).With(new Domino(2, 1));

        dominoSet.OrderToFormCircularChain();

        dominoSet.FormsCircularChain().Should().BeTrue();
    }

    [Test]
    public void UnorderedDominoSetOf3Dominos_SetIsOrderedToBeCircular()
    {
        var dominoSet = DominoSet.Empty()
            .With(new Domino(1, 2))
            .With(new Domino(2, 1))
            .With(new Domino(2, 2));

        dominoSet.FormsCircularChain().Should().BeFalse();
        dominoSet.OrderToFormCircularChain();
        dominoSet.FormsCircularChain().Should().BeTrue();
    }
    
    [Test]
    public void UnorderedDominoSetOfMoreThan3Dominos_SoSeveralDominosAreOutOfOrder_SetIsOrderedToBeCircular()
    {
        var dominoSet = DominoSet.Empty()
            .With(new Domino(2, 3))
            .With(new Domino(1, 2))
            .With(new Domino(2, 1))
            .With(new Domino(3, 2));

        dominoSet.FormsCircularChain().Should().BeFalse();
        dominoSet.OrderToFormCircularChain();
        dominoSet.FormsCircularChain().Should().BeTrue();
    }

    [Test]
    public void DominosKnowWhenTheyMatchWithOneAnother()
    {
        new Domino(1, 2).LinksWith(new Domino(2, 1)).Should().BeTrue();
        new Domino(1, 2).LinksWith(new Domino(1, 2)).Should().BeFalse();
    }
    
    [Test]
    public void DominoSetThatCanNotBeOrderedToBeCircular_ItIsReturned()
    {
        var dominoSet = DominoSet.Empty()
            .With(new Domino(1, 2))
            .With(new Domino(2, 2));

        dominoSet.OrderToFormCircularChain().Success.Should().BeFalse();
    }
    
    [Test]
    public void DominoSetThatCanBeOrderedToBeCircular_ItIsReturned()
    {
        var dominoSet = DominoSet.Empty()
            .With(new Domino(1, 2))
            .With(new Domino(2, 1))
            .With(new Domino(2, 2));

        dominoSet.OrderToFormCircularChain().Success.Should().BeTrue();
    }
}