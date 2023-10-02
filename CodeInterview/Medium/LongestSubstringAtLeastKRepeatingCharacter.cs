using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort
{

    /// <summary>
    /// Find the biggest binary gap
    /// </summary>
    public class LongestSubstringAtLeastKRepeatingCharacter
    {
        Dictionary<char, int> counterList = new Dictionary<char, int>();

        public int Execute(string s, int k)
        {

            // Performance measure
            var PT = PerformanceTracker.StartTrack("LongestSubstringAtLeastKRepeatingCharacter", "");

            // Algorithm
            if (s.Length < k) return 0;
            int longestSubstring = 0;

            for (int i = 0; i <= s.Length - k; i++)
            {

                for (int j = i; j < s.Length; j++)
                {
                    var key = s[j];
                    if (counterList.TryGetValue(key, out var counter))
                    {
                        counterList[key] = counter + 1;
                    } else
                    {
                        counterList.Add(key, 1);
                    }

                    var calculatedLongestSubstring = CalculateLongestSubstring(k);
                    if (calculatedLongestSubstring == 0) { continue; }

                    if (longestSubstring < calculatedLongestSubstring)
                    {
                        longestSubstring = calculatedLongestSubstring;
                    }
                }

                counterList.Clear();
            }

            // Performance measure
            PT.Stop();

            return longestSubstring;
        }

        public int CalculateLongestSubstring(int k)
        {
            var sum = 0;
            foreach (var counter in counterList)
            {
                if (counter.Value < k)
                {
                    return 0;
                } else
                {
                    sum += counter.Value;
                }
            }

            return sum;
        }
    }

}
