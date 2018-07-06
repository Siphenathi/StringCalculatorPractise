using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace StringCalculator_2ndPractise.Test
{
    [TestFixture]
    public class StringCalculatorTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Add_GivenInvalidString_ShouldReturnZero(string input)
        {
            //Arrange
            var expected = 0;
            var sut = new  StringCalculator();

            //Act

            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1",1)]
        [TestCase("1,2",3)]
        [TestCase("1,4,8",13)]
        public void Add_GivenNumberAsString_ShouldReturnSumOfNumbers(string input, int expected)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }


        [TestCase("1\n2,3", 6)]
        [TestCase("\n8,\n6\n,,3", 17)]
        public void Add_GivenInputWithNewLines_ShouldReturnSumOfNumbers(string input, int expected)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("//;\n1;2", 3)]
        [TestCase("//*\n3*2", 5)]
        [TestCase("//,\n3,7", 10)]
        public void Add_GivenInputWithCustomDelimiter_ShouldReturnSumOfNumbers(string input, int expected)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("-1,-2", "negatives not allowed -1,-2")]
        public void Add_GivenInputWithNegatives_ShouldThrowExceptionWithTheMessage(string input, string expectedMessage)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = Assert.Throws<Exception>(()=>sut.Add(input));

            //Assert
            Assert.AreEqual(expectedMessage, actual.Message);
        }
        [TestCase("1000,3,\n9", 1012)]
        [TestCase("1002,3,\n9", 12)]
        public void Add_GivenInput_ShouldIgnoreNumbersGreatherThan1000(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[###]\n1###3###3", 7)]
        public void Add_GivenInputWithDelimiter_ShouldIgnoreDelimiterAndGiveSumOfValues(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        public void Add_GivenInputWithMultipleDelimiterInBrackets_ShouldIgnoreDelimiterAndGiveSumOfValues(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculator();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }
    }
}
