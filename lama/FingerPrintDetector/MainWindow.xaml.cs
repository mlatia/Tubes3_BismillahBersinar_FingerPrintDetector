using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.Win32;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                string destinationFolder = "assets"; // Folder tujuan di dalam proyek
                string fileName = "input.png";
                string destinationPath = Path.Combine(destinationFolder, fileName);

                // Buat folder jika belum ada
                if (!Directory.Exists(destinationFolder))
                {
                    Directory.CreateDirectory(destinationFolder);
                }

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

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Show a loading indicator
            // (You can implement this with a progress bar, status message, etc.)
            // For simplicity, let's disable the Search button while processing.
            SearchButton.IsEnabled = false;

            string inputFingerprint = ImageTranslate.ImagetoAscii("assets/input.png");
            // Remove MessageBox, it might cause UI blocking

            List<string> database = new List<string> { inputFingerprint };

            // Let's execute the search operation asynchronously
            await Task.Run(() =>
            {
                Tuple<string, int> result = KMP.FindMostSimilarFingerprint(inputFingerprint, database);
                string mostSimilar = result.Item1;
                int distance = result.Item2;

                // Once the search is done, update UI
                Dispatcher.Invoke(() =>
                {
                    // Update UI with the search result
                    // (You can display the result in a label, textbox, etc.)
                    // For simplicity, let's show a message box.
                    MessageBox.Show($"Most similar fingerprint: {mostSimilar}, Distance: {distance}");

                    // Re-enable the Search button
                    SearchButton.IsEnabled = true;

                    // Update other UI elements
                    // Assuming database contains other information related to the fingerprint
                    // NIKText.Text = ...;
                    // NamaText.Text = ...;
                    // TTLText.Text = ...;
                    // JenisKelaminText.Text = ...;
                    // GolonganDarahText.Text = ...;
                    // AlamatText.Text = ...;
                    // AgamaText.Text = ...;
                    // StatusPerkawinanText.Text = ...;
                    // PekerjaanText.Text = ...;
                    // KewarganegaraanText.Text = ...;

                    // Example: updating search time and match percentage
                    // SearchTimeText.Text = $"Waktu Pencarian: {searchTime} ms";
                    // MatchPercentageText.Text = $"Persentase Kecocokkan: {matchPercentage}%";
                });
            });
        }
    }
}
