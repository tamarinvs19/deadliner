namespace Deadliner;

public static class Deadliner
{
    public static void Main()
    {
        MainContainer.BuildContainer();
        Console.WriteLine($"Hello World! Current datetime: {MainContainer.TimeProvider().Now()}");
    }
}