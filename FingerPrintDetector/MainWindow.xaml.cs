using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging; // Add this using directive
using Microsoft.Win32;
using System.IO; // Add this using directive
using System.Collections.Generic;

namespace FingerPrintDetector
{
    public partial class MainWindow : Window
    {
        private string imagePath;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (openFileDialog.ShowDialog() == true)
            {
                // Mendapatkan path file gambar yang dipilih
                imagePath = openFileDialog.FileName;

                // Menyalin file gambar ke folder tujuan di dalam proyek
                string destinationFolder = ""; // Folder tujuan di dalam proyek
                string fileName = "input.png";
                string destinationPath = Path.Combine(destinationFolder, fileName);
                File.Copy(imagePath, destinationPath, true);
                BitmapImage bitmap = new BitmapImage(new Uri(imagePath));
                UploadedImage.Source = bitmap;
            }
                
        }
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            UploadedImage.Source = null;
            imagePath = null;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string inputFingerprint = ImageTranslate.ImagetoAscii("input.png"); // Perbaikan: Gunakan ImagetoAscii, bukan ImageToAscii
            List<string> database = new List<string> { inputFingerprint };

            Tuple<string, int> result = KMP.FindMostSimilarFingerprint(inputFingerprint, database);
            string mostSimilar = result.Item1;
            int distance = result.Item2;
        }
    }
}
