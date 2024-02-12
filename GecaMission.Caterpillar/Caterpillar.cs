namespace GecaMission;

public class Caterpillar
{
    public LinkedList<Segment> Segments { get; set; }
    public int Spices { get; set; }
    public enum Direction
    {
        UP = 'U',
        DOWN = 'D',
        LEFT = 'L',
        RIGHT = 'R'
    }

    public Caterpillar()
    {
        Segments = new LinkedList<Segment>(
        [
            new Segment { Position = (-1,-1), Part = Segment.Parts.HEAD },
            new Segment { Position = (-1,-1), Part = Segment.Parts.TAIL }
        ]);
    }

    public void Grow((int X, int Y) position)
    {
        if (Segments.Count < 5)
        {
            Segments.Last.ValueRef.Part = Segment.Parts.BODY;

            Segments.AddLast(new Segment 
            { 
                Position = position,
                Part = Segment.Parts.TAIL
            });
        }
    }

    public void Shrink()
    {
        if (Segments.Count > 2)
        {
            Segments.Remove(Segments.Last);

            Segments.Last.ValueRef.Part = Segment.Parts.TAIL;
        }
    }

    public void Disintegrate()
    {
        throw new Exception("Oops! Caterpillar disintegrated");
    }

    public (int X, int Y) Move(Direction direction)
    {
        var head = Segments.First ?? throw new Exception("Caterpillar needs a head to move");
        var nextSegment = head.Next ?? throw new Exception("Caterpillar needs a tails to drag");
        var nextPosition = head.Value.Position;

        switch (direction)
        {
            case Direction.UP:
                head.ValueRef.Position.Y = head.ValueRef.Position.Y - 1;
                break;
            
            case Direction.DOWN:
                head.ValueRef.Position.Y = head.ValueRef.Position.Y + 1;
                break;
            
            case Direction.RIGHT:
                head.ValueRef.Position.X = head.ValueRef.Position.X + 1;
                break;
            
            case Direction.LEFT:
                head.ValueRef.Position.X = head.ValueRef.Position.X - 1;
                break;
        }

        var xDistance = Math.Abs(head.ValueRef.Position.X - nextSegment.ValueRef.Position.X);
        var yDistance = Math.Abs(head.ValueRef.Position.Y - nextSegment.ValueRef.Position.Y);

        if (xDistance <= 1 && yDistance <= 1)
        {
            return nextSegment.Value.Position;
        }

        return Drag(nextSegment, nextPosition);
    }

    private (int X, int Y) Drag(LinkedListNode<Segment> segment, (int X, int Y) position)
    {
        var nextSegment = segment.Next;
        var nextPosition = segment.Value.Position;
    
        segment.ValueRef.Position = position;

        return nextSegment != null ? Drag(nextSegment, nextPosition) : nextPosition;
    }
}
