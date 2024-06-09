using System;
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

    public static Tuple<string, int> FindMostSimilarFingerprint(string inputFingerprint, List<string> database) {
        string mostSimilarFingerprint = null;
        int minDistance = int.MaxValue;

        int distance;
        foreach (string fingerprint in database) {
            string dataFingerprint = ImageManager.ImagetoAscii(fingerprint,0);
            bool result = KmpSearch(dataFingerprint, inputFingerprint);

            if (result){
                minDistance = 0;
                mostSimilarFingerprint = fingerprint;
                break;
            } else{
                string subText = dataFingerprint.Substring(0, 30);
                distance = Similarity.CalculateHammingDistance(subText, inputFingerprint);
            }

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