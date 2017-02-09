using ObcidiaNetwork.Base;

namespace ObcidiaNetwork.Items
{
    internal class Neuron : NeuronBase
    {
        public Neuron(float outputValue)
        {
            OutputValue = outputValue;
        }

        public Neuron(float inputValue, float outputValue)
        {
            InputValue = inputValue;
            OutputValue = outputValue;
        }
    }
}