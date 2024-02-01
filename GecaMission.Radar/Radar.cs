namespace GecaMission;

public class Radar
{
    public string Area { get; set; } = string.Empty;
    private Caterpillar _Caterpillar { get; set; }
    private List<(int X, int Y)> _Spices = new List<(int X, int Y)>
    {
        (0,0), (5,0), (15,0),
        (1,3),
        (2,4),
        (0,5),(1,5),
        (19,6),
        (10,7),(14,7),
        (14,8),
        (7,14),
        (7,21),(9,21),(14,21),
        (4,26),(18,27),
        (11,27),(17,27),
        (8,29),
    };
    private List<(int X, int Y)> _Boosters = new List<(int X, int Y)>
    {
        (1,9),
        (3,14),
        (19,15),
        (29,16),
        (21,21),
        (4,25),(16,25),
        (26,26),
        (20,28),
        (20,29),
    };
    private List<(int X, int Y)> _Obstacles = new List<(int X, int Y)>
    {
        (26,1),
        (25,2),
        (5,3),
        (28,4),
        (5,9),
        (21,7),
        (10,10),
        (16,16),
        (8,28),
        (18,29),
    };
    private (int X, int Y) _FirstPosition = (-1,-1);
    private (int X, int Y) _LastPosition = (-1,-1);

    public Radar(Caterpillar caterpillar)
    {
        _Caterpillar = caterpillar;
        _FirstPosition = caterpillar.Segments.First.Value.Position;

        Tick();
    }

    public void Tick()
    {
        Area = string.Empty;

        for (int y = 0; y < 30; y++)
        {
            for (int x = 0; x < 30; x++)
            {
                char spot;

                if ((x,y) == _FirstPosition)
                {
                    spot = (char) Spots.FIRST;
                }
                else if (_Spices.Contains((x,y)))
                {
                    spot = (char) Spots.SPICE;
                }
                else if (_Boosters.Contains((x,y)))
                {
                    spot = (char) Spots.BOOSTER;
                }
                else if (_Obstacles.Contains((x,y)))
                {
                    spot = (char) Spots.OBSTACLE;
                }
                else
                {
                    spot = (char) Spots.EMPTY;
                }

                for (var segment = _Caterpillar.Segments.First; segment != null; segment = segment.Next)
                {
                    if (segment.ValueRef.Position.X == x && segment.ValueRef.Position.Y == y)
                    {
                        if (spot == (char) Spots.SPICE)
                        {
                            _Caterpillar.Spices = _Caterpillar.Spices + 1;
                            _Spices.RemoveAt(_Spices.FindIndex(spice => spice == segment.ValueRef.Position));
                        }
                        else if (spot == (char) Spots.BOOSTER)
                        {
                            _Caterpillar.Grow(_LastPosition);
                            _Boosters.RemoveAt(_Boosters.FindIndex(booster => booster == segment.ValueRef.Position));
                        }
                        else if (spot == (char) Spots.OBSTACLE)
                        {
                            _Caterpillar.Disintegrate();
                        }

                        spot = (char) segment.ValueRef.Part;
                        break;
                    }
                }

                _LastPosition = _Caterpillar.Segments.Last.Value.Position;
                Area += spot;
            }

            Area += '\n';
        }
    }
}
