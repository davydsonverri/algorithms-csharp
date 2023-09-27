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
    public class BinaryGap_01_BiggestGap
    {

        public static int Execute(int n)
        {

            // Performance measure
            var PT = PerformanceTracker.StartTrack("BinaryGap_01_BiggestGap", $"with number {n}");

            // Algorithm
            string binary = Convert.ToString(n, 2);

            var stack = new Stack<char>();

            int biggestGap = 0;

            foreach (char c in binary)
            {
                if (!stack.Any() && c == '1')
                {
                    stack.Push(c);
                    continue;
                }

                if (stack.Any() && c == '0')
                {
                    stack.Push(c);
                } else if (stack.Any() && c == '1')
                {
                    if (stack.Count >= 2 && stack.Count - 1 > biggestGap)
                    {
                        biggestGap = stack.Count - 1;
                    }
                    stack.Clear();
                    stack.Push(c);
                    continue;
                }
            }
            Console.WriteLine("Binary:      " + binary);
            Console.WriteLine("Biggest Gap:   " + biggestGap);

            // Performance measure
            PT.Stop();

            return biggestGap;
        }
    }
}
