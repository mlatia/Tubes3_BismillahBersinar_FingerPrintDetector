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
            // int[] lastOccurrence = new int[256];
            // for (int i = 0; i < 256; i++)
            //     lastOccurrence[i] = -1;
            // for (int i = 0; i < pattern.Length; i++)
            //     if (pattern[i] < 256)
            //         lastOccurrence[pattern[i]] = i;

            // Initialize variables
            int textLength = text.Length;
            int patternLength = pattern.Length;
            Console.WriteLine("Text Length:");
            Console.WriteLine(textLength);
            Console.WriteLine("Pattern Length:");
            Console.WriteLine(patternLength);
            int hammingDistance = int.MaxValue;
            int textIndex = patternLength - 1;
            int patternIndex = patternLength - 1;

            // Start the Boyer-Moore pattern matching with Hamming distance calculation
            while (textIndex < textLength)
            {   
                // Console.WriteLine("current text index");
                // Console.WriteLine(textIndex);

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

        public static Tuple<string, int> FindMostSimilarFingerprint(string inputFingerprint, List<string> database) {
            string mostSimilarFingerprint = null;
            int minDistance = int.MaxValue;
            int distance;
            foreach (string fingerprint in database) {
                string dataFingerprint = ImageManager.ImagetoAscii(fingerprint,0);
                bool result = BmSearch(dataFingerprint, inputFingerprint);

                if (result){
                    minDistance = 0;
                    mostSimilarFingerprint = fingerprint;
                    break;
                } else{
                    distance = Similarity.HammingDistance(fingerprint, inputFingerprint);
                }
                
                Console.WriteLine("Hamming distance:");
                Console.WriteLine(distance);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    mostSimilarFingerprint = fingerprint;
                }
            }
            
            return Tuple.Create(mostSimilarFingerprint, minDistance);
        }
    }
}

