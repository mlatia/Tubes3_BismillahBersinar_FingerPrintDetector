using System;
using System.Collections.Generic;

namespace FingerPrintDetector
{
public class KMP
{
    static int[] KmpPreprocess(string pattern)
    {
        int[] prefix = new int[pattern.Length];
        int j = 0;
        for (int i = 1; i < pattern.Length; i++)
        {
            while (j > 0 && pattern[i] != pattern[j])
            {
                j = prefix[j - 1];
            }
            if (pattern[i] == pattern[j])
            {
                j++;
            }
            prefix[i] = j;
        }
        return prefix;
    }

    static Tuple<int, int> KmpSearch(string text, string pattern)
    {
        int[] prefix = KmpPreprocess(pattern);
        int j = 0;
        int distance = 0;
        for (int i = 0; i < text.Length; i++)
        {
            while (j > 0 && text[i] != pattern[j])
            {
                j = prefix[j - 1];
            }
            if (text[i] == pattern[j])
            {
                j++;
            }
            if (j == pattern.Length)
            {
                return Tuple.Create(i - j + 1, distance);
            }
            distance += Convert.ToInt32(text[i] != pattern[j]);
        }
        return Tuple.Create(-1, int.MaxValue);
    }

    public static Tuple<string, int> FindMostSimilarFingerprint(string inputFingerprint, List<string> database)
    {
        string mostSimilarFingerprint = null;
        int minDistance = int.MaxValue;

        foreach (string fingerprint in database)
        {
            string dataFingerprint = ImageManager.ImagetoAscii(fingerprint);
            Tuple<int, int> result = KmpSearch(dataFingerprint, inputFingerprint);
            int index = result.Item1;
            int distance = result.Item2;

            if (index != -1)
            {
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