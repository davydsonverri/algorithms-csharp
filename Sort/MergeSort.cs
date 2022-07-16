using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sort;

/// <summary>
/// MergeSort is a divide and conquer algorithm.
/// Recursively split array at midpoint until reach minimum size then merge results
/// </summary>
public static class MergeSort {

    public static int[] Sort(int[] A, int n) {
        // Performance measure
        var PT = PerformanceTracker.StartTrack("MergeSort", $"with {n} keys");

        Sort(ref A, 0, n - 1);        

        // Performance measure
        PT.Stop();

        return A;
    }

    public static void Sort(ref int[] A, int p, int r) {
        if (p >= r) return;

        var q = (p + r) / 2;
        Sort(ref A, p, q);
        Sort(ref A, q + 1, r);

        Merge(ref A, p, q, r);
    }

    private static void Merge(ref int[] A, int p, int q, int r) {

        if (!(p <= q && q < r)) throw new ArgumentException("Inputs must follow the rule p <= q < r");

        // Algorithm
        int nL = q - p + 1;                      // Length of A[p:q]
        int nR = r - q;                          // Length of A[q+1:r]
        int[] L = new int[nL];
        int[] R = new int[nR];

        for (int ii = 0; ii < nL; ii++) {        // copy A[p:q] into L[0:nLeft-1]
            L[ii] = A[p + ii];
        }

        for (int jj = 0; jj < nR; jj++) {        // copy A[q+1:r] into R[0:nR-1]
            R[jj] = A[q + jj + 1];
        }

        int i = 0;                               // i indexes the smallest remaining element in L
        int j = 0;                               // j indexes the smallest remaining element in R
        int k = p;                               // k indexes the location in A to fill

        /*
        As long each of the arrays L and R contains an unmerged elemnt,
        copy the smallest unmerged element back into A[p:r].
        */
        while (i < nL && j < nR) {
            if (L[i] <= R[j]) {
                A[k] = L[i];
                i++;
            } else {
                A[k] = R[j];
                j++;
            }
            k++;
        }

        /*
        Having gone through one of L and R entirely, copy the
        remainder of the other to the end of A[p:r]
        */
        while (i < nL) {
            A[k] = L[i];
            i++;
            k++;
        }
        while (j < nR) {
            A[k] = R[j];
            j++;
            k++;
        }
    }

    public static void TestMerge() {
        int[] arr = { 2, 4, 6, 7, 1, 2, 3, 1 };
        Merge(ref arr, 0, 3, 7);
    }

    public static void TestSort() {
        for (int i = 1; i <= 9; i++) {
            var arrSize = Convert.ToInt32(Math.Pow(10, i));
            var arr = new int[arrSize];
            for (int k = 0; k < arrSize; k++) {
                arr[arrSize - 1 - k] = k + 1;
            }
            var sortedArr = Sort(arr, arrSize);
        }
    }
}

