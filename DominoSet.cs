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
            var result = AttemptOrderingOneOutOfPlaceDomino();
            if (result.Success.HasValue && !result.Success.Value)
                break;
        }

        var dominosAfterOrdering = dominos.Count;
        if (dominosBeforeOrdering != dominosAfterOrdering)
            throw new ArgumentException();
    }

    private Result AttemptOrderingOneOutOfPlaceDomino()
    {
        var lastOutOfOrderDomino = LastOutOfOrderDomino();

        if (lastOutOfOrderDomino == null)
            return new Result(Success: false);

        dominos.Remove(lastOutOfOrderDomino);

        dominos.Insert(
            FindFittingPlaceFor(lastOutOfOrderDomino),
            lastOutOfOrderDomino);

        return new Result(Success: null);
    }

    private int FindFittingPlaceFor(Domino lastOutOfOrderDomino)
    {
        for (int i = 0; i < dominos.Count - 1; i++)
        {
            var domino = dominos[i];

            if (lastOutOfOrderDomino.LinksWith(domino))
                return i;
        }

        return 0;
    }

    private Domino? LastOutOfOrderDomino()
    {
        Domino lastOutOfOrder = null;

        for (int i = 0; i < dominos.Count - 1; i++)
        {
            var domino = dominos[i];
            var dominoAtItsRight = dominos[i + 1];

            if (!domino.LinksWith(dominoAtItsRight))
                lastOutOfOrder = dominoAtItsRight;
        }

        return lastOutOfOrder;
    }
}

public record Result(bool? Success);