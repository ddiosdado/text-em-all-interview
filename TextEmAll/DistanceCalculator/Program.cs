using System;
using System.Collections.Generic;
using System.Linq;

namespace DistanceCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tests = new List<string> { "gbcjbdha", "amstwze", "zabes", "az", "a", "ab", "aebiejp" };
            var challengeRunner = new Challenge2();
            foreach (var test in tests)
            {
                var maxDistance = challengeRunner.MaxDistance(test);
                Console.WriteLine($"test:{test}, maxDistance:{maxDistance}");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
    public class Challenge2
    {
        public int MaxDistance(string input)
        {
            var intValues = input.Select(x => (int)x);
            int maxDistance = default, elementsToLoop = intValues.Count() - 1;
            char leftChar = default, rightChar = default;
            for (int currentIndex = 0; currentIndex < elementsToLoop; currentIndex++)
            {
                var currentChar = intValues.ElementAt(currentIndex);
                var maxOfOthers = intValues
                    //We don't want to compare the char with itself or any chars that come before in the collection
                    .Skip(currentIndex + 1)
                    //All characters to compare need to come after the current one(alphabetically)
                    .Where(max => max > currentChar)
                    //The we just order by ASCII value
                    .OrderByDescending(x => x).FirstOrDefault();
                var currentMaxDistance = maxOfOthers - currentChar - 1;//range ends should not be considered
                //maxDistance = currentMaxDistance > maxDistance ? currentMaxDistance : maxDistance;
                if (currentMaxDistance > maxDistance)
                {
                    leftChar = (char)currentChar;
                    rightChar = (char)maxOfOthers;
                    maxDistance = currentMaxDistance;
                }
                //maxDistance = Math.Max(maxDistance, currentMaxDistance);
            }
            Console.WriteLine($"Left char:{leftChar}, right char:{rightChar}");
            return maxDistance;
        }
    }
}
