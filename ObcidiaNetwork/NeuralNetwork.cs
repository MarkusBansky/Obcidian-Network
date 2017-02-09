using System;
using System.Windows.Forms;
using ObcidiaNetwork.Client;
using ObcidiaNetwork.Items;

namespace ObcidiaNetwork
{
    public class NeuralNetwork
    {
        private RestClient _client;
        private readonly NeuronsController _controller;

        private bool _debugEnabled;

        public NeuralNetwork (int inputsCount, int computationalCount, int outputsCount)
        {
            _controller = new NeuronsController(inputsCount, computationalCount, outputsCount);
        }

        public void EnableDebugging()
        {
            try
            {
                _client = new RestClient();
                _client.StartClient();

                _debugEnabled = true;

                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Enabled visual debugging!\n");
                Console.ResetColor();
            }
            catch
            {
                _debugEnabled = false;
                MessageBox.Show("Error!", "Cannot start the debugger client. Please check if debugger instance is running!");
            }
        }

        public void SetInputValues(float[] inputValues)
        {
            _controller.SetInputValues(inputValues);

            if (_debugEnabled)
            {
                _client.SendMessage(_controller.ToString());
            }
        }

        public float[] GetOutputValues()
        {
            return _controller.GetOutputValues();
        }

        public void CalculateOutputs ()
        {
            _controller.ForwardPropagation();

            if (_debugEnabled)
            {
                _client.SendMessage (_controller.ToString ());
            }
        }

        public void AdjustWeights (float[] expectedValues)
        {
            _controller.BackwardPropagation(expectedValues);

            if (_debugEnabled)
            {
                _client.SendMessage (_controller.ToString ());
            }
        }

        public string ExportJson()
        {
            return _controller.ToString();
        }
    }
}
