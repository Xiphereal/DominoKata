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
        if (dominos.Count == 1)
            return FirstDominoHalf == LastDominoHalf;

        for (int i = 0; i < dominos.Count - 1; i++)
        {
            if (dominos[i].Right != dominos[i + 1].Left)
                return false;
        }

        return true;
    }

    private int FirstDominoHalf => dominos.First().Left;
    private int LastDominoHalf => dominos.Last().Right;

    public void OrderToFormCircularChain()
    {
    }
}