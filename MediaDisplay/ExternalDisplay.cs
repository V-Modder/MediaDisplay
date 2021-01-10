using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading;
using Encoder = System.Drawing.Imaging.Encoder;

namespace MediaDisplay {
    public abstract class ExternalDisplay : IDisposable {
        public event EventHandler<ExternalEventArgs> OnEventReceived;
        public delegate void EventReceivedEventHandler<ExternalEventArgs>(object sender, ExternalEventArgs e);

        private DisplayStatus status;

        public abstract void Dispose();
        protected abstract void CallService(string data);
        protected abstract void InitConnection();

        protected virtual void EventReceived(ExternalEventArgs e) {
            EventHandler<ExternalEventArgs> handler = OnEventReceived;
            handler?.Invoke(this, e);
        }

        protected void MessageReceived(string msg) {
            var action = JsonConvert.DeserializeObject<ExternalEventArgs>(msg);
            EventReceived(action);
        }

        protected void SetStatus(DisplayStatus status) {
            this.status = status;
        }

        public void SendMetric(Metric data) {
            new Thread(() => {
                try {
                    CallService(data.ToJson());
                }
                finally {
                }
            }).Start();
        }

        public bool IsConnected { get { return status == DisplayStatus.Connected; } }

        public DisplayStatus Status { get { return status; } }

        public void Connect() {
            new Thread(() => {
                InitConnection();
            }).Start();
        }
    }

    public enum DisplayStatus {
        Disconnected, Connecting, Connected, Closing
    }
}
