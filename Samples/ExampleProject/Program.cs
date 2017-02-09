using System;
using ObcidiaNetwork;
using ObsidiaDebugger;

namespace ExampleProject_Lite
{
    class Program
    {
        // ReSharper disable once UnusedParameter.Local
        static void Main (string[] args)
        {
            Console.WindowWidth = 50;
            Console.WindowHeight = 30;
            // Start debugger instance
            new NeuralNetworkDebugger ();
            // ReSharper disable once ObjectCreationAsStatement
            new NetworkApplication ();
        }
    }

    public class NetworkApplication
    {
        private NeuralNetwork _network;
        private ulong _pass;

        private readonly float[] _initialValues = { 1, 0, 1, 0, 0 };
        private readonly float[] _expectedOutput = { 0, 1 };

        public NetworkApplication ()
        {
            GenerateNetwork ();
            ApplicationLoop ();
        }

        private void GenerateNetwork ()
        {
            _network = new NeuralNetwork (5, 10, 2);
            _network.SetInputValues (_initialValues);
        }

        private void ApplicationLoop ()
        {
            _network.EnableDebugging ();

            ConsoleKey cki;
            do
            {
                PrintData ();
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
                        _network.CalculateOutputs();
                        break;
                    case ConsoleKey.D2:
                        for (int i = 0; i < 10; i++)
                        {
                            _network.AdjustWeights(_expectedOutput);
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
            Console.WriteLine ("2. Texh 10 times.");
            Console.WriteLine ("5. Export JSON.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine ("   ESC to exit.");
            Console.WriteLine ();
            Console.ResetColor();
        }

        private void PrintData ()
        {
            Console.ResetColor ();
            Console.WriteLine ($"Pass: #{_pass}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine ("Inputs: [" + string.Join (", ", _initialValues) + "]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine ("Expected: [" + string.Join (", ", _expectedOutput) + "]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine ("Result: [" + string.Join (", ", _network.GetOutputValues()) + "]");
            Console.WriteLine ();
            Console.ResetColor();
        }
    }
}
