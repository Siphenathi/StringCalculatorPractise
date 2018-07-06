using System;
using NUnit.Framework;
using StringCalculator.Business;
namespace StringCalculator.Test
{
    [TestFixture]
    public class StringCalculatorTest
    {
        [TestCase("", 0)]
        [TestCase(" ", 0)]
        [TestCase(null, 0)]
        public void Add_GivenInValidInput_ShouldReturnZero(string input, int expected)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1", 1)]
        [TestCase("1,2", 3)]
        [TestCase("1,4,5", 10)]
        public void Add_GivenValidInput_ShouldReturnSum(string input, int expected)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1\n2, 3", 6)]
        [TestCase("1\n,4,3", 8)]
        [TestCase("\n12,,1,2", 15)]
        public void Add_GivenValidInputWithLine_ShouldReturnSum(string input, int expected)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("//1\n70;3", 74)]
        [TestCase("//;\n4;2", 6)]
        [TestCase("//;\n1;2", 3)]
        public void Add_GivenValidInputWithLineAndDelimiter_ShouldReturnSum(string input, int expected)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("//;\n1;-2", "Negatives not allowed -2")]
        [TestCase("//;\n1;-3", "Negatives not allowed -3")]
        [TestCase("//;\n-2;-3", "Negatives not allowed -2,-3")]
        public void Add_GivenValidInputWithLine_DelimiterAndNegativeNumbers_ShouldThrowException(string input, string expectedMessage)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var exception = Assert.Throws<Exception>(() => sut.Add(input));

            //Assert
            Assert.AreEqual(expectedMessage, exception.Message);
        }

        [TestCase("//;\n1001;2", 2)]
        [TestCase("//;\n1001;", 0)]
        [TestCase("//;\n1000;44", 1044)]
        public void Add_GivenInputWithLine_DelimiterAndNumbersEqualOrBiggerThan1000_ShouldExcludeNumbersEqualOrBiggerThan1000InSum(string input, int expectedSum)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.AreEqual(expectedSum, actual);
        }


        [TestCase("//[***]4\n100", 104)]
        [TestCase("//[***]\n1***2***3", 6)]
        [TestCase("//[**]\n1**6***3", 10)]
        public void Add_GivenInputWithLine__And_2Delimiters_ShouldRetrunSumOfNumbersProvided(string input, int expectedSum)
        {
            //Arrange 
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);
            //Assert
            Assert.AreEqual(expectedSum, actual);
        }
        [TestCase("//[**][%]\n1**2%3", 6)]
        [TestCase("//[#][$]\n1#5$3", 9)]
        [TestCase("//[^][~][@]\n5^5~1@4", 15)]

        public void Add_GivenInputWithLine__And_ManyDelimiters_ShouldRetrunSumOfNumbersProvided(string input, int expectedSum)
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
