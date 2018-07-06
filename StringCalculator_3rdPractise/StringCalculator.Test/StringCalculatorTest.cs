using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using StringCalculator.Logic;

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

        [TestCase("1", 1)]
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

        [TestCase("1000,2", 1002)]
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
        [TestCase("//[&][#]\n9&2#3", 14)]
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
}
