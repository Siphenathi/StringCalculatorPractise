using System;
using System.Collections.Generic;
using System.Linq;

namespace stringCalculator_5thParactise
{
    public class StringCalculatorLogic
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var delimiter = GetDelimiter(input);
            input = RemoveSlash(input);

            var listOfNumbers = input.Split(new[] { delimiter, '\n','[',']','%','#','@','$','&' }, StringSplitOptions.RemoveEmptyEntries);
            HandleNegativeNumbers(listOfNumbers);
            var sum = listOfNumbers.Where(x => int.Parse(x) <= 1000).Sum(int.Parse);
            return sum;
        }

        private static void HandleNegativeNumbers(IEnumerable<string> listOfNumbers)
        {
            var negatives = listOfNumbers.Where(x => int.Parse(x) < 0);

            if (negatives.Any())
            {
                var negativesString = string.Join(",", negatives);
                throw new Exception($"negatives not allowed {negativesString}");
            }
        }

        private static char GetDelimiter(string input)
        {
            if (input.StartsWith("//") && input.Contains("[") && input.Contains("]"))
            {
                return char.Parse(input.Substring(3, 1));
            }
            if (input.StartsWith("//"))
            {
                return char.Parse(input.Substring(2, 1));
            }
            return ',';
        }

        private static string RemoveSlash(string input)
        {
            return input.StartsWith("//") ? input.Substring(2) : input;
        }
    }
}