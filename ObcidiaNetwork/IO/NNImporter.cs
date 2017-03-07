using System.IO;
using System.Security.Cryptography;
using ObcidiaNetwork.Base;
using ObcidiaNetwork.Controllers;
// ReSharper disable AssignNullToNotNullAttribute

namespace ObcidiaNetwork.IO
{
    internal class NnImporter
    {
        private NeuronsController _controller;

        public NnImporter (NeuronsController controller)
        {
            _controller = controller;
        }

        public NeuronsController Import (string pathToFile)
        {
            // The encryption vectors
            byte[] key = { 19, 43, 19, 45, 19, 97, 20, 2, 20, 13, 20, 15, 20, 16, 20, 17 };
            byte[] iv = { 43, 15, 45, 16, 18, 92, 57, 5, 14, 82, 43, 17, 64, 28, 42, 38 };

            // Build the encryption mathematician
            using (FileStream dataStream = new FileStream (pathToFile, FileMode.Open, FileAccess.Read))
            using (TripleDESCryptoServiceProvider encryption = new TripleDESCryptoServiceProvider ())
            using (ICryptoTransform transform = encryption.CreateDecryptor (key, iv))
            using (Stream encryptedOutputStream = new CryptoStream (dataStream, transform, CryptoStreamMode.Read))
            using (StreamReader reader = new StreamReader (encryptedOutputStream))
            {
                int inputsCount = int.Parse(reader.ReadLine());
                int hiddenCount = int.Parse (reader.ReadLine ());
                int outputCount = int.Parse (reader.ReadLine ());

                _controller = new NeuronsController(inputsCount, hiddenCount, outputCount);

                for (int n = 0; n < hiddenCount; n++)
                {
                    string neuronInfoLine = reader.ReadLine().Split('{')[1];
                    string[] neuronInfoStrings = neuronInfoLine.Split('}')[0].Split(':');

                    _controller.HiddenLayer[n].Bias = double.Parse(neuronInfoStrings[0]);
                    _controller.HiddenLayer[n].BiasDelta = double.Parse (neuronInfoStrings[1]);
                    _controller.HiddenLayer[n].Gradient = double.Parse (neuronInfoStrings[2]);

                    string[] connectionsStrings = neuronInfoLine.Split('}')[1].Split(';');
                    for (int c = 0; c < connectionsStrings.Length - 1; c++)
                    {
                        _controller.HiddenLayer[n].InputConnections[c].Weight = double.Parse(connectionsStrings[c].Split(':')[0]);
                        _controller.HiddenLayer[n].InputConnections[c].WeightDelta = double.Parse (connectionsStrings[c].Split (':')[1]);
                    }
                }

                reader.ReadLine();

                for (int n = 0; n < outputCount; n++)
                {
                    string neuronInfoLine = reader.ReadLine ().Split ('{')[1];
                    string[] neuronInfoStrings = neuronInfoLine.Split ('}')[0].Split (':');

                    _controller.OutputLayer[n].Bias = double.Parse (neuronInfoStrings[0]);
                    _controller.OutputLayer[n].BiasDelta = double.Parse (neuronInfoStrings[1]);
                    _controller.OutputLayer[n].Gradient = double.Parse (neuronInfoStrings[2]);

                    string[] connectionsStrings = neuronInfoLine.Split ('}')[1].Split (';');
                    for (int c = 0; c < connectionsStrings.Length - 1; c++)
                    {
                        _controller.OutputLayer[n].InputConnections[c].Weight = double.Parse (connectionsStrings[c].Split (':')[0]);
                        _controller.OutputLayer[n].InputConnections[c].WeightDelta = double.Parse (connectionsStrings[c].Split (':')[1]);
                    }
                }
            }

            return _controller;
        }
    }
}