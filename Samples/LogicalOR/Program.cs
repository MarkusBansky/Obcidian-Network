using System;
using Network.Instances;

namespace LogicalOR
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            // ReSharper disable once UnusedVariable
            Application application = new Application();
        }
    }

    public class Application
    {
        private NeuralNetwork _network;
        private ulong _pass;

        private readonly double[] _initialValues = { 1, 1 };
        private readonly double[] _expectedOutput = {1, 0};
        private double[] _outputValues = {};

        public Application()
        {
            GenerateNetwork();
            ApplicationLoop();
        }

        private void GenerateNetwork()
        {
            _network = new NeuralNetwork(2, 2);

            // adding 2 neurons for 1 layer
            _network.AddNeuron(new Neuron());
            _network.AddNeuron(new Neuron());
            // 2 more for 2 layer
            //_network.AddNeuron(new Neuron());
            //_network.AddNeuron(new Neuron());

            // adding 2 neurons as biases for them
            _network.AddNeuron(new Neuron { InputValue = 1 });
            _network.AddNeuron(new Neuron { InputValue = 1 });

            // inputs to 1 layer
            _network.AddConnection(0, 2);
            _network.AddConnection(0, 3);
            _network.AddConnection(1, 2);
            _network.AddConnection(1, 3);

            // bias to 1 layer
            _network.AddConnection(4, 2);
            _network.AddConnection(5, 3);

            // 1 layer to output
            _network.AddConnection(2, 6);
            _network.AddConnection(2, 7);
            _network.AddConnection(3, 6);
            _network.AddConnection(3, 7);
        }

        private void ApplicationLoop()
        {
            ConsoleKey cki;
            do
            {
                PrintData();
                DisplayMenu();

                cki = Console.ReadKey().Key;
                Console.WriteLine();

                switch (cki)
                {
                    // Create new network
                    case ConsoleKey.D0:
                        GenerateNetwork();
                        break;
                    // Train once
                    case ConsoleKey.D1:
                        _outputValues = _network.TrainPropagation(_initialValues, _expectedOutput);
                        _pass++;
                        break;
                    // Train 1000 times
                    case ConsoleKey.D2:
                        for (int i = 0; i < 100; i++)
                        {
                            _outputValues = _network.TrainPropagation(_initialValues, _expectedOutput);
                        }
                        _pass += 100;
                        break;
                    // Performe forward calculations
                    case ConsoleKey.D3:
                        break;
                }
            } while (cki != ConsoleKey.Escape);
        }

        private void DisplayMenu()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("0. Create new network.");
            Console.WriteLine("1. Teach 1 time.");
            Console.WriteLine("2. Teach 100 time.");
            Console.WriteLine("3. Calculate output for your own values.");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("   ESC to exit.");
            Console.WriteLine();
        }

        private void PrintData()
        {
            Console.ResetColor();
            Console.WriteLine($"Pass: #{_pass}");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Inputs: [" + string.Join(", ", _initialValues) + "]");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Expected: [" + string.Join(", ", _expectedOutput) + "]");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Result: [" + string.Join(", ", _outputValues) + "]");
            Console.WriteLine();
        }
    }
}
