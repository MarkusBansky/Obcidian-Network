using System.IO;
using System.Security.Cryptography;
using ObcidiaNetwork.Controllers;

namespace ObcidiaNetwork.IO
{
    /// <summary>
    /// Creates instance of exporter.
    /// </summary>
    internal class NnExporter
    {
        /// <summary>
        /// Controller instance.
        /// </summary>
        private readonly NeuronsController _controller;

        /// <summary>
        /// Exporter contructor.
        /// </summary>
        /// <param name="controller"></param>
        public NnExporter(NeuronsController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Exports neural network to specific file.
        /// </summary>
        /// <param name="pathToFile"></param>
        public void Export(string pathToFile)
        {
            // The encryption vectors
            byte[] key = { 49, 73, 39, 75, 39, 37, 60, 35, 50, 43, 50, 45, 50, 46, 50, 47 };
            byte[] iv =  { 43, 35, 45, 36, 38, 33, 57, 35, 44, 82, 43, 47, 64, 58, 42, 38 };

            // Build the encryption mathematician
            using (var dataStream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write))
            using (var encryption = new TripleDESCryptoServiceProvider ())
            using (var transform = encryption.CreateEncryptor (key, iv))
            using (Stream encryptedOutputStream = new CryptoStream (dataStream, transform, CryptoStreamMode.Write))
            using (var writer = new StreamWriter (encryptedOutputStream))
            {
                writer.WriteLine (_controller.InputLayer.Count);
                writer.WriteLine (_controller.HiddenLayers.Count);
                writer.WriteLine (_controller.HiddenLayers[0].Neurons.Count);
                writer.WriteLine (_controller.OutputLayer.Count);

                foreach (var l in _controller.HiddenLayers)
                {
                    foreach (var n in l.Neurons)
                    {
                        writer.Write("{" + n.Bias + ":" + n.BiasDelta + ":" + n.Gradient + "}");
                        foreach (var c in n.InputConnections)
                            writer.Write(c.Weight + ":" + c.WeightDelta + ";");
                        writer.WriteLine();
                    }
                }

                writer.WriteLine();

                foreach (var n in _controller.OutputLayer)
                {
                    writer.Write ("{" + n.Bias + ":" + n.BiasDelta + ":" + n.Gradient + "}");
                    foreach (var c in n.InputConnections)
                        writer.Write (c.Weight + ":" + c.WeightDelta + ";");
                    writer.WriteLine();
                }
            }
        }
    }
}