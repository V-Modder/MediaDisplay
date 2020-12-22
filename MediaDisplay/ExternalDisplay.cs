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


        public abstract void Dispose();
        protected abstract void callService(string data);
        protected abstract bool isConnected();

        private static ImageCodecInfo GetEncoder(ImageFormat format) {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

        protected virtual void EventReceived(ExternalEventArgs e) {
            EventHandler<ExternalEventArgs> handler = OnEventReceived;
            handler?.Invoke(this, e);
        }

        protected void messageReceived(string msg) {
            var action = JsonConvert.DeserializeObject<JsonAction>(msg);
            if (action.Action != null) {
                ExternalEventArgs args = new ExternalEventArgs(action);
                EventReceived(args);
            }
        }

        protected static byte[] ConvertToBytes(Bitmap image) {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            MemoryStream memStream = new MemoryStream();
            image.Save(memStream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

            return memStream.ToArray();
        }

        public void sendMetric(Metric data) {
            new Thread(() => {
                try {
                    callService(data.ToJson());
                }
                finally {
                }
            }).Start();
        }

        public bool IsConnected { get { return isConnected(); } }
    }
}
