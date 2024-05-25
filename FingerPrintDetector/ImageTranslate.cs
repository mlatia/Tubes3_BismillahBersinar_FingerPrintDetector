using System;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

class ImageTranslate
{
    const int BITS_PER_BYTE = 8;

    // Fungsi untuk mengonversi byte ke urutan biner
    static string ByteToBinary(byte b)
    {
        return Convert.ToString(b, 2).PadLeft(BITS_PER_BYTE, '0');
    }

    // Fungsi untuk mengonversi gambar ke urutan biner
    static string ImageToBinary(string imagePath)
    {
        using (Image<L8> image = Image.Load<L8>(imagePath))
        {
            string binaryString = "";

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    L8 pixel = image[x, y];
                    binaryString += ByteToBinary(pixel.PackedValue);
                }
            }

            return binaryString;
        }
    }

    // Fungsi untuk mengonversi urutan biner menjadi teks ASCII
    static string BinaryToAscii(string binary)
    {
        string ascii = "";

        for (int i = 0; i < binary.Length; i += BITS_PER_BYTE)
        {
            string binaryByte = binary.Substring(i, BITS_PER_BYTE);
            int asciiValue = Convert.ToInt32(binaryByte, 2);
            ascii += (char)asciiValue;
        }

        return ascii;
    }

    public static string ImagetoAscii(string image){
         // Mengonversi gambar ke urutan biner
        string binaryString = ImageToBinary(image);

        // Mengonversi urutan biner menjadi teks ASCII
        string asciiText = BinaryToAscii(binaryString);
        
        return asciiText;
    }
    
}
