using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculator_2ndPractise.Test
{
    public class StringCalculator
    {
        public int Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var delimiter = GetDelimiter(input);

            input = RemoveDelimiter(input);
            var arrayOfNumbersFromString = input.Split(new []{delimiter, '\n',']','*','%','@','&','#','?','$'},StringSplitOptions.RemoveEmptyEntries);

            HandleException(arrayOfNumbersFromString);

            var sum = arrayOfNumbersFromString.Where(x => int.Parse(x)<=1000).Sum(int.Parse);
            return sum;

        }

        private static void HandleException(IEnumerable<string> arrayOfNumbersFromString)
        {
            var negatives = arrayOfNumbersFromString.Where(x => int.Parse(x) < 0);

            if (!negatives.Any()) return;
            var listOfNegativesAsString = string.Join(",", negatives.ToArray());
            throw new Exception($"negatives not allowed {listOfNegativesAsString}");
        }

        private static string RemoveDelimiter(string input)
        {
            return input.StartsWith("//") ? input.Substring(2) : input;
        }

        private static char GetDelimiter(string input)
        {
            var delimiter = ' ';
            delimiter = input.StartsWith("//") ? char.Parse(input.Substring(input.IndexOf("//", StringComparison.Ordinal) + 2, 1)) : ',';
     
            return delimiter;
        }


    }
}