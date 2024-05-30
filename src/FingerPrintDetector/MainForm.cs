using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        
                Tuple<string, int> result = KMP.FindMostSimilarFingerprint(inputFingerprint, database);
                string mostSimilar = result.Item1;
                int distance = result.Item2;
                SimilarImagePictureBox.Image = Image.FromFile(mostSimilar);
            
                Console.WriteLine($"Most similar fingerprint: {mostSimilar}, Distance: {distance}");

                this.Invoke((Action)(() =>
                {
    

                    Console.WriteLine("Input fingerprint:");
                    MessageBox.Show($"Most similar fingerprint: {mostSimilar}, Distance: {distance}");
                    SearchButton.Enabled = true;
                    Console.WriteLine("Search button re-enabled");
                }));
            });
        }
    }

}
