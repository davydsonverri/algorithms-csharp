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
        /// Sort values with InserctionSort algorithm
        /// </summary>
        /// <param name="array">Values to be sorted</param>
        /// <param name="n">Array count</param>
        /// <returns></returns>
        public static int[] Sort(int[] A, int n) {

            // Performance measure
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

            return A;
        }

        public static void TestSort() {
            for (int i = 1; i <= 5; i++) {
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

