using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Text.RegularExpressions;

namespace StringCalculator.Business
{
    public class StringCalculatorLogic
    {
        public object Add(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            var listOfInputs = RemoveInValidCharacters(input);

            HandleNegitives(listOfInputs);

            var sum = listOfInputs.Where(number => int.Parse(number) <= 1000).Sum(int.Parse);

            return sum;
        }

        private static void HandleNegitives(IEnumerable<string> listOfInputs)
        {
            var listOfNegatives = listOfInputs.Where(number => int.Parse(number) < 0);

            if (!listOfNegatives.Any()) return;

            var negative = string.Join(",", listOfNegatives.ToArray());
            throw new Exception($"Negatives not allowed {negative}");
        }

        private static string[] RemoveInValidCharacters(string input)
        {
            var delimiters = GetDelimiters(input);
            input = GetDeliminatedNumbers(input, delimiters);


            var listOfNumbers =new string[input.Length];
            var listOfDelimiters = delimiters.ToArray();

            foreach (var delimiter in listOfDelimiters)
            {
                listOfNumbers = input.Split(new[] { delimiter, '\n' }, StringSplitOptions.RemoveEmptyEntries);
                input = GetUpDatedInputString(listOfNumbers);
            }
            
            return listOfNumbers;
        }

        private static string GetUpDatedInputString(string[] listOfNumbers)
        {
            var results = "";

            foreach (var value in listOfNumbers)
            {
                results += value + "\n";
            }
       
            return results.TrimEnd('\n');
        }

        private static string GetDeliminatedNumbers(string input, IEnumerable<char> delimiters)
        {
           
                if (delimiters.First() == ';')
                {
                    input = input.Substring(2);
                }

                if (input.Contains("[") && input.Contains("]"))
                {
                   input = input.Substring(GetLastBracketIndex(input)+1);
                }

            return input;
        }

        public static int GetLastBracketIndex(string input)
        {
            var index = 0;

            for (var x = 0; x< input.Length;x++)
            {
                if (input[x].ToString().Equals("]"))
                {
                    index = x;
                }
            }
            return index;
        }

        private static List<char> GetDelimiters(string input)
        {
            var delimiter = new List<char>();

            delimiter.Add(input.StartsWith("//") ? ';' : ',');

            if (input.Contains("["))
            {
                delimiter = GetDelimiterInSideTheBrackets(input, delimiter);
            }
            return delimiter;
        }

        private static List<char> GetDelimiterInSideTheBrackets(string input, List<char>delimiter)
        {
            const string regularExpressionPattern = @"\[(.*?)\]";

            var regex = new Regex(regularExpressionPattern);

            foreach (Match m in regex.Matches(input))
            {
                var value = m.Groups[1].Value.Substring(0, 1);
                delimiter.Add(Convert.ToChar(value));
            }
            return delimiter;
        }
    }
}
