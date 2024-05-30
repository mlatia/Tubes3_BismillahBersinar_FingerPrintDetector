using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fingerprint_Detection
{
    public static class KMPAlgorithm
    {
        public static int Search(string text, string pattern)
        {
            int[] lps = ComputeLPSArray(pattern);
            int i = 0;
            int j = 0;

            while (i < text.Length)
            {
                if (pattern[j] == text[i])
                {
                    i++;
                    j++;
                }

                if (j == pattern.Length)
                {
                    return i - j; // Match found
                }
                else if (i < text.Length && pattern[j] != text[i])
                {
                    if (j != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }
            return -1; // No match found
        }

        private static int[] ComputeLPSArray(string pattern)
        {
            int length = 0;
            int i = 1;
            int[] lps = new int[pattern.Length];
            lps[0] = 0;

            while (i < pattern.Length)
            {
                if (pattern[i] == pattern[length])
                {
                    length++;
                    lps[i] = length;
                    i++;
                }
                else
                {
                    if (length != 0)
                    {
                        length = lps[length - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }
            return lps;
        }
    }
}
