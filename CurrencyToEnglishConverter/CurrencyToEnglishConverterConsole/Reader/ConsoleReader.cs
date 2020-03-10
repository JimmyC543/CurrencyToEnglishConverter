using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyToEnglishConverterConsole.Reader
{
    public class ConsoleReader : IReader
    {
        //For this project we're just dealing with console input and output, but we could feasibly extend it to file input/output.
        public string ReadCurrency()
        {
            return Console.ReadLine();
        }
    }
}
