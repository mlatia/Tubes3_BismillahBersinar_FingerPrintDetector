using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintDetector
{
    public static class BM
    {
        public static int Compare(string text, string pattern)
        {
            const int MAX_HAMMING_DISTANCE = 10;
            int minHammingDistance = int.MaxValue;

            // Swap if pattern is longer than text
            if (pattern.Length > text.Length)
            {
                string temp = pattern;
                pattern = text;
                text = temp;
            }

            // Initialize lastOccurrence array
            int[] lastOccurrence = new int[256];
            for (int i = 0; i < 256; i++)
                lastOccurrence[i] = -1;
            for (int i = 0; i < pattern.Length; i++)
                lastOccurrence[pattern[i]] = i;

            // Initialize variables
            char lastCharDiff = '\0';
            int textLength = text.Length;
            int patternLength = pattern.Length;
            int hammingDistance = 0;
            int textIndex = patternLength - 1;
            int patternIndex = patternLength - 1;

            // Start the Boyer-Moore pattern matching with Hamming distance calculation
            while (textIndex < textLength)
            {
                if (pattern[patternIndex] == text[textIndex])
                {
                    if (patternIndex == 0)
                    {
                        // Reached the start of the pattern, update minimum Hamming distance
                        if (hammingDistance < minHammingDistance)
                        {
                            minHammingDistance = hammingDistance;
                        }

                        // Prepare to jump to the next potential match
                        int lastOccurIndex;
                        if (lastCharDiff != '\0')
                        {
                            lastOccurIndex = lastOccurrence[lastCharDiff];
                        }
                        else
                        {
                            lastOccurIndex = lastOccurrence[text[textIndex + patternLength - 1]];
                        }

                        textIndex += patternLength - Math.Min(patternIndex, 1 + lastOccurIndex);
                        patternIndex = patternLength - 1;
                        hammingDistance = 0;
                        lastCharDiff = '\0'; // Reset the lastCharDiff
                    }
                    else
                    {
                        textIndex--;
                        patternIndex--;
                    }
                }
                else
                {
                    lastCharDiff = text[textIndex];
                    hammingDistance++;
                    if (hammingDistance > MAX_HAMMING_DISTANCE)
                    {
                        hammingDistance = 0;
                        int lastOccurIndex = lastOccurrence[text[textIndex]];
                        textIndex += patternLength - Math.Min(patternIndex, 1 + lastOccurIndex);
                        patternIndex = patternLength - 1;
                        lastCharDiff = '\0'; // Reset the lastCharDiff
                    }
                    else
                    {
                        textIndex--;
                        patternIndex--;
                    }
                }

                // Check for boundaries
                if (textIndex < 0 || patternIndex < 0)
                    break;
            }

            return minHammingDistance == int.MaxValue ? -1 : minHammingDistance; // Return the minimum Hamming distance or -1 if no comparison was possible
        }

        public static Tuple<string, int> FindMostSimilarFingerprint(string inputFingerprint, List<string> database) {
            string mostSimilarFingerprint = null;
            int minDistance = int.MaxValue;

            foreach (string fingerprint in database) {
                string dataFingerprint = ImageManager.ImagetoAscii(fingerprint);
                int result = Compare(dataFingerprint, inputFingerprint);
                int distance = result;

                if (distance != -1) {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        mostSimilarFingerprint = fingerprint;
                    }
                }
            }

            return Tuple.Create(mostSimilarFingerprint, minDistance);
    }
    }
}

