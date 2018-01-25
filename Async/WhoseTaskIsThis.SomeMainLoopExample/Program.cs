using System;
using System.Threading.Tasks;

namespace WhoseTaskIsThis
{
    class Program
    {
        static void Main(string[] args)
        {
            var loop = new MainLoop();
            loop.Run().GetAwaiter().GetResult();
        }
    }

    class MainLoop
    {
        public async Task Run()
        {
            var delayer = new AsyncDelayer();

            while (true)
            {
                ConsoleHelper.ColorfulMessage(ConsoleColor.Blue, "Main loop");
                await Task.Delay(100);
                delayer.DelayForSecond();
            }
        }
    }

    class AsyncDelayer
    {
        Task _task;

        public async Task DelayForSecond()
        {
            if (_task?.IsCompleted ?? true)
            {
                ConsoleHelper.ColorfulMessage(ConsoleColor.Red, "Create task");
                _task = Task.Delay(1000);

                await _task;
                ConsoleHelper.ColorfulMessage(ConsoleColor.Green, "It's done");
            }
        }
    }

    static class ConsoleHelper
    {
        public static void ColorfulMessage(ConsoleColor color, string message)
        {
            var originColor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = originColor;
        }
    }
}
