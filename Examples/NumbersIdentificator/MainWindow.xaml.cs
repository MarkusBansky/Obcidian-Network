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
            _network = new NeuralNetwork(256, 128, 10);
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

            double[] outputValues = _network.Calculate(inputValues.Where((x, i) => i % 4 == 1).Select(x => x / 255.0).ToArray());

            var pairs = outputValues.Select((x, i) => new KeyValuePair<double, int>(x, i)).OrderByDescending(p => p.Key).ToArray();
            foreach(var pair in pairs)
            {
                outputTextBox.Text += $"[{pair.Value}] : {pair.Key:F}\n";
            }
        }

        private void trainButton_Click (object sender, RoutedEventArgs e)
        {
            List<KeyValuePair<double[], double[]>> trainingPairs = new List<KeyValuePair<double[], double[]>>();

            foreach (var path in Directory.GetFiles("data"))
            {
                BitmapImage bitmapImage = new BitmapImage(new Uri(Path.GetFullPath(path)));
                int nStride = (bitmapImage.PixelWidth * bitmapImage.Format.BitsPerPixel + 7) / 8;
                byte[] trainingValues = new byte[bitmapImage.PixelHeight * nStride];
                bitmapImage.CopyPixels(trainingValues, nStride, 0);

                double[] resultValues = new double[10];
                for (int i = 0; i < 10; i++)
                    resultValues[i] = 0;
                resultValues[int.Parse(path.Substring(5, 1))] = 1;

                trainingPairs.Add(
                    new KeyValuePair<double[], double[]>(
                        trainingValues.Where((x, i) => i % 4 == 1).Select(x => x / 255.0).ToArray(), resultValues));
            }
            _network.Train(trainingPairs.ToArray(), 0.1);
        }

        private void trainButton_Copy1_Click (object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog
            {
                FileName = "network.dat",
                Filter = "Dat files (*.dat)|*.dat"
            };

            if (saveFile.ShowDialog () == true)
            {
                _network.ExportToFile (Path.GetFullPath (saveFile.FileName));
            }
        }

        private void importButton_Click (object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog
            {
                Filter = "Dat files (*.dat)|*.dat"
            };

            if (openFile.ShowDialog () == true)
            {
                _network.ImportFromFile (Path.GetFullPath (openFile.FileName));
            }
        }
    }
}
