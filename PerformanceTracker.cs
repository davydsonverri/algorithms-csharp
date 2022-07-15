using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms {
    public class PerformanceTracker : IDisposable {

        public readonly string Algorithm;
        public readonly string Detail;
        private int GCGen0Before;
        private int GCGen0After;
        private long MemoryBefore;
        private long MemoryAfter;
        private Stopwatch SW;

        public long ElapsedMilliseconds { get { return SW.ElapsedMilliseconds; } }
        public long MemoryConsumption { get { return MemoryAfter - MemoryBefore; } }
        public long GCGen0Counter { get { return GCGen0After - GCGen0Before; } }


        internal PerformanceTracker(string algorithm, string detail) {
            Log("Algorithm", $"{algorithm} {detail}");
            Algorithm = algorithm;
            Detail = detail;
            GCGen0Before = GC.CollectionCount(0);
            MemoryBefore = Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
            SW = Stopwatch.StartNew();
        }

        public static PerformanceTracker StartTrack(string algorithm, string detail) {            
            var performanceTracker = new PerformanceTracker(algorithm, detail);            
            return performanceTracker;
        }

        public void Stop() {
            SW.Stop();
            MemoryAfter = Process.GetCurrentProcess().WorkingSet64 / 1024 / 1024;
            GCGen0After = GC.CollectionCount(0);
            Log("Time", $"{ElapsedMilliseconds} ms");
            Log("#Gen0", $"{GCGen0Counter}");
            Log("#Mem", $"{MemoryConsumption}");
            Console.WriteLine("");
        }
        
        private void Log(string key, string value) {
            Console.WriteLine($"{string.Format("{0, 15}", key)} .: {value}");
        }

        public void Dispose() {
            throw new NotImplementedException();
        }
    }
}

