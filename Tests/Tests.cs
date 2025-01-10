using DominoKata;
using FluentAssertions;
using NUnit.Framework;

namespace DominoKata.Tests;

public class Tests
{
    // Circular chain not possible -> Output it.
    // Ask if set is in circle
    //  - [1|1] is
    //  - [1|2] is not
    //  - [1|2] [2|1] is
    //  - [1|2] [1|2] is not
    // Order set.
    //  - [1|2] [2|1] -> [1|2] [2|1]
    //  - [1|2] [1|2] -> [1|2] [2|1]
    
    [Test]
    public void SingleDoubletDomino_IsCircular()
    {
        DominoSet.Empty()
            .With(new Domino(left: 1, right: 1))
            .FormsCircularChain()
            .Should().BeTrue();
    }
}