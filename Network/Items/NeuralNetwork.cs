#region Licence
//  /*************************************************************************
//  * 
//  * AFGOR CONFIDENTIAL
//  * __________________
//  * 
//  *  [2015] - [2016] Markus Benovsky, Afgor Entertainment
//  *  All Rights Reserved.
//  * 
//  * NOTICE:  All information contained herein is, and remains
//  * the property of Afgor Entertainment and its suppliers,
//  * if any.  The intellectual and technical concepts contained
//  * herein are proprietary to Afgor Entertainment
//  * and its suppliers and may be covered by UA and Foreign Patents,
//  * patents in process, and are protected by trade secret or copyright law.
//  * Dissemination of this information or reproduction of this material
//  * is strictly forbidden unless prior written permission is obtained
//  * from Afgor Entertainment.
//  * 
//  * Code written by Markus Benovsky for ObsidiaNetwork project in NauralNetworks
//  * 2016 11 25
//  */
#endregion
using System.Collections.Generic;
using System.Linq;
using Network.Base.Network;
using Network.Base.Neurons;
using Network.Enumerations;
using Network.Extentions.Templates;

namespace Network.Items
{
    public class NeuralNetwork : NetworkBase
    {
        public NeuralNetwork(NeuronFunctions neuronDefaultFunction, int numberOfInputValues, int numberOfOutputNeurons, int numberOfBiasNeurons, int numberOfComputationalNeurons, bool makeDefaultMappings)
        {
            NeuronsCollection = new NetworkNeuronsCollection();
            NeuralConnections = new List<NeuralConnection>();

            for (int i = 0; i < numberOfInputValues; i++)
                NeuronsCollection.AddInputNeuron();

            for (int i = 0; i < numberOfOutputNeurons; i++)
                NeuronsCollection.AddOutputNeuron(new Neuron(neuronDefaultFunction));

            for (int i = 0; i < numberOfComputationalNeurons; i++)
                NeuronsCollection.AddComputationalNeuron(new Neuron(neuronDefaultFunction));

            if (makeDefaultMappings)
            {
                
            }
        }

        public void AddConnection(int first, int second)
        {
            NeuralConnections.Add(new NeuralConnection(first, second));
        }

        public void AddConnections(int[,] connectionPairsArray)
        {
            int height = connectionPairsArray.Length / 2;

            for (int y = 0; y < height; y++)
            {
                NeuralConnections.Add(new NeuralConnection(connectionPairsArray[y, 0], connectionPairsArray[y, 1]));
            }
        }

        public double[] TrainPropagation(double[] inputDoubles, double[] outputDoubles)
        {
            for (int i = 0; i < NeuronsCollection.InputNeuronsCount; i++)
                SetInputValue(i, inputDoubles[i]);

            // Perform forward propagation
            ForwardPropagation();

            // Perform backward propagation
            BackPropagation(outputDoubles);

            return GetOutputValues();
        }

        public void SetInputValue(int index, double value)
        {
            NeuronsCollection.GetInputNeuron(index).Value = value;
        }

        public double[] GetOutputValues()
        {
            return NeuronsCollection.NetworkNeurons.GetRange(NeuronsCollection.InputNeuronsCount, NeuronsCollection.OutputNeuronsCount).Select(r => r.Value).ToArray();
        }

        public double[] ForwardPropagation()
        {
            foreach (NeuronBase neuronBase in NeuronsCollection.NetworkNeurons.GetRange(NeuronsCollection.OutputNeuronsCount, NeuronsCollection.ComputationalNeuronsCount))
            {
                Neuron cn = (Neuron) neuronBase;
                cn.InputValue = NeuralConnections.Where(n => this[n.NextNeuron].Guid.Equals(cn.Guid)).Sum(nc => this[nc.PreviousNeuron].Value * nc.Multiplier);
                cn.Value = cn.Invoke();
            }

            NeuronsCollection.NetworkNeurons.GetRange(NeuronsCollection.InputNeuronsCount, NeuronsCollection.OutputNeuronsCount).ForEach(neuronBase =>
            {
                Neuron on = (Neuron)neuronBase;
                on.InputValue =
                    NeuralConnections.Where(n => this[n.NextNeuron].Guid.Equals(on.Guid))
                        .Sum(nc => this[nc.PreviousNeuron].Value * nc.Multiplier);
                on.Value = on.Invoke();
            });

            return GetOutputValues();
        }

        public void BackPropagation(double[] targetValues)
        {
            // Fixing connections
            List<NeuralConnection> newNeuralConnections = new List<NeuralConnection>(NeuralConnections);

            // For output connections
            for (int i = 0; i < targetValues.Length; i++)
            {
                NeuralConnection connection = NeuralConnections[NeuralConnections.Count - NeuronsCollection.OutputNeuronsCount + i];
                double dEtotal = -(targetValues[i] - NeuronsCollection.GetOutputNeuron(i).Value) * NeuronsCollection.GetOutputNeuron(i).Value * (1 - NeuronsCollection.GetOutputNeuron(i).Value) * this[connection.PreviousNeuron].Value;

                newNeuralConnections[NeuralConnections.IndexOf(connection)].Multiplier -= 0.5 * dEtotal;
            }

            // for other connections
            foreach (NeuralConnection connection in NeuralConnections.Where(nc => nc.NextNeuron < NeuronsCollection.InputNeuronsCount + NeuronsCollection.ComputationalNeuronsCount))
            {
                double dEtotal =
                    DeltaTotalSum(this[connection.NextNeuron], targetValues)
                    * this[connection.NextNeuron].Value
                    * (1 - this[connection.NextNeuron].Value)
                    * this[connection.PreviousNeuron].Value;
                newNeuralConnections[NeuralConnections.IndexOf(connection)].Multiplier -= 0.5 * dEtotal;
            }

            NeuralConnections = newNeuralConnections;
        }

        protected double DeltaTotalSum(Neuron neuron, double[] targetValues)
        {
            List<NeuralConnection> outputConnections = 
                NeuralConnections.Where(
                    nc => this[nc.PreviousNeuron].Equals(neuron)
                    ).ToList();

            return 
                outputConnections.OrderBy(c => c.NextNeuron).Last().NextNeuron >=
                   NeuronsCollection.ComputationalNeuronsCount + NeuronsCollection.InputNeuronsCount
                   ? targetValues.Select(
                       (t, i) => 
                       -(t - NeuronsCollection.GetOutputNeuron(i).Value) 
                       * NeuronsCollection.GetOutputNeuron(i).Value 
                       * (1 - NeuronsCollection.GetOutputNeuron(i).Value) 
                       * NeuralConnections[NeuralConnections.Count - NeuronsCollection.OutputNeuronsCount + i].Multiplier
                       ).Sum() 
                   : targetValues.Select(
                       (t, i) => 
                       -(t - NeuronsCollection.GetOutputNeuron(i).Value) 
                       * neuron.Value 
                       * (1 - neuron.Value) 
                       * outputConnections[i].Multiplier
                       ).Sum();
        }
    }
}
