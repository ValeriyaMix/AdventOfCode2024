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
                List<int> reportLevels = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
                string[] reportLevelsString = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                int[] orderAscend = reportLevels.OrderBy(x => x).ToArray();
                int[] orderDescend = reportLevels.OrderByDescending(x => x).ToArray();

                string stringAscend = string.Join(" ", orderAscend);
                string stringDescend = string.Join(" ", orderDescend);

                if (line != stringAscend)
                {
                    if (line != stringDescend)
                    {
                        initialLength--;
                    }
                    else
                    {
                        for (int i = 0; i < reportLevels.Count - 1; i++)
                        {
                            int differenceNext = Math.Abs(reportLevels[i] - reportLevels[i + 1]);
                            if (!(differenceNext >= 1 && differenceNext <= 3))
                            {
                                initialLength--;
                                break;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < reportLevels.Count - 1; i++)
                    {
                        int differenceNext = Math.Abs(reportLevels[i] - reportLevels[i + 1]);
                        if (!(differenceNext >= 1 && differenceNext <= 3))
                        {
                            initialLength--;
                            break;
                        }
                    }
                }
            }
            Console.WriteLine(initialLength);
        }

        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2024\{folderName}\input.txt");

        }


    }
}

