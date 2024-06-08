using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace FingerPrintDetector
{
    public static class RegexHelper
    {
        private static string ConvertAlayToNormal(string alayText){
            var alayPatterns = new Dictionary<string, string>
            {
                { "[4@Aa]", "a" },
                { "[8Bb]", "b" },
                { "[3Ee]", "e" },
                { "[6Gg]", "g" },
                { "[1!Ii]", "i" },
                { "[0Oo]", "o" },
                { "[5$Ss]", "s" },
                { "[7Tt]", "t" },
                { "[2Zz]", "z" },
                { "[@]", "a" },
                { "[!]", "i" },
                { "[$]", "s" },
                { "[<\\[]", "c" },
                { "[|<]", "k" },
                { "[+]", "t" }
            };

            foreach (var pattern in alayPatterns)
            {
                alayText = Regex.Replace(alayText, pattern.Key, pattern.Value, RegexOptions.IgnoreCase);
            }

            // Mengabaikan kombinasi huruf besar-kecil
            alayText = alayText.ToLower();

            return alayText;

        }
       
        public static bool IsMatch(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static int LevenshteinDistance(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1)) return s2.Length;
            if (string.IsNullOrEmpty(s2)) return s1.Length;

            int[,] d = new int[s1.Length + 1, s2.Length + 1];
            for (int i = 0; i <= s1.Length; i++) d[i, 0] = i;
            for (int j = 0; j <= s2.Length; j++) d[0, j] = j;

            for (int i = 1; i <= s1.Length; i++)
            {
                for (int j = 1; j <= s2.Length; j++)
                {
                    int cost = (s2[j - 1] == s1[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[s1.Length, s2.Length];
        }

        public static string FindClosestMatch(string input, List<string> candidates)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentException("Input cannot be null or empty.", nameof(input));
            }

            if (candidates == null)
            {
                throw new ArgumentNullException(nameof(candidates), "Candidates cannot be null.");
            }

            if (candidates.Count == 0)
            {
                throw new ArgumentException("Candidates cannot be an empty list.", nameof(candidates));
            }

            string closestMatch = null;
            int minDistance = int.MaxValue;

            foreach (var candidate in candidates)
            {
                int distance = LevenshteinDistance(input, candidate);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestMatch = candidate;
                }
            }

            return closestMatch;
        }

        public static List<string> GetAllNamesFromBiodata()
        {
            var names = new List<string>();
            string connectionString = $"Data Source=fingerprintalay.db";
            // string connectionString = $"Data Source=stima.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nama FROM biodata";
                using (var command = new SqliteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            names.Add(reader.GetString(0));
                        }
                    }
                }
            }
            return names;
        }

        public static string GetAlayName(string sidikJariName){
            List<string> biodataAlayNames = RegexHelper.GetAllNamesFromBiodata();
            Dictionary<string, string> biodataNameAlayMap = new Dictionary<string, string>();
            foreach (string alayName in biodataAlayNames)
            {
                string normalName = ConvertAlayToNormal(alayName);
                biodataNameAlayMap[normalName] = alayName;
            }
            List<string> biodataNormalNames = new List<string>();
            foreach (var pair in biodataNameAlayMap)
            {
                biodataNormalNames.Add(pair.Key);
            }

            string closestMatch = RegexHelper.FindClosestMatch(sidikJariName, biodataNormalNames);
            string alayClosestMatch = biodataNameAlayMap[closestMatch];
            int distance = RegexHelper.LevenshteinDistance(sidikJariName, closestMatch);
            float similarity = (1 - (float)distance / Math.Max(sidikJariName.Length, closestMatch.Length)) * 100;

            Console.WriteLine($"Sidik Jari Name: {sidikJariName}, Closest Biodata Match: {alayClosestMatch}, Distance: {distance}, Similarity: {similarity}%");    
            return alayClosestMatch;
        }
    }
}
