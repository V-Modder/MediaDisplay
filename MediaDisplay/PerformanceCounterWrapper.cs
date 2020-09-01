using System;
using System.Diagnostics;

namespace MediaDisplay {
    class PerformanceCounterWrapper : IDisposable {

        private PerformanceCounter pc;
        public PerformanceCounterWrapper(string type, string sensore, string name) {
            pc = new PerformanceCounter(type, sensore, name);
        }

        public long getValue() {
            return pc.NextSample().RawValue;
        }

        public void Dispose() {
            pc.Close();
            pc.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
