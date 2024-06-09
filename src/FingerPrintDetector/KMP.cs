using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FingerPrintDetector
{
public class KMP
{
    static int[] KmpPreprocess(string pattern) {
        int[] prefix = new int[pattern.Length];
        int j = 0;
        for (int i = 1; i < pattern.Length; i++)  {
            while (j > 0 && pattern[i] != pattern[j])  {
                j = prefix[j - 1];
            }
            if (pattern[i] == pattern[j]) {
                j++;
            }
            prefix[i] = j;
        }
        return prefix;
    }

    public static bool KmpSearch(string text, string pattern) {
        int[] prefix = KmpPreprocess(pattern);
        int j = 0;
        for (int i = 0; i < text.Length; i++) {
            if (j > 0 && text[i] != pattern[j])    {
                j = prefix[j - 1];
            }
            if (text[i] == pattern[j]) {
                j++;
            }
            if (j == pattern.Length) {
                return true;
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
            bool result = KmpSearch(dataFingerprint, inputFingerprint);

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