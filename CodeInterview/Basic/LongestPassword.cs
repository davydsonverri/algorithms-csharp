using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{

    /// <summary>
    /// Find the biggest binary gap
    /// </summary>
    public class LongestPassword
    {

        public static int Execute(string S)
        {

            // Performance measure
            var PT = PerformanceTracker.StartTrack("LongestPassword", $" on {S}");

            // Algorithm
            var biggestPassword = 0;
            var passwords = S.Split(' ');
            foreach (var password in passwords)
            {
                if (isValidPassword(password) && password.Length > biggestPassword)
                {
                    biggestPassword = password.Length;
                }
            }

            // Performance measure
            PT.Stop();

            return biggestPassword;
        }

        private static bool isValidPassword(string password)
        {
            if (password.Length == 0) return false;

            var asciiNumber = new Tuple<int, int>(48, 57);
            var asciiChars = new Tuple<int, int>(97, 122);
            var asciiCharsCaptilized = new Tuple<int, int>(65, 90);
            var stackNumbers = new Stack<int>();
            var stackChars = new Stack<char>();

            foreach (char c in password)
            {
                if (c >= asciiNumber.Item1 && c <= asciiNumber.Item2)
                {
                    stackNumbers.Push(c);
                } else if ((c >= asciiChars.Item1 && c <= asciiChars.Item2) || (c >= asciiCharsCaptilized.Item1 && c <= asciiCharsCaptilized.Item2))
                {
                    stackChars.Push(c);
                } else
                {
                    return false;
                }
            }

            var isEvenLetters = stackChars.Count % 2 == 0;
            var isOddNumbers = stackNumbers.Count % 2 != 0;

            return isEvenLetters && isOddNumbers;
        }
    }
}
