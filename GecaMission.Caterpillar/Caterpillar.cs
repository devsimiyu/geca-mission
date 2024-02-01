﻿namespace GecaMission;

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
            new Segment { Position = (0,29), Part = Segment.Parts.HEAD },
            new Segment { Position = (-1,29), Part = Segment.Parts.TAIL }
        ]);
    }

    public void Grow((int X, int Y) position)
    {
        if (Segments.Count < 5)
        {
            Segments.AddBefore(Segments.Last, new Segment 
            { 
                Position = position,
                Part = Segment.Parts.BODY
            });
        }
    }

    public void Shrink()
    {
        Segments.RemoveLast();
    }

    public void Move(Direction direction)
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

        Drag(nextSegment, nextPosition);
    }

    private void Drag(LinkedListNode<Segment> segment, (int X, int Y) position)
    {
        var nextSegment = segment.Next;
        var nextPosition = segment.Value.Position;
    
        segment.ValueRef.Position = position;

        if (nextSegment != null)
        {
            Drag(nextSegment, nextPosition);
        }
    }
}
