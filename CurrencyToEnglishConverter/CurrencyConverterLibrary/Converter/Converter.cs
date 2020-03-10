using System;
using System.Collections.Generic;

namespace CurrencyConverterLibrary.Converter
{
    public class Converter : ICurrencyConverter<string>
    {
        #region unit setup

        private static string[] units = new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] decades = new string[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        const long TRILLION = 1_000_000_000_000;
        const long BILLION = 1_000_000_000;
        const long MILLION = 1_000_000;
        const long THOUSAND = 1_000;
        const long HUNDRED = 100;

        private readonly static IEnumerable<OrderOfMagnitude> ordersOfMagnitude = new List<OrderOfMagnitude>
            {
                new OrderOfMagnitude("trillion", TRILLION),
                new OrderOfMagnitude("billion", BILLION),
                new OrderOfMagnitude("million", MILLION),
                new OrderOfMagnitude("thousand", THOUSAND),
                new OrderOfMagnitude("hundred", HUNDRED)
        };
        #endregion


        public Converter()
        {

        }

        /// <summary>
        /// Check that the amount we're converting is actually a number, and that we start/finish with an arabic numeral rather than other notation.
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public bool ValidateAmount(string amount)
        {
            return decimal.TryParse(amount, out var parsedAmount)//Check that it can be parsed into a decimal format
                && !amount.Trim('-', ' ').StartsWith(".")//Don't allow informally written numbers e.g. ".5"
                && !amount.EndsWith(".")
                && Math.Abs(parsedAmount) < THOUSAND * TRILLION; //Trillion is the highest order of magnitude that we allow. 
        }

        /// <summary>
        /// Convert the numerical amount supplied by the user into its English equivalent
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string ConvertToEnglish(string amount)
        {
            ParseMonetaryUnits(amount, out long dollars, out long? cents);

            string centsInEnglish = string.Empty;
            if (cents.HasValue)
            {
                centsInEnglish = ParseAmountToEnglishPluralised(cents.Value, "cent");

            }

            string polarity = string.Empty;
            var blah = amount.Replace(",", string.Empty);
            if (decimal.Parse(blah) < 0)
            {
                polarity = "negative ";
                dollars *= -1;
            }

            string dollarsInEnglish = ParseAmountToEnglishPluralised(dollars, "dollar");

            string englishEquivalent = dollarsInEnglish;
            if (centsInEnglish != string.Empty)
            {
                englishEquivalent += " and " + centsInEnglish;
            };

            return polarity + englishEquivalent;
        }

        private void ParseMonetaryUnits(string amount, out long dollars, out long? cents)
        {
            //Here we're assuming Australian, US etc currency formats, whereby groupings are separated by commas
            //and the demarcation between dollars and cents with a period.
            string[] dollarsAndCents = amount.Replace(",", string.Empty).Split(".");
            dollars = long.Parse(dollarsAndCents[0]);

            cents = null;

            if (dollarsAndCents.Length > 1)
            {
                string unparsedCents = dollarsAndCents[1];
                unparsedCents = unparsedCents.Length == 1 ? unparsedCents + "0" : unparsedCents.Substring(0, 2);//Ensure that we're dealing with 0-99 cents.
                cents = long.Parse(unparsedCents);
            }
        }

        private string ParseAmountToEnglishPluralised(long amount, string currencyName)
        {
            return amount == 1
                ? "one " + currencyName
                : ParseAmountToEnglish(amount)  + " " + currencyName + "s";
        }

        private string ParseGroupingToEnglish(long amount)
        {
            string amountInEnglish;

            if (amount < 20)
            {
                amountInEnglish = units[amount];
            }
            else
            {
                amountInEnglish = decades[amount / 10];
                if (amount % 10 > 0)
                {
                    amountInEnglish += " " + units[amount % 10];
                }
            }

            return amountInEnglish;
        }


        private string ParseAmountToEnglish(long amount)
        {
            if (amount == 0)
            {
                return "zero";
            }

            string amountInEnglish = string.Empty;

            foreach (var level in ordersOfMagnitude)
            {
                if (amount / level.Amount > 0)
                {
                    amountInEnglish += ParseAmountToEnglish(amount / level.Amount) + $" {level.Name}, ";
                    amount %= level.Amount;
                }
            }
            if (amountInEnglish.EndsWith(", "))
            {
                amountInEnglish = amountInEnglish[0..^2];//strip off the trailing ", "
            }

            if (amount > 0)
            {
                if (amountInEnglish != string.Empty) { amountInEnglish += " and "; }
                if (amount < 20)
                {
                    amountInEnglish += units[amount];
                }
                else
                {
                    amountInEnglish += decades[amount / 10];
                    if (amount % 10 > 0)
                    {
                        amountInEnglish += " " + units[amount % 10];
                    }
                }
            }
            return amountInEnglish;
        }

    }
}
