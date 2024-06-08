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

        public static int CalculateLevenshteinDistance(string text1, string text2)
        {
            int m = text1.Length;
            int n = text2.Length;
            
            int[,] dp = new int[m + 1, n + 1];
            
            for (int i = 0; i <= m; i++)
            {
                dp[i, 0] = i;
            }
            
            for (int j = 0; j <= n; j++)
            {
                dp[0, j] = j;
            }
            
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    int cost = (text1[i - 1] == text2[j - 1]) ? 0 : 1;
                    
                    dp[i, j] = Math.Min(Math.Min(
                        dp[i - 1, j] + 1,     // Deletion
                        dp[i, j - 1] + 1),    // Insertion
                        dp[i - 1, j - 1] + cost); // Substitution
                }
            }
            
            return dp[m, n];
        }
    }
}
