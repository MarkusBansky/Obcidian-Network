using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ObcidiaNetwork;

namespace NumbersIdentificator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NeuralNetwork _network;

        public MainWindow ()
        {
            InitializeComponent ();
            _network = new NeuralNetwork(256, 256, 10, true);
        }

        private void chooseButton_Click (object sender, RoutedEventArgs e)
        {
            outputTextBox.Text = "";

            BitmapImage bitmapImage = null;
            OpenFileDialog openFileDialog = new OpenFileDialog ();
            if (openFileDialog.ShowDialog () == true)
                bitmapImage = new BitmapImage (new Uri (Path.GetFullPath (openFileDialog.FileName)));

            inputImage.Source = bitmapImage;
            int nStride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
            byte[] inputValues = new byte[bitmapImage.PixelHeight * nStride];
            bitmapImage.CopyPixels (inputValues, nStride, 0);

            float[] outputValues = _network.CalculateOutputs(inputValues.Where((x, i) => i % 4 == 1).Select(x => x / 255f).ToArray());

            var pairs = outputValues.Select((x, i) => new KeyValuePair<float, int>(x, i)).OrderByDescending(p => p.Key).ToArray();
            foreach(var pair in pairs)
            {
                outputTextBox.Text += $"[{pair.Value}] : {pair.Key:R}\n";
            }
        }

        private void trainButton_Click (object sender, RoutedEventArgs e)
        {
            for (int j = 0; j < 50; j++)
            {
                foreach (var path in Directory.GetFiles("data"))
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(Path.GetFullPath(path)));
                    int nStride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
                    byte[] trainingValues = new byte[bitmapImage.PixelHeight * nStride];
                    bitmapImage.CopyPixels(trainingValues, nStride, 0);

                    int[] resultValues = new int[10];
                    for (int i = 0; i < 10; i++)
                        resultValues[i] = 0;
                    resultValues[int.Parse(path.Substring(5, 1))] = 1;


                    _network.Train(trainingValues.Where((x, i) => i % 4 == 1).Select(x => x / 255f).ToArray(),
                        resultValues.Select(x => (float) x).ToArray());
                    Console.WriteLine ($@"[IMAGE TRAINING] Image path {path}, Image index [{path.Substring (5, 1)}]...");
                }

                Console.WriteLine ($@"[PERFORMING TRAINING COMMAND] Index {j} ended. Starting new {j + 1}...");
            }
            File.WriteAllText ("network_values.dat", _network.ExportJson ());
        }
    }
}
