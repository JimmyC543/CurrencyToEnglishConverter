using System;
using System.Collections.Generic;
using System.Text;

namespace CurrencyConverterLibrary
{
    public class OrderOfMagnitude
    {
        public OrderOfMagnitude(string name, long amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; }
        public long Amount { get; }
    }
}
