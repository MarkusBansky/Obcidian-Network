using System;
using ObcidiaNetwork;

namespace ExampleProject_Lite
{
    class Program
    {
        static void Main (string[] args)
        {
            // Create new network with 3 inputs, 2 computational and 1 output neurons
            NeuralNetwork network = new NeuralNetwork(3, 2, 1);

            // Set input values
            network.SetInputValues(new []{ 1f, 0f, 1f});

            // Calculate outputs
            network.CalculateOutputs ();

            // Write out the values of outputs
            Console.WriteLine("Result: " + string.Join(", ", network.GetOutputValues()) + "\n");

            // Export json
            Console.WriteLine("\nJson: \n" + network.ExportJson() + "\n");
        }
    }
}
