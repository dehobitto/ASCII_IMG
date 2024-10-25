namespace ASCII_CORE;

internal class Program
{
    static void Main()
    {
        var app = new App();
        while (true)
        {
            app.Start();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
    
}

