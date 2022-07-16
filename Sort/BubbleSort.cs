using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort {
    
    public class BubbleSort {
    
        public static int[] Sort(int[] A, int n) {

            // Performance measure
            var PT = PerformanceTracker.StartTrack("BubbleSort", $"with {n} keys");

            // Algorithm
            for (int i = 0; i < n; i++) {
                for (int j = n - 1; j > i ; j--) {
                    if (A[j] < A[j - 1]) {
                        var cache = A[j];
                        A[j] = A[j - 1];
                        A[j - 1] = cache;
                    }
                }
            }

            // Performance measure
            PT.Stop();

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
