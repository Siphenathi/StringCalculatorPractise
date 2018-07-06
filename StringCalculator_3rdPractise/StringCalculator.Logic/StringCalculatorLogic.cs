using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator.Logic
{
    public class StringCalculatorLogic
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var listOfInput = Spilt(input);
            var sum = listOfInput.Where(x => int.Parse(x) <= 1000).Sum(int.Parse);

            return sum;
        }

        private IEnumerable<string> Spilt(string input)
        {
            var delimiterInsideTheFirstBracketAndOutsideBracket = GetDelimiter(input);
            var delimiterInSecondBrackets = GetDelimiterInSecondBracket(input);

            input = RemoveDelimiterInInput(input);

            var listOfInput = input.Split(new[] { delimiterInsideTheFirstBracketAndOutsideBracket, '\n', '[', ']', delimiterInSecondBrackets }, StringSplitOptions.RemoveEmptyEntries);

            HandleNegatives(listOfInput);
            return listOfInput;
        }
        private static void HandleNegatives(string[] listOfInput)
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
        public static char GetDelimiterInSecondBracket(string input)
        {
            var secondBracketIndex = 0;

            if (!input.Contains("[")) return ' ';
            var firstBracketIndex = input.IndexOf("[", StringComparison.Ordinal);

            for (int x = 0; x < input.Length; x++)
            {
                if (input[x].ToString() == "[")
                {
                    secondBracketIndex = x;
                }
            }

            if (firstBracketIndex == secondBracketIndex) return ' ';
            var character = Convert.ToChar(input.Substring(secondBracketIndex + 1, 1));
            return character;

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