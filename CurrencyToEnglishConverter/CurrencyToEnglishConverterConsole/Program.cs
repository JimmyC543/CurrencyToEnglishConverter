using CurrencyConverterLibrary.Converter;
using CurrencyToEnglishConverterConsole.App;
using CurrencyToEnglishConverterConsole.Reader;
using CurrencyToEnglishConverterConsole.Writer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace CurrencyToEnglishConverterConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            OtherShit();



            //Setup the Dependency Injection for the project.
            var serviceProvider = new ServiceCollection()
                .AddSingleton<IConsoleApp, ConsoleApp>()
                .AddSingleton<ICurrencyConverter<string>, Converter>()
                .AddSingleton<IWriter, ConsoleWriter>()
                .AddSingleton<IReader, ConsoleReader>()
                .BuildServiceProvider();

            var app = serviceProvider.GetService<IConsoleApp>();
            app.Execute();
        }
        private static void OtherShit()
        {

            int[] arr = new int[2];
            arr[1] = 10;
            Object o = arr;
            int[] arr1 = (int[])o;
            arr1[1] = 100;
            Console.WriteLine(arr[1]);
            ((int[])o)[1] = 1000;
            Console.WriteLine(arr[1]);
        }
    }
}
