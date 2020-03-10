using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyToEnglishConverterConsole.Writer
{
    public class ConsoleWriter : IWriter
    {
        //For this project we're just dealing with console input and output, but we could feasibly extend it to file input/output.
        public void WriteLine(string output)
        {
            Console.WriteLine(output);
        }
    }
}
