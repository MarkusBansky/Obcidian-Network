using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using Network;
using Network.Base.Neurons;

namespace NauralNetworks
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // ===== Testing the neural networks
            // Creating network
            NeuralNetwork network = new NeuralNetwork(2);

            // Adding 4 computational neurons
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });
            network.AddNeuron(new ComputationalNeuron
            {
                ForwardCalculation = Function,
                BackwardCalculation = FunctionPrime
            });

            // Adding connections beetween them
            network.AddConnection(0, 2);
            network.AddConnection(0, 3);
            network.AddConnection(0, 4);

            network.AddConnection(1, 2);
            network.AddConnection(1, 3);
            network.AddConnection(1, 4);

            network.AddConnection(2, 5);
            network.AddConnection(2, 6);
            network.AddConnection(2, 7);

            network.AddConnection(3, 5);
            network.AddConnection(3, 6);
            network.AddConnection(3, 7);

            network.AddConnection(4, 5);
            network.AddConnection(4, 6);
            network.AddConnection(4, 7);

            network.AddConnection(5, 8);
            network.AddConnection(6, 8);
            network.AddConnection(7, 8);

            // Calculations values:
            double output;
            double[] values = {1, 1, 0};

            // Calculation parameters
            const double epsilon = 0.0000000000001;
            int generation = 0;

            // Perform forward calculation and backward
            do
            {
                if (generation != 0)
                    network.BackPropagation(values[2]);

                network[0].Value = values[0];
                network[1].Value = values[1];

                network.ForwardPropagation();
                output = network.GetOutputValue();

                // OUTPUT
                //Print(ref network, generation, false);

                generation++;
            } while (output > values[2] + epsilon || output < values[2] - epsilon);

            // OUTPUT
            Print(ref network, generation);

            Console.ForegroundColor = ConsoleColor.Yellow;
        }

        public static void Print(ref NeuralNetwork network, int generation, bool last = true)
        {
            if (last)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine("==========================================================================");
                Console.ResetColor();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nDone!");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Result");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(network.GetOutputValue().ToString("0.0000000000"));

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Multiplier values:");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(network.GetMultiplierValues()
                .Select(i => i.ToString(CultureInfo.InvariantCulture))
                .Aggregate((s1, s2) => s1 + ", " + s2));
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Generation: " + generation);
            Console.WriteLine();
        }

        public static double Function(double value)
        {
            return 1.0/(1.0 + Math.Exp(value));
        }

        public static double FunctionPrime(double value)
        {
            return Math.Exp(value)*Math.Pow(1 + Math.Exp(value), 2);
        }
    }
}
