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

    public bool LinksWith(Domino dominoAtMyRight)
    {
        return this.right == dominoAtMyRight.left;
    }

    public bool LinksConsideringTurningWith(Domino dominoAtMyRight)
    {
        return this.left == dominoAtMyRight.left
               || this.left == dominoAtMyRight.right
               || this.right == dominoAtMyRight.left
               || this.right == dominoAtMyRight.right;
    }
}