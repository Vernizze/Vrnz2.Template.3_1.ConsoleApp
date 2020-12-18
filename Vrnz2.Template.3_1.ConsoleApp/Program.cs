using MediatR;
using System;
using System.Threading;

namespace Vrnz2.Template._3_1.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"[STARTING] At: {DateTime.UtcNow:yyyy-HH-ddTHH:mm:ss}");

            Startup.ConfigureServices();

            var mediatr = Startup.GetService<IMediator>();

            Startup.GetStartableServices()
                .ForEach(m => mediatr.Publish(Activator.CreateInstance(m)));

            Thread.Sleep(Timeout.Infinite);
        }
    }
}
