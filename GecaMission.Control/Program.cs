using GecaMission;

var caterpillar = new Caterpillar();
var radar = new Radar(caterpillar);

using StreamWriter log = File.AppendText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "logs.txt"));

Caterpillar.Direction? direction = null;
int? steps = null;

while (true)
{
    Console.WriteLine("Enter command to move caterpillar direction \nby number of steps using the below commands \n");
    Console.WriteLine("e.g., u2 to move up by 2 steps. (Direction and steps should be separated by a space) \n");
    Console.WriteLine("Exit the application by entering X \n");
    Console.WriteLine("u - Up \nd - Down \nl - Left \nr - Right \nx - Exit \n");
    Console.WriteLine(radar.Area + "\n");
    Console.WriteLine("Spices collected - {0} \n", caterpillar.Spices);

    if (direction.HasValue && steps.HasValue)
    {
        Console.WriteLine("You've moved {0} by {1} step(s) \n", direction.Value, steps.Value);
    }

    var prompt = Console.ReadLine();

    if (prompt == null || prompt.Contains('x'))
    {
        log.WriteLine("PROGRAM EXIT");
        break;
    }

    var command = prompt.ToUpper().ToCharArray();
    direction = command[0] switch
    {
        (char) Caterpillar.Direction.UP => Caterpillar.Direction.UP,
        (char) Caterpillar.Direction.DOWN => Caterpillar.Direction.DOWN,
        (char) Caterpillar.Direction.LEFT => Caterpillar.Direction.LEFT,
        (char) Caterpillar.Direction.RIGHT => Caterpillar.Direction.RIGHT,
        _ => throw new Exception("Oops! Command not recognized")
    };
    steps = int.Parse(command[1].ToString());

    log.WriteLine($"{direction} {steps} STEPS");

    for (int step = 0; step < steps; step++)
    {
        radar.Trail = caterpillar.Move(direction.Value);

        radar.Tick();
    }

    Console.Clear();
}
