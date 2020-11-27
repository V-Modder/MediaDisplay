using System;
using System.Net.WebSockets;
using Websocket.Client;

namespace MediaDisplay {
    public class ExternalDisplayWebSocket : ExternalDisplay {

        private WebsocketClient websocket;

        public ExternalDisplayWebSocket(IpAddress ipAddress) {
            websocket = new WebsocketClient(new Uri($"ws://{ipAddress}"));
            websocket.IsReconnectionEnabled = true;
            websocket.ReconnectionHappened.Subscribe(info =>
               Console.WriteLine($"Reconnection happened, type: {info.Type}"));

            websocket.MessageReceived.Subscribe(msg => messageReceived(msg.Text));
            websocket.StartOrFail().Wait();
        }

        protected override void callService(byte[] data) {
            websocket.Send(data);
        }

        public override void Dispose() {
            Console.WriteLine("websocker close requested");
            websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop").Wait();
            websocket.Dispose();
            Console.WriteLine("websocker closed");
        }
    }
}
