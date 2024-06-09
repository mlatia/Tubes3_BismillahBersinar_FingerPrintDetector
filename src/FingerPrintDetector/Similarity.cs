using System;

namespace FingerPrintDetector{
    public class Similarity
    {
        public static int CalculateHammingDistance(string text1, string text2)
        {
            if (text1.Length != text2.Length)
            {
                throw new ArgumentException("Strings must be of the same length to calculate Hamming distance.");
            }

            int hammingDistance = 0;

            for (int i = 0; i < text1.Length; i++)
            {
                if (text1[i] != text2[i])
                {
                    hammingDistance++;
                }
            }

            return hammingDistance;
        }
    }
}
