using System.Collections.Generic;
using System.Linq;
using Network.Base.Connections;
using Network.Instances;

namespace Network.Base.Network
{
    public class NetworkBase
    {
        protected List<Neuron> InputNeurons;
        protected List<Neuron> OutputNeurons;

        protected List<Neuron> ComputationalNeurons;
        protected List<NeuralConnection> NeuralConnections;

        #region Overloaded Methods
        public Neuron this[int index]
        {
            get { return InputNeurons[index] ?? (InputNeurons[index] = new Neuron()); }
            set { InputNeurons[index] = value; }
        }
        #endregion

        public void SetInputValue(int index, double value)
        {
            if(InputNeurons[index] == null)
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
                cn.InputValue =NeuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(cn.Guid)).Sum(nc => GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier);
                cn.Value = cn.Invoke();
            }
            
            OutputNeurons.ForEach(on =>
            {
                on.InputValue =
                    NeuralConnections.Where(n => GetNeuron(n.NextNeuron).Guid.Equals(on.Guid))
                        .Sum(nc => GetNeuron(nc.PreviousNeuron).Value*nc.Multiplier);
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
                NeuralConnection connection =NeuralConnections[NeuralConnections.Count - OutputNeurons.Count + i];
                double dEtotal = -(targetValues[i] - OutputNeurons[i].Value)*OutputNeurons[i].Value * (1 - OutputNeurons[i].Value)*GetNeuron(connection.PreviousNeuron).Value;

                newNeuralConnections[NeuralConnections.IndexOf(connection)].Multiplier -= 0.5*dEtotal;
            }

            // for other connections
            foreach(NeuralConnection connection in NeuralConnections.Where(nc => nc.NextNeuron < InputNeurons.Count + ComputationalNeurons.Count))
            {
                double dEtotal = 
                    DeltaTotalSum(GetNeuron(connection.NextNeuron), targetValues) 
                    * GetNeuron(connection.NextNeuron).Value 
                    * (1 - GetNeuron(connection.NextNeuron).Value) 
                    * GetNeuron(connection.PreviousNeuron).Value;
                newNeuralConnections[NeuralConnections.IndexOf(connection)].Multiplier -= 0.5*dEtotal;
            }

            NeuralConnections = newNeuralConnections;
        }

        protected double DeltaTotalSum(Neuron neuron, double[] targetValues)
        {
            List<NeuralConnection> outputConnections = NeuralConnections.Where(nc => GetNeuron(nc.PreviousNeuron).Equals(neuron)).ToList();
            return outputConnections.OrderBy(c => c.NextNeuron).Last().NextNeuron >=
                   ComputationalNeurons.Count + InputNeurons.Count ? targetValues.Select((t, i) => -(t - OutputNeurons[i].Value)*OutputNeurons[i].Value*(1 - OutputNeurons[i].Value)*NeuralConnections[NeuralConnections.Count - OutputNeurons.Count + i].Multiplier).Sum() : targetValues.Select((t, i) => -(t - OutputNeurons[i].Value)*neuron.Value*(1 - neuron.Value)*outputConnections[i].Multiplier).Sum();
        }

        protected Neuron GetNeuron(int index)
        {
            if (index < InputNeurons.Count)
                return InputNeurons[index];
            else if (index < ComputationalNeurons.Count + InputNeurons.Count)
                return ComputationalNeurons[index - InputNeurons.Count];
            else
                return OutputNeurons[index - (InputNeurons.Count + ComputationalNeurons.Count)];
        }
    }
}
