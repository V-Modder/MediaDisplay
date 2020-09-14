using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using Websocket.Client;
using Encoder = System.Drawing.Imaging.Encoder;

namespace MediaDisplay {
    class ExternalDisplay : IDisposable {

        private WebsocketClient websocket;
        public event EventHandler<ExternalEventArgs> OnEventReceived;
        public delegate void EventReceivedEventHandler<ExternalEventArgs>(object sender, ExternalEventArgs e);

        public ExternalDisplay(string ip, int port) {
            websocket = new WebsocketClient(new Uri($"ws://{ip}:{port}"));
            websocket.IsReconnectionEnabled = true;
            websocket.ReconnectionHappened.Subscribe(info =>
               Console.WriteLine($"Reconnection happened, type: {info.Type}"));

            websocket.MessageReceived.Subscribe(msg => messageReceived(msg));
            websocket.StartOrFail().Wait();
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format) {
            var codecs = ImageCodecInfo.GetImageDecoders();
            foreach (var codec in codecs) {
                if (codec.FormatID == format.Guid) {
                    return codec;
                }
            }
            return null;
        }

        private void messageReceived(ResponseMessage msg) {
            var action = JsonConvert.DeserializeObject<JsonAction>(msg.Text);
            if (action.Action != null) {
                ExternalEventArgs args = new ExternalEventArgs(action);
                EventReceived(args);
            }
        }

        private static byte[] ConvertToBytes(Bitmap image) {
            var encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
            MemoryStream memStream = new MemoryStream();
            image.Save(memStream, GetEncoder(ImageFormat.Jpeg), encoderParameters);

            return memStream.ToArray();
        }

        public void sync(Bitmap image) {
            Bitmap imageClone = new Bitmap(image);
            new Thread(() => {
                byte[] data = ConvertToBytes(imageClone);
                
                try {
                    callService(data);
                }
                finally {
                    imageClone.Dispose();
                }
            }).Start(); 
        }

        protected virtual void EventReceived(ExternalEventArgs e) {
            EventHandler<ExternalEventArgs> handler = OnEventReceived;
            handler?.Invoke(this, e);
        }

        private void callService(byte[] data) {
            websocket.Send(data);
        }

        public void Dispose() {
            Console.WriteLine("websocker close requested");
            websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop").Wait();
            websocket.Dispose();
            Console.WriteLine("websocker closed");
        }
    }
}
