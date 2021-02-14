using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StringCalculator
{
    public class Calculator
    {
        private readonly List<string> _supportedDelimiters;

        public Calculator()
        {
            _supportedDelimiters = new List<string>();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     Gets the User Inputs and Validates
        /// </summary>
        /// <param name="numbers">A numbers</param>
        /// <returns>Calculated result</returns>
        public int Add(string numbers)
        {
            var result = 0;
            if (string.IsNullOrEmpty(numbers)) return 0;

            if (IsHeaderPresent(numbers))
            {
                var endOfHeaderIndex = GetIndexOfFirstNumber(numbers);
                var header = numbers.Substring(0, endOfHeaderIndex);
                numbers = numbers.Substring(endOfHeaderIndex);
                GetDelimitersFromHeader_recursiv(header);
            }
            else
            {
                UseDefaultDelimiters();
            }

            var numbersToSum = GetNumbersFromDelimitersBasedString(numbers);

            if (HasOnlyPositiveValues(numbersToSum)) result = SumIntArray(numbersToSum);
            return result;
        }

        /// <summary>
        ///     Gets the delimiters from the header(i.e input)
        /// </summary>
        /// <param name="header">The Headers</param>
        private void GetDelimitersFromHeader_recursiv(string header)
        {
            if (header.StartsWith("//")) header = header.Remove(0, 2);

            if ('[' == header[0])
            {
                header = header.Remove(0, 1);
                //Brackets delimited delimiters
                var continueLookingForHeaders = true;
                while (continueLookingForHeaders)
                {
                    var nextDelimiterIndex = header.IndexOf("][");
                    var endOfHeader = header.IndexOf("]\n", StringComparison.Ordinal);

                    if (nextDelimiterIndex > 0)
                    {
                        _supportedDelimiters.Add(header.Substring(0, nextDelimiterIndex));
                        header = header.Remove(0, nextDelimiterIndex + 2);
                    }
                    else
                    {
                        _supportedDelimiters.Add(header.Substring(0, endOfHeader));
                        continueLookingForHeaders = false;
                    }
                }
            }
            else
            {
                _supportedDelimiters.Add(header[0].ToString());
            }
        }

        /// <summary>
        ///     This is Default Delimiter
        /// </summary>
        private void UseDefaultDelimiters()
        {
            _supportedDelimiters.Add(",");
            _supportedDelimiters.Add("\n");
        }

        /// <summary>
        ///     Checks the input if there is any negative value
        /// </summary>
        /// <param name="numbersToSum">The numbersToSum</param>
        /// <returns>Returns true if it has positive value or else returns false</returns>
        private bool HasOnlyPositiveValues(int[] numbersToSum)
        {
            var negativeNumbersFromArray = numbersToSum.Where(x => x < 0).ToArray();
            if (negativeNumbersFromArray.Length != 0)
                throw new Exception($"Negatives not allowed: {string.Join(",", negativeNumbersFromArray)}");

            return true;
        }

        /// <summary>
        ///     Sums the int Array
        /// </summary>
        /// <param name="numbersToSum">The numbersToSum</param>
        /// <returns>Returns the sum of result</returns>
        public int SumIntArray(int[] numbersToSum)
        {
            var result = 0;
            foreach (var t in numbersToSum)
                if (t < 1000)
                    result += t;

            return result;
        }

        /// <summary>
        ///     Split the input
        /// </summary>
        /// <param name="stringToSplit">The stringToSplit</param>
        /// <returns>Returns string array </returns>
        public string[] SplitString(string stringToSplit)
        {
            return stringToSplit.Split(_supportedDelimiters.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        ///     Checks input if it has escape characters
        /// </summary>
        /// <param name="numbers">The numbers</param>
        /// <returns>Returns true if it has escape characters, else returns false</returns>
        public bool IsHeaderPresent(string numbers)
        {
            if (numbers.StartsWith("//")) return true;
            return false;
        }

        /// <summary>
        ///     Gets the index of input value
        /// </summary>
        /// <param name="numbers">The numbers</param>
        /// <returns>Returns the Index of First Number</returns>
        public int GetIndexOfFirstNumber(string numbers)
        {
            var regex = new Regex(@"\d+");
            var match = regex.Match(numbers);

            return match.Index;
        }

        /// <summary>
        ///     Gets the integer value from the input based on delimiter
        /// </summary>
        /// <param name="numbers">The numbers</param>
        /// <returns>Returns an Integer Array</returns>
        public int[] GetNumbersFromDelimitersBasedString(string numbers)
        {
            numbers = numbers.Replace(@"\n", ",");
            var splittedNumbers = SplitString(numbers) ?? throw new ArgumentNullException("SplitString(numbers)");
            return Array.ConvertAll(splittedNumbers, int.Parse);
        }
    }
}