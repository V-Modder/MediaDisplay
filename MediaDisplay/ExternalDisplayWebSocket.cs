﻿using System;
using System.Net.WebSockets;
using Websocket.Client;

namespace MediaDisplay {
    public class ExternalDisplayWebSocket : ExternalDisplay {

        private WebsocketClient websocket;

        public ExternalDisplayWebSocket() {
            ServerDiscoverer discoverer = new ServerDiscoverer();
            IpAddress address = discoverer.discover();
            websocket = new WebsocketClient(new Uri($"ws://{address}"));
            websocket.IsReconnectionEnabled = true;
            websocket.ReconnectionHappened.Subscribe(info =>
               Console.WriteLine($"Reconnection happened, type: {info.Type}"));

            websocket.MessageReceived.Subscribe(msg => messageReceived(msg.Text));
            websocket.StartOrFail().Wait();
        }

        protected override void callService(string data) {
            websocket.Send(data);
        }

        public override void Dispose() {
            Console.WriteLine("websocker close requested");
            websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop").Wait();
            websocket.Dispose();
            Console.WriteLine("websocker closed");
        }

        protected override bool isConnected() {
            return websocket.IsRunning;
        }
    }
}
