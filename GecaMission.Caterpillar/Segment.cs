namespace GecaMission;

public record class Segment
{
    public (int X, int Y) Position;
    public Parts Part;

    public enum Parts
    {
        HEAD = 'H',
        BODY = '0',
        TAIL = 'T'
    }
}
