using System.Collections;

namespace DominoKata;

public class DominoSet
{
    private readonly List<Domino> dominos = [];

    public static DominoSet Empty()
    {
        return new DominoSet();
    }

    public DominoSet With(Domino domino)
    {
        dominos.Add(domino);

        return this;
    }

    public bool FormsCircularChain()
    {
        return AreInBetweenDominosLinked() && FirstDominoHalf == LastDominoHalf;
    }

    private bool AreInBetweenDominosLinked()
    {
        for (int i = 0; i < dominos.Count - 1; i++)
        {
            var domino = dominos[i];
            var dominoAtItsRight = dominos[i + 1];

            if (!domino.LinksWith(dominoAtItsRight))
                return false;
        }

        return true;
    }

    private int FirstDominoHalf => dominos.First().Left;
    private int LastDominoHalf => dominos.Last().Right;

    public Result OrderToFormCircularChain()
    {
        if (FormsCircularChain())
            return new Result(Success: true);

        OrderUnorderedSet();

        return new Result(Success: FormsCircularChain());
    }

    private void OrderUnorderedSet()
    {
        var dominosBeforeOrdering = dominos.Count;

        while (!FormsCircularChain())
        {
            var result = asdfasdf();
            if (result.Success.HasValue && !result.Success.Value)
                break;
        }

        var dominosAfterOrdering = dominos.Count;
        if (dominosBeforeOrdering != dominosAfterOrdering)
            throw new ArgumentException();
    }

    private Result asdfasdf()
    {
        Domino outOfOrderDomino = null;
        for (int i = 0; i < dominos.Count - 1; i++)
        {
            var domino = dominos[i];
            var dominoAtItsRight = dominos[i + 1];

            if (!domino.LinksWith(dominoAtItsRight))
                outOfOrderDomino = dominoAtItsRight;
        }

        if (outOfOrderDomino == null)
            return new Result(Success: false);
        
        dominos.Remove(outOfOrderDomino);

        var newFittingPlace = 0;
        for (int i = 0; i < dominos.Count - 1; i++)
        {
            var domino = dominos[i];

            if (outOfOrderDomino.LinksWith(domino))
            {
                newFittingPlace = i;
                break;
            }
        }

        dominos.Insert(newFittingPlace, outOfOrderDomino);

        return new Result(Success: null);
    }
}

public record Result(bool? Success);