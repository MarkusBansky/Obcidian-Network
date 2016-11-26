using Microsoft.VisualStudio.TestTools.UnitTesting;
using Network.Base;
using Network.Items;

namespace ObsidiaNetworkTests.Base.Neurons
{
    [TestClass()]
    public class NeuronBaseTests
    {
        [TestMethod()]
        public void NeuronBaseTest()
        {
            NeuronBase neuron = new Neuron();
            neuron.InputValue = 1;
            neuron.Value = neuron.ForwardCalculation.Invoke(neuron.InputValue);

            Assert.AreNotEqual(0, neuron.Value);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            NeuronBase neuron1 = new Neuron();
            neuron1.InputValue = 1;
            neuron1.Value = neuron1.ForwardCalculation.Invoke(neuron1.InputValue);

            NeuronBase neuron2 = new Neuron();
            neuron2.InputValue = 0.5;
            neuron2.Value = neuron2.ForwardCalculation.Invoke(neuron2.InputValue);

            Assert.IsFalse(neuron1.Equals(neuron2));
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            NeuronBase neuron1 = new Neuron();
            neuron1.InputValue = 1;
            neuron1.Value = neuron1.ForwardCalculation.Invoke(neuron1.InputValue);

            NeuronBase neuron2 = new Neuron();
            neuron2.InputValue = 0.5;
            neuron2.Value = neuron2.ForwardCalculation.Invoke(neuron2.InputValue);

            Assert.IsFalse(neuron1 == neuron2);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            NeuronBase neuron1 = new Neuron();
            neuron1.InputValue = 1;
            neuron1.Value = neuron1.ForwardCalculation.Invoke(neuron1.InputValue);

            NeuronBase neuron2 = new Neuron();
            neuron2.InputValue = 0.5;
            neuron2.Value = neuron2.ForwardCalculation.Invoke(neuron2.InputValue);

            Assert.AreNotEqual(neuron1.GetHashCode(), neuron2.GetHashCode());
        }
    }
}