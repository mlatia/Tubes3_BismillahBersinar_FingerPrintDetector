using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace FingerPrintDetector
{
    public static class RegexHelper
    {
        public static bool IsMatch(string input, string pattern)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        public static List<string> FindMatches(string input, List<string> patterns)
        {
            var matches = new List<string>();
            foreach (var pattern in patterns)
            {
                if (IsMatch(input, pattern))
                {
                    matches.Add(pattern);
                }
            }
            return matches;
        }

        public static List<string> GenerateCommonPatterns()
        {
            return new List<string>
            {
                @"^[a-zA-Z]+$", // Only letters
                @"^[A-Z][a-z]+$", // Capital first letter
                @"^[a-z]+[0-9]+$", // Letters followed by numbers
                @"^[A-Za-z0-9]+$", // Alphanumeric
                @"^[A-Z]+$", // All uppercase
                @"^[a-z]+$", // All lowercase
                @"^[0-9]+$" // All numbers
            };
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
            if (string.IsNullOrEmpty(input) || candidates == null || candidates.Count == 0)
            {
                throw new ArgumentException("Input or candidates cannot be null or empty.");
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
            string connectionString = $"Data Source=stima.db";
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

        public static List<string> GetAllNamesFromSidikJari()
        {
            var names = new List<string>();
            string connectionString = $"Data Source=stima.db";
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT nama FROM sidik_jari";
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
    }
}
