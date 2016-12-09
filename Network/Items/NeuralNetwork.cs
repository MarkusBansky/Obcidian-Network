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

namespace Network.Items
{
    public class NeuralNetwork : NetworkBase
    {
        public NeuralNetwork(int numberOfInputValues, int numberOfOutputNeurons)
        {
            ComputationalNeurons = new List<Neuron>();
            NeuralConnections = new List<NeuralConnection>();

            InputNeurons = new List<Neuron>();
            for(int i = 0; i < numberOfInputValues; i++)
                InputNeurons.Add(new Neuron());

            OutputNeurons = new List<Neuron>();
            for (int i = 0; i < numberOfOutputNeurons; i++)
                OutputNeurons.Add(new Neuron());
        }
        
        public void AddNeuron(Neuron neuron)
        {
            ComputationalNeurons.Add(neuron);
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
            for (int i = 0; i < InputNeurons.Count; i++)
                SetInputValue(i, inputDoubles[i]);

            // Perform forward propagation
            ForwardPropagation();

            // Perform backward propagation
            BackPropagation(outputDoubles);

            return GetOutputValues();
        }

        public void SetInputValue(int index, double value)
        {
            if (InputNeurons[index] == null)
                InputNeurons[index] = new Neuron();

            InputNeurons[index].Value = value;
        }

        public double[] GetOutputValues()
        {
            return OutputNeurons.Select(on => on.Value).ToArray();
        }

        public double[] ForwardPropagation()
        {
            foreach (Neuron cn in ComputationalNeurons)
            {
                cn.InputValue = NeuralConnections.Where(n => this[n.NextNeuron].Guid.Equals(cn.Guid)).Sum(nc => this[nc.PreviousNeuron].Value * nc.Multiplier);
                cn.Value = cn.Invoke();
            }

            OutputNeurons.ForEach(on =>
            {
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
                NeuralConnection connection = NeuralConnections[NeuralConnections.Count - OutputNeurons.Count + i];
                double dEtotal = -(targetValues[i] - OutputNeurons[i].Value) * OutputNeurons[i].Value * (1 - OutputNeurons[i].Value) * this[connection.PreviousNeuron].Value;

                newNeuralConnections[NeuralConnections.IndexOf(connection)].Multiplier -= 0.5 * dEtotal;
            }

            // for other connections
            foreach (NeuralConnection connection in NeuralConnections.Where(nc => nc.NextNeuron < InputNeurons.Count + ComputationalNeurons.Count))
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
                   ComputationalNeurons.Count + InputNeurons.Count
                   ? targetValues.Select(
                       (t, i) => 
                       -(t - OutputNeurons[i].Value) 
                       * OutputNeurons[i].Value 
                       * (1 - OutputNeurons[i].Value) 
                       * NeuralConnections[NeuralConnections.Count - OutputNeurons.Count + i].Multiplier
                       ).Sum() 
                   : targetValues.Select(
                       (t, i) => 
                       -(t - OutputNeurons[i].Value) 
                       * neuron.Value 
                       * (1 - neuron.Value) 
                       * outputConnections[i].Multiplier
                       ).Sum();
        }
    }
}
