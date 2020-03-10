using CurrencyConverterLibrary;
using CurrencyConverterLibrary.Converter;
using System;
using Xunit;

namespace CurrencyConverterLibraryTests
{
    public class ConverterTests
    {
        [Theory]
        //Test $0-$99.99
        [InlineData("1.14", "one dollar and fourteen cents")]
        [InlineData("20", "twenty dollars")]
        [InlineData("20.00", "twenty dollars and zero cents")]
        [InlineData("37", "thirty seven dollars")]
        [InlineData("74", "seventy four dollars")]
        [InlineData("45.67", "forty five dollars and sixty seven cents")]
        [InlineData("0.01", "zero dollars and one cent")]
        [InlineData("0.2", "zero dollars and twenty cents")]
        [InlineData("0", "zero dollars")]
        [InlineData("0.0", "zero dollars and zero cents")]
        [InlineData("99.99", "ninety nine dollars and ninety nine cents")]
        [InlineData("99.9999", "ninety nine dollars and ninety nine cents")]
        [InlineData("14.55", "fourteen dollars and fifty five cents")]

        //Test 100-999.99
        [InlineData("100", "one hundred dollars")]
        [InlineData("100.00", "one hundred dollars and zero cents")]
        [InlineData("999.99", "nine hundred and ninety nine dollars and ninety nine cents")]

        //Test 1,000-999,999.99
        [InlineData("1000", "one thousand dollars")]
        [InlineData("1000.00", "one thousand dollars and zero cents")]
        [InlineData("9999.99", "nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]
        [InlineData("500,000", "five hundred thousand dollars")]
        [InlineData("999,999.99", "nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]

        //Test 1,000,000 - 999,999,999.99
        [InlineData("1000000", "one million dollars")]
        [InlineData("1,000,000.00", "one million dollars and zero cents")]
        [InlineData("9,999,999.99", "nine million, nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]
        [InlineData("500,000,000", "five hundred million dollars")]
        [InlineData("999,999,999.99", "nine hundred and ninety nine million, nine hundred and ninety nine thousand" +
            ", nine hundred and ninety nine dollars and ninety nine cents")]

        //Test 1,000,000,000 - 999,999,999,999.99 (1 billion - 999.99... billion)
        [InlineData("1000000000", "one billion dollars")]
        [InlineData("1,000,000,000.00", "one billion dollars and zero cents")]
        [InlineData("9,999,999,999.99", "nine billion, nine hundred and ninety nine million" +
            ", nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]
        [InlineData("500,000,000,000", "five hundred billion dollars")]
        [InlineData("999,999,999,999.99", "nine hundred and ninety nine billion, nine hundred and ninety nine million" +
            ", nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]

        //Test 1,000,000,000,000 - 999,999,999,999,999.99 (1 trillion - 999.99... trillion)
        [InlineData("1000000000000", "one trillion dollars")]
        [InlineData("1,000,000,000,000.00", "one trillion dollars and zero cents")]
        [InlineData("9,999,999,999,999.99", "nine trillion, nine hundred and ninety nine billion, nine hundred and ninety nine million" +
            ", nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]
        [InlineData("500,000,000,000,000", "five hundred trillion dollars")]
        [InlineData("999,999,999,999,999.99", "nine hundred and ninety nine trillion, nine hundred and ninety nine billion" +
            ", nine hundred and ninety nine million, nine hundred and ninety nine thousand" +
            ", nine hundred and ninety nine dollars and ninety nine cents")]

        //Test negative values
        [InlineData("-1.14", "negative one dollar and fourteen cents")]
        [InlineData("-20", "negative twenty dollars")]
        [InlineData("-20.00", "negative twenty dollars and zero cents")]
        [InlineData("-37", "negative thirty seven dollars")]
        [InlineData("-74", "negative seventy four dollars")]
        [InlineData("-45.67", "negative forty five dollars and sixty seven cents")]
        [InlineData("-0.01", "negative zero dollars and one cent")]
        [InlineData("-0.2", "negative zero dollars and twenty cents")]
        [InlineData("-0", "zero dollars")]
        [InlineData("-0.0", "zero dollars and zero cents")]

        [InlineData("-1000000000000", "negative one trillion dollars")]
        [InlineData("-1,000,000,000,000.00", "negative one trillion dollars and zero cents")]
        [InlineData("-9,999,999,999,999.99", "negative nine trillion, nine hundred and ninety nine billion, nine hundred and ninety nine million" +
            ", nine hundred and ninety nine thousand, nine hundred and ninety nine dollars and ninety nine cents")]
        [InlineData("-500,000,000,000,000", "negative five hundred trillion dollars")]
        [InlineData("-999,999,999,999,999.99", "negative nine hundred and ninety nine trillion, nine hundred and ninety nine billion" +
            ", nine hundred and ninety nine million, nine hundred and ninety nine thousand" +
            ", nine hundred and ninety nine dollars and ninety nine cents")]
        public void ConvertToEnglish_ShouldProduceCorrectResult(string amount, string expected)
        {
            //Arrange
            Converter converter = new Converter();
            
            //Act
            var result = converter.ConvertToEnglish(amount);

            //Assert
            Assert.Equal(expected, result);
        }

        
        [Theory]
        [InlineData("1.14")]
        [InlineData("0.5")]
        [InlineData("0")]
        [InlineData("-999,999,999,999,999.99")]
        [InlineData("999,999,999,999,999.99")]
        public void ValidateAmount_ShouldPass_ValidAmounts(string amount)
        {
            //Arrange
            Converter converter = new Converter();

            //Act
            var result = converter.ValidateAmount(amount);

            //Assert
            Assert.True(result);
        }
        [Theory]
        [InlineData("0.")]
        [InlineData(".5")]
        [InlineData("-.5")]
        [InlineData("100.")]
        [InlineData("$100")]
        [InlineData("--100")]
        [InlineData("1000000000000000")]
        [InlineData("-1000000000000000")]
        public void ValidateAmount_ShouldFail_InvalidAmounts(string amount)
        {
            //Arrange
            Converter converter = new Converter();

            //Act
            var result = converter.ValidateAmount(amount);

            //Assert
            Assert.False(result);
        }
    }
}
