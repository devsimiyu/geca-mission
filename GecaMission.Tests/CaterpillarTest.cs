namespace GecaMission.Tests;

public class CaterpillarTest
{
    private readonly Caterpillar _Caterpillar = new Caterpillar();

    [Fact]
    public void Should_Grow_By_Linear_Growth_Mode()
    {
        var count = _Caterpillar.Segments.Count;
        var position = (2,1);
        
        _Caterpillar.Grow(position);

        Assert.Equal(1, _Caterpillar.Segments.Count - count);
        Assert.Equal(position, _Caterpillar.Segments.Last.ValueRef.Position);
    }

    [Fact]
    public void Should_Not_Grow_More_Than_5_Segments()
    {
        _Caterpillar.Segments = new LinkedList<Segment>(
        [
            new Segment { Position = (-1,-1), Part = Segment.Parts.HEAD },
            new Segment { Position = (-1,-1), Part = Segment.Parts.BODY },
            new Segment { Position = (-1,-1), Part = Segment.Parts.BODY },
            new Segment { Position = (-1,-1), Part = Segment.Parts.BODY },
            new Segment { Position = (-1,-1), Part = Segment.Parts.TAIL }
        ]);
        var position = (2,1);
        
        _Caterpillar.Grow(position);

         Assert.Equal(5, _Caterpillar.Segments.Count);
    }
    
    [Fact]
    public void Should_Shrink_By_Linear_Growth_Mode()
    {
        _Caterpillar.Segments = new LinkedList<Segment>(
        [
            new Segment { Position = (-1,-1), Part = Segment.Parts.HEAD },
            new Segment { Position = (2,2), Part = Segment.Parts.BODY },
            new Segment { Position = (-1,-1), Part = Segment.Parts.TAIL }
        ]);
        var count = _Caterpillar.Segments.Count;
        var position = (2,2);
        
        _Caterpillar.Shrink();

        Assert.Equal(1, count - _Caterpillar.Segments.Count);
        Assert.Equal(position, _Caterpillar.Segments.Last.ValueRef.Position);
    }
    
    [Fact]
    public void Should_Not_Shrink_Less_Than_2_Segments()
    {
        _Caterpillar.Shrink();

        Assert.Equal(2, _Caterpillar.Segments.Count);
    }
    
    [Fact]
    public void Should_Disintegrate()
    {
        var ex = Assert.Throws<Exception>(_Caterpillar.Disintegrate);
        
        Assert.Equal("Oops! Caterpillar disintegrated", ex.Message);
    }
    
    [Theory]
    [InlineData(Caterpillar.Direction.UP)]
    [InlineData(Caterpillar.Direction.DOWN)]
    [InlineData(Caterpillar.Direction.LEFT)]
    [InlineData(Caterpillar.Direction.RIGHT)]
    public void Should_Move_In_Direction(Caterpillar.Direction direction)
    {
        var position = _Caterpillar.Segments.Last.Value.Position;
        var trail = _Caterpillar.Move(direction);

        Assert.Equal(position, trail);
    }
}
