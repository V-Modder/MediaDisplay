using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;

namespace MediaDisplay {
    public class NetworkMonitor {
        private Timer timer;
        private List<NetworkAdapter> adapters;
        private List<NetworkAdapter> monitoredAdapters;

        public NetworkMonitor() {
            adapters = new List<NetworkAdapter>();
            monitoredAdapters = new List<NetworkAdapter>();
            EnumerateNetworkAdapters();

            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
        }

        private void EnumerateNetworkAdapters() {
            PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

            foreach (string name in category.GetInstanceNames()) {
                if (name == "MS TCP Loopback interface")
                    continue;
                adapters.Add(new NetworkAdapter(name));
            }
        }

        private void timer_Elapsed(object sender, ElapsedEventArgs e) {
            foreach (NetworkAdapter adapter in monitoredAdapters)
                adapter.refresh();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        public List<NetworkAdapter> Adapters {
            get {
                return adapters;
            }
        }

        public void StartMonitoring() {
            if (adapters.Count > 0) {
                foreach (NetworkAdapter adapter in adapters)
                    if (!monitoredAdapters.Contains(adapter)) {
                        monitoredAdapters.Add(adapter);
                        adapter.init();
                    }

                timer.Enabled = true;
            }
        }

        public void StartMonitoring(NetworkAdapter adapter) {
            if (!monitoredAdapters.Contains(adapter)) {
                monitoredAdapters.Add(adapter);
                adapter.init();
            }
            timer.Enabled = true;
        }

        public void StopMonitoring() {
            monitoredAdapters.Clear();
            timer.Enabled = false;
        }

        public void StopMonitoring(NetworkAdapter adapter) {
            if (monitoredAdapters.Contains(adapter))
                monitoredAdapters.Remove(adapter);
            if (monitoredAdapters.Count == 0)
                timer.Enabled = false;
        }

        public string sumDownloadSpeed() {
            return SizeSuffix(adapters.Sum(a => a.DownloadSpeed));
        }

        public string sumUploadSpeed() {
            return SizeSuffix(adapters.Sum(a => a.UploadSpeed));
        }

        private static readonly string[] SizeSuffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        private static string SizeSuffix(long value, int decimalPlaces = 1) {
            if (value < 0) { return "-" + SizeSuffix(-value); }
            if (value == 0) { return "0,0 B"; }

            // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
            int mag = (int)Math.Log(value, 1024);

            // 1L << (mag * 10) == 2 ^ (10 * mag) 
            // [i.e. the number of bytes in the unit corresponding to mag]
            decimal adjustedSize = (decimal)value / (1L << (mag * 10));

            // make adjustment when the value is large enough that
            // it would round up to 1000 or more
            if (Math.Round(adjustedSize, decimalPlaces) >= 1000) {
                mag += 1;
                adjustedSize /= 1024;
            }

            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        }
    }
}
