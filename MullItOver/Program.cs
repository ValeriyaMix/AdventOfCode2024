using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using static System.Formats.Asn1.AsnWriter;

namespace MyApp
{
    internal class Program
    {
        const string FilePath = @"C:\Users\valer\source\repos\AdventOfCode2024\MullItOver\input.txt";
        static void Main(string[] args)
        {
            int sum = 0;
            string line = File.ReadAllText(FilePath);
            string pattern = @"mul\(\d{1,3},\d{1,3}\)";

            var matches = Regex.Matches(line, pattern).ToList();

            foreach (var match in matches)
            {
                string intermediaryResult = match.Value.Remove(0, 4);
                string finalResult = intermediaryResult.Remove(intermediaryResult.Length - 1);
                int[] values = finalResult.Split(",").Select(int.Parse).ToArray();
                int product = values[0] * values[1];
                sum += product;
            }
            Console.WriteLine(sum);
        }
    }
}
