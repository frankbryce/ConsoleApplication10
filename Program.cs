using System;
using System.Linq;

namespace ConsoleApplication10
{
    class Program
    {
        static void Main()
        {
            var random = new Random((int)DateTime.Now.Ticks);

            var numberOfChoices = 40;

            var numberOfRuns = 1000;
            var totalChoiceCount = new int[numberOfChoices];
            var graphDownScaleFactor = numberOfRuns;

            // per sample
            var numberOfEffectiveSamples = numberOfChoices * numberOfChoices;
            var subsampleIndex = 1;

            for (var runIdx = 0; runIdx < numberOfRuns; runIdx++)
            {
                var choiceCount = new int[numberOfChoices];
                for (var idx = 0; idx < numberOfEffectiveSamples * subsampleIndex; idx++)
                {
                    choiceCount[random.Next(numberOfChoices)]++;
                }
                for (var choiceIdx = 0; choiceIdx < numberOfChoices; choiceIdx++)
                {
                    choiceCount[choiceIdx] /= subsampleIndex;
                }

                Array.Sort(choiceCount, (a, b) => b - a);

                for (var idx = 0; idx < numberOfChoices; idx++)
                {
                    totalChoiceCount[idx] += choiceCount[idx];
                }
            }

            for (var idx = 0; idx < numberOfChoices; idx++)
            {
                totalChoiceCount[idx] /= graphDownScaleFactor;
            }

            printGraph(totalChoiceCount);

            Console.ReadLine();
        }

        private static void printGraph(int[] choiceCount)
        {
            var numberOfChoices = choiceCount.Length;
            var max = choiceCount.Max();

            Console.WriteLine("Probability Distrubution");
            Console.WriteLine();

            for (var idx = max; idx > 0; idx--)
            {
                Console.Write(string.Format("{0:00}", idx) + " ");
                for (var choiceIdx = 0; choiceIdx < numberOfChoices; choiceIdx++)
                {
                    if (choiceCount[choiceIdx] >= idx)
                    {
                        Console.Write(" | ");
                    }
                    // COOL! commenting this out essentially sorts the distribution largest to smallest
                    else
                    {
                        Console.Write("   ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write("00 ");
            for (var choiceIdx = 0; choiceIdx < numberOfChoices; choiceIdx++)
            {
                Console.Write("---");
            }
            Console.WriteLine();

            Console.Write("   ");
            for (var choiceIdx = 0; choiceIdx < numberOfChoices; choiceIdx++)
            {
                Console.Write(" {0:00}", choiceIdx);
            }
            Console.WriteLine();
        }
    }
}
