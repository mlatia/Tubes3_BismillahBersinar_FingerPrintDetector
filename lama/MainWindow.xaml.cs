using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Imaging;

namespace Fingerprint_Detection
{
    public partial class MainWindow : Window
    {
        private DatabaseController dbHelper = new DatabaseController();
        private bool useBM = true;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void PilihCitra_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                BitmapImage bitmap = new BitmapImage(new Uri(filePath));
                InputFingerprintImage.Source = bitmap;
                // Convert the image to ASCII string for processing
                string inputFingerprint = ConvertImageToAsciiString(filePath);
                // Assuming inputFingerprintTextBox is a TextBox in your XAML
                // inputFingerprintTextBox.Text = inputFingerprint; 
            }
        }

        private void BMButton_Checked(object sender, RoutedEventArgs e)
        {
            KMPButton.IsChecked = false;
            useBM = true;
        }

        private void BMButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (KMPButton.IsChecked == false)
            {
                BMButton.IsChecked = true; // Ensure one algorithm is always selected
            }
        }

        private void KMPButton_Checked(object sender, RoutedEventArgs e)
        {
            BMButton.IsChecked = false;
            useBM = false;
        }

        private void KMPButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (BMButton.IsChecked == false)
            {
                KMPButton.IsChecked = true; // Ensure one algorithm is always selected
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            //string inputFingerprint = inputFingerprintTextBox.Text;
            //List<Biodata> allBiodata = dbHelper.GetAllBiodata();
            //List<string> storedFingerprints = new List<string>();

            //foreach (var biodata in allBiodata)
            //{
            //    string fingerprint = dbHelper.GetFingerprintByNIK(biodata.NIK);
            //    if (fingerprint != null)
            //    {
            //        storedFingerprints.Add(fingerprint);
            //    }
            //}

            //string closestMatchFingerprint = useBM
            //    ? FindClosestFingerprintMatchBM(inputFingerprint, storedFingerprints)
            //    : FindClosestFingerprintMatchKMP(inputFingerprint, storedFingerprints);

            //if (closestMatchFingerprint != null)
            //{
            //    DisplayBiodata(closestMatchFingerprint);
            //}
            //else
            //{
            //    MessageBox.Show("No matching fingerprint found.");
            //}
        }

        private void DisplayBiodata(string closestMatchFingerprint)
        {
            //Biodata matchedBiodata = null;
            //foreach (var biodata in dbHelper.GetAllBiodata())
            //{
            //    if (dbHelper.GetFingerprintByNIK(biodata.NIK) == closestMatchFingerprint)
            //    {
            //        matchedBiodata = biodata;
            //        break;
            //    }
            //}

            //if (matchedBiodata != null)
            //{
            //    NIKText.Text = matchedBiodata.NIK;
            //    NamaText.Text = matchedBiodata.Nama;
            //    TTLText.Text = $"{matchedBiodata.TempatLahir}, {matchedBiodata.TanggalLahir.ToShortDateString()}";
            //    JenisKelaminText.Text = matchedBiodata.JenisKelamin;
            //    GolonganDarahText.Text = matchedBiodata.GolonganDarah;
            //    AlamatText.Text = matchedBiodata.Alamat;
            //    AgamaText.Text = matchedBiodata.Agama;
            //    StatusPerkawinanText.Text = matchedBiodata.StatusPerkawinan;
            //    PekerjaanText.Text = matchedBiodata.Pekerjaan;
            //    KewarganegaraanText.Text = matchedBiodata.Kewarganegaraan;
            //}
            //else
            //{
            //    MessageBox.Show("No biodata found for the matched fingerprint.");
            //}
        }

        private string ConvertImageToAsciiString(string imagePath)
        {
            // Implement the method to convert the image at the specified path to an ASCII string
            // For now, return a dummy fingerprint string
            return "dummy_fingerprint_string";
        }

        private string FindClosestFingerprintMatchBM(string inputFingerprint, List<string> storedFingerprints)
        {
            string closestMatch = null;
            int minDistance = int.MaxValue;

            foreach (var storedFingerprint in storedFingerprints)
            {
                int distance = BoyerMoore.Compare(storedFingerprint, inputFingerprint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMatch = storedFingerprint;
                }
            }

            return closestMatch;
        }

        private string FindClosestFingerprintMatchKMP(string inputFingerprint, List<string> storedFingerprints)
        {
            string closestMatch = null;
            int minDistance = int.MaxValue;

            foreach (var storedFingerprint in storedFingerprints)
            {
                int distance = KMPAlgorithm.Search(storedFingerprint, inputFingerprint);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMatch = storedFingerprint;
                }
            }

            return closestMatch;
        }
    }
}
