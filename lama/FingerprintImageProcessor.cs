using System;
using System.Text;
using System.Windows.Media;
using System.Drawing;

namespace Fingerprint_Detection
{

    public static class FingerprintImageProcessor
    {
        public static string ConvertImageToAscii(string imagePath, int startX, int startY, int width, int height)
        {
            using (Bitmap bitmap = new Bitmap(imagePath))
            {
                StringBuilder binaryBuilder = new StringBuilder();

                for (int y = startY; y < startY + height && y < bitmap.Height; y++)
                {
                    for (int x = startX; x < startX + width && x < bitmap.Width; x++)
                    {
                        System.Drawing.Color pixelColor = bitmap.GetPixel(x, y);  // Use System.Drawing.Color
                        int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);
                        string binaryValue = Convert.ToString(grayValue, 2).PadLeft(8, '0');
                        binaryBuilder.Append(binaryValue);
                    }
                }

                string binaryString = binaryBuilder.ToString();
                StringBuilder asciiBuilder = new StringBuilder();

                for (int i = 0; i < binaryString.Length; i += 8)
                {
                    if (i + 8 <= binaryString.Length)
                    {
                        string byteString = binaryString.Substring(i, 8);
                        byte byteValue = Convert.ToByte(byteString, 2);
                        char asciiChar = Convert.ToChar(byteValue);
                        asciiBuilder.Append(asciiChar);
                    }
                }

                return asciiBuilder.ToString();
            }
        }
    }

}
