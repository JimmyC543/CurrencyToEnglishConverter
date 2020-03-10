using CurrencyConverterLibrary.Converter;
using CurrencyToEnglishConverterConsole.Reader;
using CurrencyToEnglishConverterConsole.Writer;
using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyToEnglishConverterConsole.App
{
    public class ConsoleApp : IConsoleApp
    {
        private readonly IReader _reader;
        private readonly IWriter _writer;
        private readonly ICurrencyConverter<string> _currencyConverter;

        public ConsoleApp(IReader reader, IWriter writer, ICurrencyConverter<string> currencyConverter)
        {
            _writer = writer ?? throw new ArgumentNullException(nameof(writer));
            _currencyConverter = currencyConverter;
            _reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        public void Execute()
        {
            _writer.WriteLine("Please enter a value between (but not including) -1 quadrillion and +1 quadrillion.");
            while (true)
            {
                string userInput = _reader.ReadCurrency();
                if (_currencyConverter.ValidateAmount(userInput) == false)
                {
                    _writer.WriteLine($"'{userInput}' was not a valid amount. Please try again.");
                    continue;
                }
                _writer.WriteLine(_currencyConverter.ConvertToEnglish(userInput));
            }
        }
    }
}
