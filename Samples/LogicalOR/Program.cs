using System;
using System.Linq;
using ObcidiaNetwork;

namespace LogicalOR
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main (string[] args)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new Application ();
        }
    }

    public class Application
    {
        private NeuralNetwork _network;
        private ulong _pass;
        private float[] _values;

        private readonly float[] _initialValues1 = { 1, 0 };
        private readonly float[] _initialValues2 = { 0, 1 };
        private readonly float[] _initialValues3 = { 1, 1 };
        private readonly float[] _initialValues4 = { 0, 0 };
        private readonly float[] _expectedOutput1 = { 1 };
        private readonly float[] _expectedOutput2 = { 1 };
        private readonly float[] _expectedOutput3 = { 0 };
        private readonly float[] _expectedOutput4 = { 0 };

        public Application ()
        {
            GenerateNetwork ();
            ApplicationLoop ();
        }

        private void GenerateNetwork ()
        {
            _network = new NeuralNetwork (2, 18, 1);
        }

        private void ApplicationLoop ()
        {
            ConsoleKey cki;
            do
            {
                DisplayMenu ();

                cki = Console.ReadKey ().Key;
                Console.WriteLine ();

                switch (cki)
                {
                    // Create new network
                    case ConsoleKey.D0:
                        GenerateNetwork ();
                        break;
                    // Calculate once
                    case ConsoleKey.D1:
                        _values = Console.ReadLine().Split(' ').Select(float.Parse).ToArray();
                        _network.CalculateOutputs(_values);
                        PrintData();
                        break;
                    case ConsoleKey.D2:
                        for (int i = 0; i < 100; i++)
                        {
                            _network.Train (_initialValues1, _expectedOutput1);
                            _network.Train (_initialValues2, _expectedOutput2);
                            _network.Train (_initialValues3, _expectedOutput3);
                            _network.Train (_initialValues4, _expectedOutput4);
                            _pass++;
                        }
                        break;
                    case ConsoleKey.D5:
                        Console.WriteLine ("Json: \n" + _network.ExportJson () + "\n");
                        break;
                    // Exit
                    case ConsoleKey.D3:
                        break;
                }
            } while (cki != ConsoleKey.Escape);
        }

        private void DisplayMenu ()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine ("0. Create new network.");
            Console.WriteLine ("1. Calculate outputs.");
            Console.WriteLine ("2. Teach 100 times.");
            Console.WriteLine ("5. Export JSON.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ("   ESC to exit.");
            Console.WriteLine ();
            Console.ResetColor ();
        }

        private void PrintData ()
        {
            Console.ResetColor ();
            Console.WriteLine ($"Pass: #{_pass}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Inputs: [" + string.Join (", ", _values) + "]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ("Result: [" + string.Join (", ", _network.CalculateOutputs (_values)) + "]");
            Console.WriteLine ();
            Console.ResetColor ();
        }
    }
}
