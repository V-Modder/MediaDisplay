using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Threading;
using Encoder = System.Drawing.Imaging.Encoder;

namespace MediaDisplay {
    class ExternalDisplay {

        private string url;
        private RestClient client;
        public event EventHandler<ExternalEventArgs> OnEventReceived;
        public delegate void EventReceivedEventHandler<ExternalEventArgs>(object sender, ExternalEventArgs e);

        public ExternalDisplay(string url) {
            this.url = url;
            client = new RestClient(url);
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
                string answer;
                try {
                    answer = callService(data);
                }
                finally {
                    imageClone.Dispose();
                }

                if(answer != null) {
                    var action = JsonConvert.DeserializeObject<JsonAction>(answer);
                    if (action.Action != null) {
                        ExternalEventArgs args = new ExternalEventArgs(action);
                        EventReceived(args);
                    }
                }
            }).Start(); 
        }

        protected virtual void EventReceived(ExternalEventArgs e) {
            EventHandler<ExternalEventArgs> handler = OnEventReceived;
            handler?.Invoke(this, e);
        }

        private string callService(byte[] data) {
            RestClient client = new RestClient(url);
            RestRequest request = new RestRequest(Method.POST);
            request.AddFileBytes("render_image", data, "render_image.jpg", "image/jpeg");
            //request.Timeout = 2000;

            string content;
            try { 
                var response = client.Execute(request);
                if (response.StatusCode != HttpStatusCode.OK) {
                    return null;
                }
                content = response.Content;
            }
            catch(Exception e) {
                Console.WriteLine(e.Message);
                throw e;
            }
            finally {
                 data = null;
            }

            return content;
        }
    }
}
