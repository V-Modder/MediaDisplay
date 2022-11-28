using System;
using System.Net.WebSockets;
using System.Timers;
using Websocket.Client;

namespace MediaDisplay {
    public class ExternalDisplayWebSocket : ExternalDisplay {

        private WebsocketClient websocket;

        protected override void CallService(string data) {
            websocket.SendInstant(data);
        }

        public override void Dispose() {
            SetStatus(DisplayStatus.Closing);
            if(websocket != null) {
                Console.WriteLine("websocket close requested");
                websocket.IsReconnectionEnabled = false;
                websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop");
                websocket.Dispose();
                Console.WriteLine("websocket closed");
            }
        }

        protected override void InitConnection() {
            SetStatus(DisplayStatus.Connecting);
            ServerDiscoverer discoverer = new ServerDiscoverer();
            IpAddress address = null;
            do {
                address = discoverer.discover();
            } while ((address == null 
                || !address.Ip.StartsWith("192"))
                && Status != DisplayStatus.Closing);
            
            if(Status != DisplayStatus.Closing) {
                websocket = new WebsocketClient(new Uri($"ws://{address}"));
                websocket.IsReconnectionEnabled = true;
                websocket.ReconnectionHappened.Subscribe(info => {
                    SetStatus(DisplayStatus.Connected);
                    Console.WriteLine($"Reconnection happened, type: {info.Type}");
                });
                websocket.ReconnectTimeout = null;
                websocket.DisconnectionHappened.Subscribe(info => {
                    SetStatus(DisplayStatus.Connecting);
                    Console.WriteLine($"Disconnect happened, type: {info.Type}");
                });
                websocket.MessageReceived.Subscribe(msg => MessageReceived(msg.Text));
            }
        }

        public override void ConnectDisplay() {
            websocket.StartOrFail().Wait();
            SetStatus(DisplayStatus.Connected);
        }

        public override void Disconnect() {
            websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop");
            SetStatus(DisplayStatus.Disconnected);
        }
    }
}
