using System;

namespace StringCalculator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var endApp = false;

            while (!endApp)
            {
                var calculator = new Calculator();

                //Title as String calculator app.
                Console.WriteLine("String calculator app\r");
                Console.WriteLine("------------------------\n");

                // Ask the user to type the input.
                Console.WriteLine("Please enter your input, and then press Enter");
                var input = Console.ReadLine();

                try
                {
                    var result = calculator.Add(input);

                    Console.WriteLine("result -- {0}", result);

                    Console.WriteLine("------------------------\n");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Negatives"))
                    {
                        Console.WriteLine("here are some negative values: \n {0}", string.Join(", ", ex.Message));
                        Console.WriteLine("\n");
                    }
                }


                // Wait for the user to respond before closing.
                Console.Write("Press 'n' and Enter to close the app, or press any other key and Enter to continue: ");
                if (Console.ReadLine() == "n") endApp = true;

                Console.WriteLine("\n"); // Friendly line-spacing.
            }
        }
    }
}