using System;
using System.Diagnostics;

namespace MediaDisplay {
    public class NetworkAdapter {
        private long dlValue, ulValue;
        private long dlValueOld, ulValueOld;

        internal string name;

        public NetworkAdapter(string name) {
            this.name = name;
        }

        private long getValue(bool down) {
            PerformanceCounterWrapper wrapper = null;
            if (down) {
                wrapper = new PerformanceCounterWrapper("Network Interface", "Bytes Received/sec", name);
            }
            else {
                wrapper = new PerformanceCounterWrapper("Network Interface", "Bytes Sent/sec", name);
            }

            long l = wrapper.getValue();
            wrapper.Dispose();
            return l;
        }

        internal void init() {
            dlValueOld = getValue(true);
            ulValueOld = getValue(false);
        }

        internal void refresh() {
            dlValue = getValue(true);
            ulValue = getValue(false);


            DownloadSpeed = dlValue - dlValueOld;
            UploadSpeed = ulValue - ulValueOld;

            dlValueOld = dlValue;
            ulValueOld = ulValue;
        }

        public override string ToString() {
            return name;
        }

        public string Name {
            get {
                return name;
            }
        }

        public long DownloadSpeed { get; private set; }

        public long UploadSpeed { get; private set; }

        public long DownloadSpeedKbps {
            get {
                return DownloadSpeed;
            }
        }

        public long UploadSpeedKbps {
            get {
                return UploadSpeed;
            }
        }
    }
}
