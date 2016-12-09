using Microsoft.VisualStudio.TestTools.UnitTesting;
using Network.Base;
using Network.Items;

namespace ObsidiaNetworkTests.Base
{
    [TestClass()]
    public class ConnectionBaseTests
    {
        [TestMethod()]
        public void ConnectionBaseTest()
        {
            ConnectionBase connection = new NeuralConnection(0, 1);

            Assert.AreEqual(0, connection.PreviousNeuron);
            Assert.AreEqual(1, connection.NextNeuron);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            ConnectionBase connection1 = new NeuralConnection(0, 1);
            ConnectionBase connection2 = new NeuralConnection(0, 2);
            ConnectionBase connection3 = new NeuralConnection(0, 1) { Multiplier = connection1.Multiplier };

            Assert.IsTrue(connection1.Equals(connection3));
            Assert.IsFalse(connection1.Equals(connection2));
        }

        [TestMethod()]
        public void EqualsTest1()
        {
            ConnectionBase connection1 = new NeuralConnection(0, 1);
            ConnectionBase connection2 = new NeuralConnection(0, 2);
            ConnectionBase connection3 = new NeuralConnection(0, 1) { Multiplier = connection1.Multiplier };

            Assert.IsTrue(connection1 == connection3);
            Assert.IsTrue(connection1 != connection2);
        }

        [TestMethod()]
        public void GetHashCodeTest()
        {
            ConnectionBase connection1 = new NeuralConnection(0, 1);
            ConnectionBase connection2 = new NeuralConnection(0, 2);

            Assert.AreNotEqual(connection1.GetHashCode(), connection2.GetHashCode());
        }
    }
}