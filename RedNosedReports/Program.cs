using System;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> report = new List<int>();
            string[] lines = readFromTheFile("RedNosedReports");
            List<int> reportDifferences = new List<int>();
            int differencePrev = 0;
            int unsafeReports = 0;
            int initialLength = lines.Length;


            foreach (string line in lines)
            {
                // Split the line into rows
                int[] reportLevels = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToArray();
                string[] reportLevelsString = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                string[] orderAscend = reportLevelsString.OrderBy(x => x).ToArray();
                string[] orderDescend = reportLevelsString.OrderByDescending(x => x).ToArray();

                string stringAscend = string.Join(" ", orderAscend);
                string stringDescend = string.Join(" ", orderDescend);

                if (line == stringAscend)
                {
                    for (int i = 0; i < reportLevels.Length - 1; i++)
                    {
                        int differenceNext = Math.Abs(reportLevels[i] - reportLevels[i + 1]);
                        if (!(differenceNext >= 1 && differenceNext <= 3))
                        {
                            unsafeReports++;
                            break;
                        }
                    }
                }
                else if (line == stringDescend)
                {
                    for (int i = 0; i < reportLevels.Length - 1; i++)
                    {
                        int differenceNext = reportLevels[i] - reportLevels[i + 1];
                        if (!(differenceNext >= 1 && differenceNext <= 3))
                        {
                            unsafeReports++;
                            break;
                        }
                    }
                }
                else
                {
                    //unsafeReports++;
                }

            }
            Console.WriteLine(initialLength - unsafeReports);
        }

        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2024\{folderName}\input.txt");

        }


    }
}

