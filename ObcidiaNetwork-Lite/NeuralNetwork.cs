using System.Linq;
using System.Text.RegularExpressions;
using ObcidiaNetwork.Items;

namespace ObcidiaNetwork
{
    public class NeuralNetwork
    {
        private readonly NeuronsController _controller;

        public NeuralNetwork (int inputsCount, int computationalCount, int outputsCount)
        {
            _controller = new NeuronsController(inputsCount, computationalCount, outputsCount);
        }

        public void SetInputValues(float[] inputValues)
        {
            _controller.SetInputValues(inputValues);
        }

        public float[] GetOutputValues()
        {
            return _controller.GetOutputValues();
        }

        public void CalculateOutputs ()
        {
            _controller.ForwardPropagation();
        }

        public void AdjustWeights ()
        {

        }

        public string ExportJson()
        {
            return _controller.ToString();
        }

        public string ExportJsonMinified()
        {
            return Regex.Replace (_controller.ToString (), @"\t|\n|\r", "");
        }
    }
}
