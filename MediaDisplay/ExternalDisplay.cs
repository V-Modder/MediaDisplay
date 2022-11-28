using Newtonsoft.Json;
using System;
using System.Threading;
using System.Timers;

namespace MediaDisplay {
    public abstract class ExternalDisplay : IDisposable {
        public event EventHandler<ExternalEventArgs> OnEventReceived;
        public delegate void EventReceivedEventHandler<ExternalEventArgs>(object sender, ExternalEventArgs e);

        private DisplayStatus status;
        private long sendedBytes;
        private long sendedBytesOld;
        private long sendedBytesPerSecond;
        private System.Timers.Timer speedCalculatorTimer;

        public ExternalDisplay() {
            speedCalculatorTimer = new System.Timers.Timer(2000);
            speedCalculatorTimer.Elapsed += SpeedCalculatorTimer_Elapsed;
            speedCalculatorTimer.AutoReset = true;
        }

        public abstract void Dispose();
        protected abstract void CallService(string data);
        protected abstract void InitConnection();
        public abstract void ConnectDisplay();
        public abstract void Disconnect();

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
            ExternalEventArgs args = new ExternalEventArgs();
            args.Action = ExternalAction.ConnectionChanged;
            args.Command = status;
            EventReceived(args);
        }

        public void SendMetric(Metric data) {
            new Thread(() => {
                try {
                    string json = data.ToJson();
                    sendedBytes += System.Text.Encoding.UTF8.GetByteCount(json);
                    CallService(json);
                }
                catch(Exception e) {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace.ToString()); 
                }
            }).Start();
        }

        public bool IsConnected { get { return status == DisplayStatus.Connected; } }

        public DisplayStatus Status { get { return status; } }

        public void Connect() {
            new Thread(() => {
                InitConnection();
                ConnectDisplay();
                speedCalculatorTimer.Enabled = true;
            }).Start();
        }

        private void SpeedCalculatorTimer_Elapsed(object sender, ElapsedEventArgs e) {
            sendedBytesPerSecond = (sendedBytes - sendedBytesOld) / 2;
            sendedBytesOld = sendedBytes;
        }

        public int StreamBandwidthInKb {
            get { return (int)sendedBytesPerSecond; }
        }
    }

    public enum DisplayStatus {
        Disconnected, Connecting, Connected, Closing
    }
}
