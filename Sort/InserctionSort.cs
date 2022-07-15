using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort {
    /// <summary>
    /// An efficient algorithm for sorting a small number of elements
    /// </summary>
    public class InserctionSort {

        /// <summary>
        /// Sort values
        /// </summary>
        /// <param name="array">Values to be sorted</param>
        /// <param name="n">Array count</param>
        /// <returns></returns>
        public static int[] Sort(int[] A, int n) {

            // Performance measure
            /*
            Console.WriteLine($"Algorithm .: InserctionSort with {n} keys");
            var garbageBefore = GC.CollectionCount(0);
            var memoryBefore = Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
            var sw = Stopwatch.StartNew();
            */

            var PT = PerformanceTracker.StartTrack("InserctionSort", $"with {n} keys");

            // Algorithm
            for (int i = 1; i < n; i++) {
                var key = A[i];

                // Insert A[i] into de sorted subarray A[1:i-1]
                var j = i - 1;
                while(j>=0 && A[j] > key) {
                    A[j + 1] = A[j];
                    j = j - 1;
                    A[j + 1] = key;
                }
            }

            // Performance measure
            PT.Stop();

            /*
            sw.Stop();
            var memoryAfter = Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
            
            Console.WriteLine($"Time      .: {sw.ElapsedMilliseconds} ms");
            Console.WriteLine($"#Gen0     .: {GC.CollectionCount(0) - garbageBefore}");
            Console.WriteLine($"#Mem      .: {memoryAfter - memoryBefore}");
            Console.WriteLine("");
            */
            return A;
        }

        public static void TestSort() {
            for (int i = 1; i <= 6; i++) {
                var arrSize = Convert.ToInt32(Math.Pow(10, i));
                var arr = new int[arrSize];
                for (int k = 0; k < arrSize; k++) {
                    arr[arrSize - 1 - k] = k + 1;
                }
                var sortedArr = Sort(arr, arrSize);
            }
        }

    }
}

