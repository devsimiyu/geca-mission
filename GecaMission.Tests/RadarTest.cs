namespace GecaMission.Tests;

public class RadarTest
{
    private readonly Caterpillar _Caterpillar;
    private readonly Radar _Radar;

    public RadarTest()
    {
        _Caterpillar = new Caterpillar();
        _Radar = new Radar(_Caterpillar);
    }

    [Fact]
    public void Should_Display_Area()
    {
        Assert.NotEmpty(_Radar.Area);
    }

    [Theory]
    [InlineData(Spots.EMPTY)]
    [InlineData(Spots.BOOSTER)]
    [InlineData(Spots.SPICE)]
    [InlineData(Spots.OBSTACLE)]
    public void Should_Display_Spots_On_Start(Spots spot)
    {
        Assert.Contains((char) spot, _Radar.Area);
    }
    
    [Fact]
    public void Should_Display_Caterpillar_Head_On_Start()
    {
        Assert.Contains("H", _Radar.Area);
    }
    
    [Theory]
    [InlineData(Caterpillar.Direction.UP)]
    [InlineData(Caterpillar.Direction.RIGHT)]
    [InlineData(Caterpillar.Direction.DOWN)]
    [InlineData(Caterpillar.Direction.LEFT)]
    public void Should_Show_Caterpillar_Movement(Caterpillar.Direction direction)
    {
        _Caterpillar.Segments = new LinkedList<Segment>(
        [
            new Segment { Position = (11,22), Part = Segment.Parts.HEAD },
            new Segment { Position = (10,21), Part = Segment.Parts.TAIL }
        ]);

        _Caterpillar.Move(direction);
        _Radar.Tick();

        Assert.Contains("H", _Radar.Area);
        Assert.Contains("T", _Radar.Area);
    }

    [Theory]
    [InlineData(Caterpillar.Direction.UP, 11, 28, 2)]
    [InlineData(Caterpillar.Direction.RIGHT, 3, 26, 3)]
    public void Should_Allow_Caterpillar_To_Collect_Spices(Caterpillar.Direction direction, int x, int y, int spices)
    {
        _Caterpillar.Segments.First.ValueRef.Position = (x,y);
        _Caterpillar.Spices = spices;

        _Caterpillar.Move(direction);
        _Radar.Tick();

        Assert.Equal(spices + 1, _Caterpillar.Spices);
    }
}
