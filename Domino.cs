namespace DominoKata;

public class Domino
{
    private readonly int left;
    private readonly int right;

    public Domino(int left, int right)
    {
        this.left = left;
        this.right = right;
    }

    public int Left => left;
    public int Right => right;
}