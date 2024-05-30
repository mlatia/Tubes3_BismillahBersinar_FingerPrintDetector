using System;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing; // Untuk menggunakan metode Mutate()
using System.Collections.Generic;

namespace FingerPrintDetector
{
    public static class ImageManager
    {
        public static string ConvertImageToAscii8Bit(string imagePath)
    {
        // Menggunakan ImageSharp untuk memuat gambar
        using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath))
        {
            // Mengonversi gambar ke grayscale
            image.Mutate(x => x.Grayscale());

            // Menggunakan StringBuilder untuk mengumpulkan hasil ASCII
            StringBuilder asciiArtBuilder = new StringBuilder();

            // Iterasi melalui setiap piksel dan mengonversinya ke ASCII 8-bit
            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    // Mendapatkan nilai intensitas piksel dalam bentuk byte
                    Rgba32 pixel = image[x, y];
                    byte intensity = (byte)((pixel.R + pixel.G + pixel.B) / 3); // Rata-rata dari R, G, B untuk grayscale

                    // Mengonversi intensitas piksel ke biner 8-bit
                    string binaryValue = Convert.ToString(intensity, 2).PadLeft(8, '0');
    
                    // Mengonversi biner ke karakter ASCII 8-bit
                    char asciiChar = Convert.ToChar(Convert.ToByte(binaryValue, 2));

                    // Menambahkan karakter ASCII ke StringBuilder
                    asciiArtBuilder.Append(asciiChar);
                }
                asciiArtBuilder.AppendLine(); // Tambahkan baris baru setelah setiap baris gambar
            }

            // Mengembalikan hasil ASCII dalam bentuk string
            return asciiArtBuilder.ToString();
        }
    }


        public static string ImagetoAscii(string imagePath)
        {
            try
            {
              
                string asciiArt = ConvertImageToAscii8Bit(imagePath);

                if (asciiArt != null)
                {
                    return asciiArt; // Return ASCII art string
                }
                else
                {
                    Console.WriteLine("Failed to convert image to ASCII.");
                    return null; // Or handle the error as needed
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting image to ASCII: {ex.Message}");
                return null; // Or handle the error as needed
            }
        }

        public static List<string> GetAllImagePaths(string directoryPath)
        {
            List<string> imagePaths = new List<string>();

            // Memeriksa apakah direktori ada
            if (Directory.Exists(directoryPath))
            {
                // Mengambil semua file dengan ekstensi gambar dari direktori
                string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
                
                foreach (string file in files)
                {
                    // Memeriksa apakah file memiliki ekstensi gambar yang didukung
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                    {
                        imagePaths.Add(file);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Direktori '{directoryPath}' tidak ditemukan.");
            }

            return imagePaths;
        }

    }
}
