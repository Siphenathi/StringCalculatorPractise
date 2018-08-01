using System;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace stringCalculator_5thParactise
{
    public class StringCalculatorTest
    {
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Add_GivenInValidInput_ShouldReturnZero(string input)
        {
            //Arrange
            var sut = new StringCalculatorLogic();
            var expected = 0;

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("1,2", 3)]
        [TestCase("1", 1)]
        public void Add_GivenValidInput_ShouldReturnSumOfNumbersInTheInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("1\n2,3", 6)]
        public void Add_GivenValidInputWithNewLine_ShouldReturnSumOfNumbersInTheInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }
        [TestCase("//;\n1;2", 3)]
        [TestCase("//%3\n1%2", 6)]
        public void Add_GivenValidInputWithNewLineAndDelimiter_ShouldReturnSumOfNumbersInTheInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }
        [TestCase("-1", "negatives not allowed -1")]
        [TestCase("-1,-2", "negatives not allowed -1,-2")]
        [TestCase("//%3\n1%-2", "negatives not allowed -2")]
        public void Add_GivenValidInputWithNegative_ShouldThrowException(string input, string expectedmsg)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = Assert.Throws<Exception>(() => sut.Add(input));

            //Assert
            Assert.AreEqual(expectedmsg, actual.Message);
        }

        [TestCase("1001,2", 2)]
        [TestCase("1000,2", 1002)]
        public void Add_GivenValidInput_ShouldIgnoreNumbersbiggerthan1000(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("1001,2", 2)]
        [TestCase("1000,2", 1002)]
        public void Add_GivenValidInputWithNumberGreaterThan1000_ShouldNotIncludeNumbersGreater1000InASum(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[***]\n1***2***3" , 6)]
        [TestCase("//[^^]\n1^^2^^3", 6)]
        public void Add_GivenValidInputWithDelimiterOfAnyLength_ShouldReturnSumOfNumbersInTheInput(string input, int expectedSum)
        {
            //Arrange
            var sut = new StringCalculatorLogic();

            //Act
            var actual = sut.Add(input);

            //Assert
            Assert.AreEqual(expectedSum, actual);
        }

        [TestCase("//[*][%]\n1*2%3", 6)]
        [TestCase("//[#][$]\n1#6$2", 9)]
        [TestCase("//[!][$]4\n1!6$2", 13)]
        public void Add_GivenValidInputWithMultipleDelimitersOfAnyLength_ShouldReturnSumOfNumbersInTheInput(string input, int expectedSum)
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
