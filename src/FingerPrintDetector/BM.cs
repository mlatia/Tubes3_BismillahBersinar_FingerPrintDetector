using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FingerPrintDetector
{
    public static class BM
    {
        private static Dictionary<char, int> BmPreprocess(string pattern)
        {
            Dictionary<char, int> lastOccurrence = new Dictionary<char, int>();
            for (int i = 0; i < pattern.Length; i++)
            {
                lastOccurrence[pattern[i]] = i;
            }
            return lastOccurrence;
        }
        public static bool BmSearch(string text, string pattern)
        {
            // Swap if pattern is longer than text
            if (pattern.Length > text.Length)
            {
                string temp = pattern;
                pattern = text;
                text = temp;
            }

            // Initialize lastOccurrence Dictionary
            Dictionary<char, int> lastOccurrence = BmPreprocess(pattern);

            // Initialize variables
            int textLength = text.Length;
            int patternLength = pattern.Length;
            int hammingDistance = int.MaxValue;
            int textIndex = patternLength - 1;
            int patternIndex = patternLength - 1;

            // Start the Boyer-Moore pattern matching with Hamming distance calculation
            while (textIndex < textLength)
            {   
                if (pattern[patternIndex] == text[textIndex])
                {
                    if (patternIndex == 0)
                    {
                        return true;
                        
                    }
                    else
                    {
                        textIndex--;
                        patternIndex--;
                    }
                }
                else
                {
                    int lastOccurIndex = lastOccurrence.GetValueOrDefault(text[textIndex], -1);
                    textIndex += patternLength - Math.Min(patternIndex, 1 + lastOccurIndex);
                    patternIndex = patternLength - 1;
                }

                // Check for boundaries
                if (textIndex < 0 || patternIndex < 0)
                {
                    break;
                }
            }
            
            return false;
        }

        public static Tuple<string, float> FindMostSimilarFingerprint(string inputFingerprint, List<string> database) {
            string mostSimilarFingerprint = null;
            float minDistance = float.MaxValue;
            string input = ImageManager.ImagetoAscii("assets/input.BMP", 0);
            float distance;

            Parallel.ForEach(database, fingerprint => {
                string dataFingerprint = ImageManager.ImagetoAscii(fingerprint, 0);
                bool result = BmSearch(dataFingerprint, inputFingerprint);

                if (result) {
                    if (input.Length < 3500) {
                        minDistance = 0;
                    } else {
                        string subText = dataFingerprint.Substring(0, 2000);
                        string subText2 = input.Substring(0, 2000);

                        string subText3 = dataFingerprint.Substring(Math.Max(0, dataFingerprint.Length - 2000));
                        string subText4 = input.Substring(Math.Max(0, input.Length - 2000));
                        int distance1 = Similarity.CalculateHammingDistance(subText, subText2);
                        int distance2 = Similarity.CalculateHammingDistance(subText3, subText4);
                        if (distance1 >= distance2) {
                            minDistance = (float)distance2 / 2000;
                        } else {
                            minDistance = (float)distance1 / 2000;
                        }
                    }
                    mostSimilarFingerprint = fingerprint;
                    return;
                } else {
                    string sub1, sub2;
                    if (dataFingerprint.Length >= input.Length) {
                        sub1 = dataFingerprint.Substring(0, input.Length);
                        sub2 = input;
                    } else {
                        sub1 = input.Substring(0, dataFingerprint.Length);
                        sub2 = dataFingerprint;
                    }

                    distance = (float)Similarity.CalculateHammingDistance(sub1, sub2) / sub1.Length;
                }

                if (distance < minDistance) {
                    minDistance = distance;
                    mostSimilarFingerprint = fingerprint;
                }
            });

            return Tuple.Create(mostSimilarFingerprint, minDistance);
        }
    }
}

