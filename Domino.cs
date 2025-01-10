namespace DominoKata;

public class Domino
{
    private readonly int left;
    private readonly int right;

    public Domino(int left, int right)
    {
        if (left is <= 0 or > 6)
            throw new ArgumentException();
        
        if (right is <= 0 or > 6)
            throw new ArgumentException();
        
        this.left = left;
        this.right = right;
    }

    public int Left => left;
    public int Right => right;
}