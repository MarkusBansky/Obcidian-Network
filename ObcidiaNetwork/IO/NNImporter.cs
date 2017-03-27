using System.IO;
using System.Security.Cryptography;
using ObcidiaNetwork.Controllers;
// ReSharper disable AssignNullToNotNullAttribute
// ReSharper disable PossibleNullReferenceException

namespace ObcidiaNetwork.IO
{
    /// <summary>
    /// Creates local importer class.
    /// </summary>
    internal class NnImporter
    {
        /// <summary>
        /// Controller instance.
        /// </summary>
        private NeuronsController _controller;

        /// <summary>
        /// Importer constructor.
        /// </summary>
        /// <param name="controller"></param>
        public NnImporter (NeuronsController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Imports neural network from specific file.
        /// </summary>
        /// <param name="pathToFile"></param>
        /// <returns></returns>
        public NeuronsController Import (string pathToFile)
        {
            // The encryption vectors
            byte[] key = { 49, 73, 39, 75, 39, 37, 60, 35, 50, 43, 50, 45, 50, 46, 50, 47 };
            byte[] iv = { 43, 35, 45, 36, 38, 33, 57, 35, 44, 82, 43, 47, 64, 58, 42, 38 };

            // Build the encryption mathematician
            using (var dataStream = new FileStream (pathToFile, FileMode.Open, FileAccess.Read))
            using (var encryption = new TripleDESCryptoServiceProvider ())
            using (var transform = encryption.CreateDecryptor (key, iv))
            using (Stream encryptedOutputStream = new CryptoStream (dataStream, transform, CryptoStreamMode.Read))
            using (var reader = new StreamReader (encryptedOutputStream))
            {
                var inputsCount = int.Parse(reader.ReadLine());
                var hiddenCount = int.Parse (reader.ReadLine ());
                var outputCount = int.Parse (reader.ReadLine ());

                _controller = new NeuronsController(inputsCount, hiddenCount, outputCount);

                for (var n = 0; n < hiddenCount; n++)
                {
                    var neuronInfoLine = reader.ReadLine().Split('{')[1];
                    var neuronInfoStrings = neuronInfoLine.Split('}')[0].Split(':');

                    _controller.HiddenLayer[n].Bias = double.Parse(neuronInfoStrings[0]);
                    _controller.HiddenLayer[n].BiasDelta = double.Parse (neuronInfoStrings[1]);
                    _controller.HiddenLayer[n].Gradient = double.Parse (neuronInfoStrings[2]);

                    var connectionsStrings = neuronInfoLine.Split('}')[1].Split(';');
                    for (var c = 0; c < connectionsStrings.Length - 1; c++)
                    {
                        _controller.HiddenLayer[n].InputConnections[c].Weight = double.Parse(connectionsStrings[c].Split(':')[0]);
                        _controller.HiddenLayer[n].InputConnections[c].WeightDelta = double.Parse (connectionsStrings[c].Split (':')[1]);
                    }
                }

                reader.ReadLine();

                for (var n = 0; n < outputCount; n++)
                {
                    var neuronInfoLine = reader.ReadLine ().Split ('{')[1];
                    var neuronInfoStrings = neuronInfoLine.Split ('}')[0].Split (':');

                    _controller.OutputLayer[n].Bias = double.Parse (neuronInfoStrings[0]);
                    _controller.OutputLayer[n].BiasDelta = double.Parse (neuronInfoStrings[1]);
                    _controller.OutputLayer[n].Gradient = double.Parse (neuronInfoStrings[2]);

                    var connectionsStrings = neuronInfoLine.Split ('}')[1].Split (';');
                    for (var c = 0; c < connectionsStrings.Length - 1; c++)
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