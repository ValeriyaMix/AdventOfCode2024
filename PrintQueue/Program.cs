using System;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = readFromTheFile("PrintQueue");
            List<string> listOfLines = lines.ToList();
            List<int> indexesOfRightOrderLines = new List<int>();
            List<int> listNumbersThatGoAfter = new List<int>();
            List<int> indexes = new List<int>();

            // Initialize lists to store column values
            List<int> leftColumn = new List<int>();
            List<int> rightColumn = new List<int>();
            int count = 0;
            List<bool> allNumbersFound = new List<bool>();

            foreach (string line in listOfLines)
            {
                if (line.Length != 0)
                {
                    // Split the line into columns
                    string[] columnParts = line.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                    if (columnParts.Length == 2)
                    {
                        leftColumn.Add(int.Parse(columnParts[0]));
                        rightColumn.Add(int.Parse(columnParts[1]));
                    }
                    count++;
                }
                else
                {
                    break;
                }
            }
            listOfLines.RemoveRange(0, count + 1);
            foreach (string item in listOfLines)
            {
                List<int> itemList = item
                    .Split(',')
                    .Select(int.Parse)
                    .ToList();
                for (int i = 0; i < itemList.Count; i++)
                { 
                    if (!leftColumn.Contains(itemList[i]))
                    {
                        break;
                    }
                    else
                    {
                        indexes = leftColumn
                            .Select((value, index) => new { value, index })  // Pair values with their indexes
                            .Where(pair => pair.value == itemList[i])        // Filter for the target value
                            .Select(pair => pair.index)                      // Select the index
                            .ToList();
                        foreach (int index in indexes)
                        {
                            listNumbersThatGoAfter.Add(rightColumn[index]);
                        }

                        for (int j = i + 1; j < itemList.Count; j++)
                        {
                            if (listNumbersThatGoAfter.Contains(itemList[j]))
                            {
                                allNumbersFound.Add(true);
                            }
                            else
                            {
                                allNumbersFound.Add(false);
                                break;
                            }
                        }

                        if (allNumbersFound.Contains(false))
                        {
                            break;
                        }
                    }
                }

                // If all numbers are found in listNumbersThatGoAfter, record the index of the item
                if (!allNumbersFound.Contains(false))
                {
                    indexesOfRightOrderLines.Add(listOfLines.IndexOf(item));
                }
                allNumbersFound.Clear();
                listNumbersThatGoAfter.Clear();
                indexes.Clear();  
                itemList.Clear();
            }

            foreach (int ind in  indexesOfRightOrderLines)
            {
                Console.WriteLine(ind);
            }
        }

        public static string[] readFromTheFile(string folderName)
        {
            return System.IO.File.ReadAllLines($@"C:\Users\valer\source\repos\AdventOfCode2024\{folderName}\inputCheck.txt");

        }


    }
}