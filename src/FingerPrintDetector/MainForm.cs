using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace FingerPrintDetector
{
    public partial class MainForm : Form
    {
        private string imagePath;

        public MainForm()
        {
            InitializeComponent();
        }

        private void UploadImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.BMP) | *.jpg; *.jpeg; *.png; *.BMP";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    Console.WriteLine($"Image path: {imagePath}");

                    string destinationFolder = "assets";
                    string fileName = "input.BMP";
                    string destinationPath = Path.Combine(destinationFolder, fileName);

                    if (!Directory.Exists(destinationFolder))
                    {
                        Directory.CreateDirectory(destinationFolder);
                        Console.WriteLine($"Created directory: {destinationFolder}");
                    }

                    File.Copy(imagePath, destinationPath, true);
                    Console.WriteLine($"Copied file to: {destinationPath}");

                    UploadedImage.Image = Image.FromFile(imagePath);

                }
            }
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            UploadedImage.Image = null;
            imagePath = null;
            Console.WriteLine("Image and path reset");
        }

        private async void SearchButton_Click(object sender, EventArgs e)
        {
            SearchButton.Enabled = false;
            Console.WriteLine("Search button clicked");

            string inputFingerprint = ImageManager.ImagetoAscii("assets/input.BMP");
            Console.WriteLine($"Input fingerprint: {inputFingerprint}");

            List<string> database = ImageManager.GetAllImagePaths("test/real");

            Console.WriteLine("INI DATABASEEEE");
            Console.WriteLine(database.Count);

            await Task.Run(() =>
            {
             string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
                Tuple<string, int> result;

                var stopwatch = Stopwatch.StartNew();
                if (selectedAlgorithm == "BM") {
                    result = BM.FindMostSimilarFingerprint(inputFingerprint, database);
                }
                else {
                    result = KMP.FindMostSimilarFingerprint(inputFingerprint, database);
                }

                stopwatch.Stop(); 
                long waktu = stopwatch.ElapsedMilliseconds;

                string mostSimilar = result.Item1;
                int distance = result.Item2;
                SimilarImagePictureBox.Image = Image.FromFile(mostSimilar);
            
                Console.WriteLine($"Most similar fingerprint: {mostSimilar}, Distance: {distance}");

                string personName = Database.GetPersonNameByImagePath(mostSimilar);
                var biodata = Database.GetBiodataByName(personName);

                float similarity = (1 - (float)distance / inputFingerprint.Length) * 100;
                Console.WriteLine($"similarity: {similarity}");

                this.Invoke((Action)(() =>{

                    SearchTimeText.Text = $"Waktu Pencarian: {waktu} ms";
                    MatchPercentageText.Text = $"Persentase Kecocokkan: {similarity}%";
                    
                     // Update Biodata
                    NIKLabel.Text = $"NIK: {biodata.NIK}";
                    NamaLabel.Text = $"Nama: {biodata.Nama}";
                    LahirLabel.Text = $"Tempat Lahir: {biodata.TempatLahir}";
                    TanggalLabel.Text = $"Tanggal Lahir: {biodata.TanggalLahir.ToShortDateString()}";
                    KelaminLabel.Text = $"Jenis Kelamin: {biodata.JenisKelamin}";
                    GoldarLabel.Text = $"Golongan Darah: {biodata.GolonganDarah}";
                    AlamatLabel.Text = $"Alamat: {biodata.Alamat}";
                    AgamaLabel.Text = $"Agama: {biodata.Agama}";
                    StatusLabel.Text = $"Status Perkawinan: {biodata.StatusPerkawinan}";
                    KerjaLabel.Text = $"Pekerjaan: {biodata.Pekerjaan}";
                    KewarganegaraanLabel.Text = $"Kewarganegaraan: {biodata.Kewarganegaraan}";

                    SearchButton.Enabled = true;
                    Console.WriteLine("Search button re-enabled");
                }));
            });
        }

    }

}
