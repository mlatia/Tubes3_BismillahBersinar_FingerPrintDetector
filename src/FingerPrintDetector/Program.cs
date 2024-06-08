using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FingerPrintDetector
{
    static class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0 && args[0] == "console")
            {
                RunConsoleMode();
            }
            else
            {
                RunGuiMode();
            }
        }

        private static void RunGuiMode()
        {
            AttachConsole(-1);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void RunConsoleMode()
        {
            List<string> biodataNames = RegexHelper.GetAllNamesFromBiodata();
            List<string> sidikJariNames = RegexHelper.GetAllNamesFromSidikJari();
            List<string> regexPatterns = RegexHelper.GenerateCommonPatterns();

            foreach (var sidikJariName in sidikJariNames)
            {
                // Check regex patterns
                List<string> matchedPatterns = RegexHelper.FindMatches(sidikJariName, regexPatterns);
                foreach (var pattern in matchedPatterns)
                {
                    Console.WriteLine($"Name '{sidikJariName}' matches pattern: {pattern}");
                }

                // Find closest match using Levenshtein distance
                string closestMatch = RegexHelper.FindClosestMatch(sidikJariName, biodataNames);
                int distance = RegexHelper.LevenshteinDistance(sidikJariName, closestMatch);
                float similarity = (1 - (float)distance / Math.Max(sidikJariName.Length, closestMatch.Length)) * 100;

                Console.WriteLine($"Sidik Jari Name: {sidikJariName}, Closest Biodata Match: {closestMatch}, Distance: {distance}, Similarity: {similarity}%");
            }
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AttachConsole(int dwProcessId);
    }
}
