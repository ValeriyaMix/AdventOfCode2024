using System;
using static System.Formats.Asn1.AsnWriter;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = readFromTheFile("HistorianHysteria");
            List<int> listDifferences = new List<int>();

            // Initialize lists to store column values
            List<int> column1 = new List<int>();
            List<int> column1P2 = new List<int>();
            List<int> column2 = new List<int>();
            List<int> column2P2 = new List<int>();

            foreach (string line in lines)
            {
                // Split the line into columns
                string[] columnParts = line.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                if (columnParts.Length == 2) // Ensure there are exactly two columns
                {
                    column1.Add(int.Parse(columnParts[0]));
                    column1P2.Add(int.Parse(columnParts[0]));
                    column2.Add(int.Parse(columnParts[1]));
                    column2P2.Add(int.Parse(columnParts[1]));
                }
            }

            int length = column1.Count;

            for (int i = 0; i < length; i++)
            {
                int minColumn1 = column1.Min();
                int minColumn2 = column2.Min();
                int difference = Math.Abs(minColumn1 - minColumn2);
                listDifferences.Add(difference);
                column1.Remove(minColumn1);
                column2.Remove(minColumn2);
            }
            int answer = listDifferences.Sum();
            Console.WriteLine(answer);

            //Part two
            List<int> listSimularityScore = new List<int>();
            foreach (int value in column1P2)
            {
                List<int> foundMatches = column2P2.FindAll(x => x == value);
                int countMatches = foundMatches.Count;
                listSimularityScore.Add(value * countMatches);
            }

            int answerPartTwo = listSimularityScore.Sum();
            Console.WriteLine(answerPartTwo);
        }

        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2024\{folderName}\input.txt");

        }

        
    }
}
