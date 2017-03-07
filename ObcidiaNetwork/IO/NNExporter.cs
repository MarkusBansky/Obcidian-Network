using System.IO;
using System.Security.Cryptography;
using ObcidiaNetwork.Base;
using ObcidiaNetwork.Controllers;

namespace ObcidiaNetwork.IO
{
    internal class NnExporter
    {
        private readonly NeuronsController _controller;

        public NnExporter(NeuronsController controller)
        {
            _controller = controller;
        }

        public void Export(string pathToFile)
        {
            // The encryption vectors
            byte[] key = { 19, 43, 19, 45, 19, 97, 20, 2, 20, 13, 20, 15, 20, 16, 20, 17 };
            byte[] iv =  { 43, 15, 45, 16, 18, 92, 57, 5, 14, 82, 43, 17, 64, 28, 42, 38 };

            // Build the encryption mathematician
            using(FileStream dataStream = new FileStream(pathToFile, FileMode.Create, FileAccess.Write))
            using (TripleDESCryptoServiceProvider encryption = new TripleDESCryptoServiceProvider ())
            using (ICryptoTransform transform = encryption.CreateEncryptor (key, iv))
            using (Stream encryptedOutputStream = new CryptoStream (dataStream, transform, CryptoStreamMode.Write))
            using (StreamWriter writer = new StreamWriter (encryptedOutputStream))
            {
                writer.WriteLine (_controller.InputLayer.Count);
                writer.WriteLine (_controller.HiddenLayer.Count);
                writer.WriteLine (_controller.OutputLayer.Count);

                foreach (Neuron n in _controller.HiddenLayer)
                {
                    writer.Write("{" + n.Bias + ":" + n.BiasDelta + ":" + n.Gradient + "}");
                    foreach (Connection c in n.InputConnections)
                        writer.Write(c.Weight + ":" + c.WeightDelta + ";");
                    writer.WriteLine ();
                }

                writer.WriteLine();

                foreach (Neuron n in _controller.OutputLayer)
                {
                    writer.Write ("{" + n.Bias + ":" + n.BiasDelta + ":" + n.Gradient + "}");
                    foreach (Connection c in n.InputConnections)
                        writer.Write (c.Weight + ":" + c.WeightDelta + ";");
                    writer.WriteLine();
                }
            }
        }
    }
}