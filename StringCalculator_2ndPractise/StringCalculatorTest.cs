using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator.Test
{
    [TestFixture]
    public class StringCalculatorTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Add_GivenInValid_ShouldReturnZero(string input)
        {
            //Arrange
            var sut = new StringCalculatorLogic();
            var expected = 0;

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1",1)]
        [TestCase("1,2", 3)]
        public void Add_GivenInput_ShouldReturnSumOfNumbersFromInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("1\n2,3", 6)]
        [TestCase("5\n,10\n,10", 25)]
        public void Add_GivenInputWithNewLine_ShouldReturnSumOfNumbersFromInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//*\n5*2", 7)]
        public void Add_GivenInputWithNewLineAndDelimiter_ShouldReturnSumOfNumbersFromInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("-2,-3", "negatives not allowed -2,-3")]
        public void Add_GivenNegativeInput_ShouldThrowException(string input, string expectedMessage)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = Assert.Throws<Exception>(() => sut.Add(input));

            //Assert
            Assert.AreEqual(expectedMessage, actual.Message);
        }

        [TestCase("1000,2",1002 )]
        [TestCase("//*\n1001*2", 2)]
        public void Add_GivenInputWithNumbersGreaterThan1000_ShouldNotIncludeInSum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[@@@]\n1@@@2@@@3", 6)]
        public void Add_GivenInputWithDelimiter_ShouldReturnSumOfInputs(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }
        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Add_GivenInputWithDifferentDelimiters_ShouldReturnSumOfInputs(string input, int expectedSum)
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

            input = RemoveDelimiterInInput(input);

            var listOfInput = input.Split(new []{ delimiter, '\n','[',']','%','#','*' },StringSplitOptions.RemoveEmptyEntries);

            HandleNegatives(listOfInput);

            var sum = listOfInput.Where( x=>int.Parse(x)<=1000).Sum(int.Parse);
            return sum;
        }

        private static void HandleNegatives(string [] listOfInput)
        {
            var listOfNegatives = listOfInput.Where(x => int.Parse(x) < 0);

            if (listOfNegatives.Any())
            {
                var listOfNegativesAsString = string.Join(",", listOfNegatives);
                throw new Exception($"negatives not allowed {listOfNegativesAsString}");
            }
        }

        private static char GetDelimiter(string input)
        {
            if (input.StartsWith("//") && input.Contains("["))
            {
                return Convert.ToChar(input.Substring(3, 1));

            }
            return input.StartsWith("//") ? Convert.ToChar(input.Substring(2, 1)) : ',';
        }

        private static string  GetDelimiterInSecondBracket(string input)
        {
            int startIndex = 0;
            for (int x = 0; x < input.Length; x++)
            {
                if (input[x].ToString() == "[")
                {
                    startIndex = x;
                }
            }

            input = input.Substring(startIndex, 1);

        }

        private string RemoveDelimiterInInput(string input)
        {

            if (input.StartsWith("//"))
            {
                return input.Substring(2);
            }

            return input;
        }

    }
}
