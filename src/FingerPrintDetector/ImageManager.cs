using System;
using System.IO;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing; // Untuk menggunakan metode Mutate()
using System.Collections.Generic;

namespace FingerPrintDetector
{
    public static class ImageManager{
        public static string ConvertImageToAscii8Bit(string imagePath, int pattern){
        using (Image<Rgba32> image = Image.Load<Rgba32>(imagePath)) {
            // Mengonversi gambar ke grayscale
            image.Mutate(x => x.Grayscale());

            // Deteksi batas area sidik jari
            int left = image.Width;
            int right = 0;
            int top = image.Height;
            int bottom = 0;

            // Iterasi melalui setiap piksel untuk menemukan batas area sidik jari
            for (int y = 0; y < image.Height; y++) {
                for (int x = 0; x < image.Width; x++) {
                    Rgba32 pixel = image[x, y];
                    byte intensity = (byte)((pixel.R + pixel.G + pixel.B) / 3);

                    if (intensity < 255) { 
                        if (x < left) left = x;
                        if (x > right) right = x;
                        if (y < top) top = y;
                        if (y > bottom) bottom = y;
                    }
                }
            }

            // Memastikan area yang valid untuk cropping
            if (left < right && top < bottom) {
                var cropRectangle = new Rectangle(left, top, right - left + 1, bottom - top + 1);
                image.Mutate(x => x.Crop(cropRectangle));
            }

            // Menggunakan StringBuilder untuk mengumpulkan hasil ASCII
            StringBuilder asciiArtBuilder = new StringBuilder();

            int crop = 0;
            bool done = false;
            // Iterasi melalui setiap piksel dan mengonversinya ke ASCII 8-bit
           
            for (int y = 0; y < image.Height; y++) {
                 if(crop>60){
                    break;
                }
                for (int x = 0; x < image.Width; x++) {      

                    Rgba32 pixel = image[x, y];
                    byte intensity = (byte)((pixel.R + pixel.G + pixel.B) / 3); 
                    // Mengonversi intensitas piksel ke biner 8-bit
                    string binaryValue = Convert.ToString(intensity, 2).PadLeft(8, '0');

                    if (binaryValue != "10100000" && binaryValue != "01101001" && binaryValue != "00000000" && binaryValue != "11111111"){
                        // Mengonversi biner ke karakter ASCII 8-bit
                        char asciiChar = Convert.ToChar(Convert.ToByte(binaryValue, 2));
                        asciiArtBuilder.Append(asciiChar);
                        if(pattern==1){
                            crop += 1;
                        }
                    }
    

                     if(crop>60){
                        break;
                    }
                }
            }

            if (crop<=60){
                while (crop<=60){
                    asciiArtBuilder.Append("");
                    crop +=1;
                }
            }
        

            // Mengembalikan hasil ASCII dalam bentuk string
            return asciiArtBuilder.ToString();
        }
    }



        public static string ImagetoAscii(string imagePath, int pattern)
        {
            try
            {
              
                string asciiArt = ConvertImageToAscii8Bit(imagePath, pattern);

                if (asciiArt != null) {
                    return asciiArt; 
                }
                else {
                    Console.WriteLine("Failed to convert image to ASCII.");
                    return null; 
                }
            }
            catch (Exception ex) {
                Console.WriteLine($"Error converting image to ASCII: {ex.Message}");
                return null; 
            }
        }

        public static List<string> GetAllImagePaths(string directoryPath) {
            List<string> imagePaths = new List<string>();

            // Memeriksa apakah direktori ada
            if (Directory.Exists(directoryPath)) {
                // Mengambil semua file dengan ekstensi gambar dari direktori
                string[] files = Directory.GetFiles(directoryPath, "*.*", SearchOption.AllDirectories);
                
                foreach (string file in files)  {
                    // Memeriksa apakah file memiliki ekstensi gambar yang didukung
                    string extension = Path.GetExtension(file).ToLower();
                    if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                    {
                        imagePaths.Add(file);
                    }
                }
            }
            else   {
                Console.WriteLine($"Direktori '{directoryPath}' tidak ditemukan.");
            }

            return imagePaths;
        }

    }
}
