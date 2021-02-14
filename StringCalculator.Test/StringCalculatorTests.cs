using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringCalculator.Test
{
    [TestClass]
    public class StringCalculatorTests
    {
        private Calculator _stringCalculator;

        #region InitializeAndClean

        [TestInitialize]
        public void TestInitialize()
        {
            _stringCalculator = new Calculator();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _stringCalculator.Dispose();
        }

        #endregion

        #region Individual Unit Tests

        [TestMethod]
        public void Test_Add_empty_zero()
        {
            const int expected = 0;
            var input = string.Empty;

            var actual = _stringCalculator.Add(input);

            Assert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void Test_Add_SingleNumber_NumberAsInt()
        {
            int[] expected = {1, 2, 15};
            string[] input = {"1", "2", "15"};

            for (var i = 0; i < input.Length; i++)
            {
                var actual = _stringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_TwoCommaSeparatedNumbers_SumOfTheTwoNumbers()
        {
            int[] expected = {2, 2, 21};
            string[] input = {"1,1", "2", "15,6"};

            for (var i = 0; i < input.Length; i++)
            {
                var actual = _stringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_MultipleCommaSeparatedNumbers_SumOfTheNumbers()
        {
            int[] expected = {9};
            string[] input = {"1,1,1,2,4"};

            for (var i = 0; i < input.Length; i++)
            {
                var actual = _stringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_MultipleNumberSpecialDelimiters_SumOfTheNumbers()
        {
            int[] expected = {6};
            string[] input = {"1\n2,3"};

            for (var i = 0; i < input.Length; i++)
            {
                var actual = _stringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_HeaderDelimiter_SumOfTheNumbers()
        {
            int[] expected = {3};
            var input = new[] {"//;\n1;2"};

            for (var i = 0; i < input.Length; i++)
            {
                var actual = _stringCalculator.Add(input[i]);
                Assert.AreEqual(expected[i], actual);
            }
        }

        [TestMethod]
        public void Test_Add_IgnoreNumberAbove1000_SumOfPositiveNumbersBelow1000()
        {
            var input = "//;\n1;2;1001";
            var expected = 3;
            Assert.AreEqual(expected, _stringCalculator.Add(input));
        }

        [TestMethod]
        public void Test_Add_MultipleCharDelimiter_SumOfNumbers()
        {
            var input = "//[***]\n1***2***3";
            var expected = 6;
            Assert.AreEqual(expected, _stringCalculator.Add(input));
        }

        [TestMethod]
        public void Test_Add_SpecialCaseMultipleCharDelimiter_SumOfNumbers()
        {
            var input = "//[]]]]\n1]]]2]]]3";
            var expected = 6;
            Assert.AreEqual(expected, _stringCalculator.Add(input));
        }


        [TestMethod]
        public void Test_Add_MultipleDelimitersUsingBraces_SumOfNumbers()
        {
            var input = "//[*][%]\n1*2%3";
            var expected = 6;
            Assert.AreEqual(expected, _stringCalculator.Add(input));
        }

        [TestMethod]
        public void Test_Add_MultipleDelimitersAnyLengthUsingBraces_SumOfNumbers()
        {
            var input = "//[**][%/]\n1**2%/3";
            var expected = 6;
            Assert.AreEqual(expected, _stringCalculator.Add(input));
        }

        #endregion
    }
}