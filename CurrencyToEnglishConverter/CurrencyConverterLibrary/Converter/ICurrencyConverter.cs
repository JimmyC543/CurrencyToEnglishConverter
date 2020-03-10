using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterLibrary.Converter
{
    public interface ICurrencyConverter<T>
    {
        public string ConvertToEnglish(T amount);
        public bool ValidateAmount(T amount);
    }
}
