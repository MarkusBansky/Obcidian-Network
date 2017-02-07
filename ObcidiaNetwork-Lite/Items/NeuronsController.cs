using System;
using System.IO;
using System.Linq;
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
                ConnectionsContainer.Add (new Connection (NormalizeIndexForBiases(x), NormalizeIndexForComputational(x)));
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

        public void ForwardPropagation()
        {
            // Calculate outputs for computational neurons
            for (int i = 0; i < ComputationalCount; i++)
            {
                NeuronsContainer[NormalizeIndexForComputational(i)].InputValue =
                    ConnectionsContainer.Where (c => c.NeuronToId.Equals (NormalizeIndexForComputational (i)))
                        .Sum (c => NeuronsContainer[c.NeuronFromId].OutputValue * c.WeightValue);

                NeuronsContainer[NormalizeIndexForComputational(i)].OutputValue =
                    NeuronsContainer[NormalizeIndexForComputational(i)].ComputeFunction(
                        NeuronsContainer[NormalizeIndexForComputational(i)].InputValue);
            }
            // Calculate outputs values
            for (int i = 0; i < OutputsCount; i++)
            {
                NeuronsContainer[NormalizeIndexForOutputs (i)].InputValue =
                    ConnectionsContainer.Where (c => c.NeuronToId.Equals (NormalizeIndexForOutputs (i)))
                        .Sum (c => NeuronsContainer[c.NeuronFromId].OutputValue * c.WeightValue);

                NeuronsContainer[NormalizeIndexForOutputs (i)].OutputValue =
                    NeuronsContainer[NormalizeIndexForOutputs (i)].ComputeFunction (
                        NeuronsContainer[NormalizeIndexForOutputs (i)].InputValue);
            }
        }

        public void BackwardPropagation(float[] trainingResultsFloats)
        {
            float[] outputErrors = new float[OutputsCount];
            for (int i = 0; i < OutputsCount; i++)
            {
                outputErrors[i] = 0.5f * (float) Math.Pow(trainingResultsFloats[i] - NeuronsContainer[NormalizeIndexForOutputs(i)].OutputValue, 2);
            }

            float totalError = outputErrors.Sum();
        }
    }
}