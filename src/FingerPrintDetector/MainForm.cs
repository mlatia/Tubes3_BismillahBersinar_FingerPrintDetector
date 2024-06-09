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
            // this.Size = new Size(800, 400);
        
            // Non-resizable form
            // this.MaximizeBox = false;
            this.MinimizeBox = false;

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
            if (string.IsNullOrEmpty(imagePath)) {
                MessageBox.Show("Please upload an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SearchButton.Enabled = false;
            Console.WriteLine("Search button clicked");

            string inputFingerprint = ImageManager.ImagetoAscii("assets/input.BMP",1);
            string allFingerprint = ImageManager.ImagetoAscii("assets/input.BMP",0);
            Console.WriteLine($"Input fingerprint: {inputFingerprint}");

            string inputFingerprint2 = ImageManager.ImagetoAscii("assets/input.BMP",0);
            int length = inputFingerprint2.Length;
            // Mengambil substring 30 karakter dari belakang
            string inputend = inputFingerprint2.Substring(Math.Max(0, length - 30));

            List<string> database = ImageManager.GetAllImagePaths("test/real");


            await Task.Run(() =>
            {
             string selectedAlgorithm = AlgorithmComboBox.SelectedItem.ToString();
                Tuple<string, float> result, result1, result2;

                string mostSimilar;
                float distance;

                var stopwatch = Stopwatch.StartNew();
                if (selectedAlgorithm == "BM") {
                    result1 = BM.FindMostSimilarFingerprint(inputFingerprint, database);
                    result2 = BM.FindMostSimilarFingerprint(inputend, database);
                }
                else {
                    result1 = KMP.FindMostSimilarFingerprint(inputFingerprint, database);
                    result2 = KMP.FindMostSimilarFingerprint(inputend, database);
                }

                float distance1 = result1.Item2;
                float distance2 = result2.Item2;
                if(distance1 < distance2){
                    distance = distance1;
                    mostSimilar = result1.Item1;
                } else{
                    distance = distance2;
                    mostSimilar = result2.Item1;
                }

                stopwatch.Stop(); 
                long waktu = stopwatch.ElapsedMilliseconds;

            
                Console.WriteLine($"Most similar fingerprint: {mostSimilar}, Distance: {distance}");
                float similarity = (1 - (float)distance) * 100;
                Console.WriteLine($"similarity: {similarity}");

                string personName = Database.GetPersonNameByImagePath(mostSimilar);
                Console.WriteLine($"Person name: {personName}");
                // Handle Bahasa Alay
                string alayPersonName = RegexHelper.GetAlayName(personName);
                var biodata = Database.GetBiodataByName(alayPersonName);


                this.Invoke((Action)(() =>{

                    SearchTimeText.Text = $"Waktu Pencarian: {waktu} ms";
                     // Jika similarity di bawah 80, tampilkan popup tidak ditemukan sidik jari yang cocok
                    if (similarity < 50) {
                        MatchPercentageText.Text = $"Persentase Kecocokkan: Not Found";
                        SimilarImagePictureBox.Image = null;

                        // Update Biodata
                        NIKLabel.Text = $"NIK: ";
                        NamaLabel.Text = $"Nama: ";
                        LahirLabel.Text = $"Tempat Lahir: ";
                        TanggalLabel.Text = $"Tanggal Lahir: ";
                        KelaminLabel.Text = $"Jenis Kelamin: ";
                        GoldarLabel.Text = $"Golongan Darah: ";
                        AlamatLabel.Text = $"Alamat: ";
                        AgamaLabel.Text = $"Agama: ";
                        StatusLabel.Text = $"Status Perkawinan: ";
                        KerjaLabel.Text = $"Pekerjaan: ";
                        KewarganegaraanLabel.Text = $"Kewarganegaraan: ";
                         MessageBox.Show("Tidak ditemukan sidik jari yang cocok.", "Informasi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Tampilkan persentase kecocokan dan biodata jika similarity di atas atau sama dengan 80
                        MatchPercentageText.Text = $"Persentase Kecocokkan: {similarity}%";
                        SimilarImagePictureBox.Image = Image.FromFile(mostSimilar);

                        // Update Biodata
                        NIKLabel.Text = $"NIK: {biodata.NIK}";
                        NamaLabel.Text = $"Nama: {personName}";
                        LahirLabel.Text = $"Tempat Lahir: {biodata.TempatLahir}";
                        TanggalLabel.Text = $"Tanggal Lahir: {biodata.TanggalLahir.ToShortDateString()}";
                        KelaminLabel.Text = $"Jenis Kelamin: {biodata.JenisKelamin}";
                        GoldarLabel.Text = $"Golongan Darah: {biodata.GolonganDarah}";
                        AlamatLabel.Text = $"Alamat: {biodata.Alamat}";
                        AgamaLabel.Text = $"Agama: {biodata.Agama}";
                        StatusLabel.Text = $"Status Perkawinan: {biodata.StatusPerkawinan}";
                        KerjaLabel.Text = $"Pekerjaan: {biodata.Pekerjaan}";
                        KewarganegaraanLabel.Text = $"Kewarganegaraan: {biodata.Kewarganegaraan}";
                    }

                    SearchButton.Enabled = true;
                    Console.WriteLine("Search button re-enabled");
                }));
            });
        }

    }

}
