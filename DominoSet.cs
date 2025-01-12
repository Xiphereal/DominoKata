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
        
        var outOfOrderDomino = dominos.Last();
        dominos.Remove(outOfOrderDomino);
        dominos.Insert(1, outOfOrderDomino);
        
        var dominosAfterOrdering = dominos.Count;
        if (dominosBeforeOrdering != dominosAfterOrdering)
            throw new ArgumentException();
    }
}

public record Result(bool Success);
