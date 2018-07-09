using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator_4thPractise.Tests
{
    [TestFixture]
    public class StringCalculatorTests
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Add_GivenInvalidInput_ShouldReturnZero(string input)
        {
            //Arrange
            var expected = 0;
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        public void Add_GivenValidInput_ShouldReturnSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("1,\n", 1)]
        public void Add_GivenInputWithNewLine_ShouldReturnSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//*1*\n", 1)]
        public void Add_GivenInputWithNewLineAndDelimiter_ShouldReturnSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("//;\n-1;-2", "negatives not allowed -1,-2")]
        public void Add_GivenNegativeInput_ShouldThrowException(string input, string expectedMsg)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = Assert.Throws<Exception>(() => sut.Add(input));

            //Assert
            Assert.AreEqual(expectedMsg, actual.Message);
        }

        [TestCase("1001,12", 12)]
        [TestCase("1000,12", 1012)]
        public void Add_GivenInputGreaterThan1000_ShouldNotAddToSumOfInputs(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[@@]\n2@@2@@@3", 7)]
        public void Add_GivenInputWithDelimiterOfAnyLength_ShouldReturnSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Add_GivenInputWithMultipleDelimitersOfAnyLength_ShouldReturnSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }


    }

    public class StringCalculatorLogic
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var delimiter = GetDelimiter(input);
            input = RemoveInvalidDelimiter(input);

            var delimiterInSecondBracket = GetDelimiterInSecondBracket(input);

            var listOfNumbers = input.Split(new[] { delimiter, delimiterInSecondBracket, '\n', '[', ']'}, StringSplitOptions.RemoveEmptyEntries);

            HandleNegatives(listOfNumbers);

            var sumOfNumbers = listOfNumbers.Where(x => int.Parse(x) <= 1000).Sum(int.Parse);

            return sumOfNumbers;
        }

        private void HandleNegatives(string[] listOfNumbers)
        {
            var listOfNegatives = listOfNumbers.Where(x => int.Parse(x) < 0);

            if (listOfNegatives.Any())
            {
                var stringListOfNegatives = string.Join(",", listOfNegatives.ToArray());
                throw new Exception($"negatives not allowed {stringListOfNegatives}");
            }
        }

        private string RemoveInvalidDelimiter(string input)
        {
            return input.StartsWith("//") ? input.Substring(2) : input;
        }

        private static char GetDelimiter(string input)
        {
            if (input.StartsWith("//") && input.Contains("["))
            {
                return Convert.ToChar(input.Substring(3, 1));
            }
            return input.StartsWith("//") ? Convert.ToChar(input.Substring(2, 1)) : ',';
        }

        private static char GetDelimiterInSecondBracket(string input)
        {
            var delimiter = ' ';
            var startPoint = 0;

            if (!input.Contains("[")) return delimiter;
            for (var x = 0; x < input.Length; x++)
            {
                if (input[x].ToString().Equals("["))
                {
                    startPoint = x;
                }
            }

            delimiter = Convert.ToChar(input.Substring(startPoint + 1, 1));
            return delimiter;
        }
    }
}
