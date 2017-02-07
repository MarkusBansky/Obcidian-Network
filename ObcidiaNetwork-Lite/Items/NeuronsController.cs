using System.IO;
using ObcidiaNetwork.Base;

namespace ObcidiaNetwork.Items
{
    internal class NeuronsController : NeuronsControllerBase
    {
        public NeuronsController(int inputs, int computational, int outputs) : base(inputs, computational, outputs)
        {
            // set biases outputs
            for (int i = 0; i < ComputationalCount; i++)
            {
                NeuronsContainer[NormalizeIndexForBiases(i)] = new Neuron(1);
            }

            // add connections beetween inputs and computational
            for (int x = 0; x < InputsCount; x++)
            {
                for (int y = 0; y < ComputationalCount; y++)
                {
                    ConnectionsContainer.Add(new Connection(NormalizeIndexForInputs(x), NormalizeIndexForComputational(y)));
                }
            }
            // add connections beetween biases and computational
            for (int x = 0; x < BiasesCount; x++)
            {
                for (int y = 0; y < ComputationalCount; y++)
                {
                    ConnectionsContainer.Add (new Connection (NormalizeIndexForBiases(x), NormalizeIndexForComputational(y)));
                }
            }
            // add connections beetween computational and outputs
            for (int x = 0; x < ComputationalCount; x++)
            {
                for (int y = 0; y < OutputsCount; y++)
                {
                    ConnectionsContainer.Add (new Connection (NormalizeIndexForComputational(x), NormalizeIndexForOutputs(y)));
                }
            }
        }

        public void SetInputValues(float[] inputFloats)
        {
            if(inputFloats.Length != InputsCount)
                throw new InvalidDataException();

            for (int i = 0; i < InputsCount; i++)
            {
                NeuronsContainer[NormalizeIndexForInputs(i)] = new Neuron(inputFloats[i]);
            }
        }

        public float[] GetOutputValues()
        {
            float[] valueFloats = new float[OutputsCount];

            for (int i = 0; i < OutputsCount; i++)
            {
                valueFloats[i] = NeuronsContainer[NormalizeIndexForOutputs(i)].OutputValue;
            }

            return valueFloats;
        }
    }
}