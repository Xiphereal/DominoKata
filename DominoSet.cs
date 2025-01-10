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
        return FirstDominoHalf == LastDominoHalf;
    }

    private int FirstDominoHalf => dominos.First().Left;
    private int LastDominoHalf => dominos.Last().Right;
}