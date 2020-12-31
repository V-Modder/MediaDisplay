﻿using System;
using System.Net.WebSockets;
using System.Threading;
using Websocket.Client;

namespace MediaDisplay {
    public class ExternalDisplayWebSocket : ExternalDisplay {

        private WebsocketClient websocket;

        protected override void CallService(string data) {
            websocket.Send(data);
        }

        public override void Dispose() {
            Console.WriteLine("websocket close requested");
            websocket.IsReconnectionEnabled = false;
            websocket.Stop(WebSocketCloseStatus.NormalClosure, "Stop");
            websocket.Dispose();
            Console.WriteLine("websocket closed");
        }

        protected override void InitConnection() {
            SetStatus(DisplayStatus.Connecting);
            ServerDiscoverer discoverer = new ServerDiscoverer();
            IpAddress address = null;
            do {
                discoverer.discover();
            } while (address == null);
            websocket = new WebsocketClient(new Uri($"ws://{address}"));
            websocket.IsReconnectionEnabled = true;
            websocket.ReconnectionHappened.Subscribe(info =>
                Console.WriteLine($"Reconnection happened, type: {info.Type}"));
            websocket.ReconnectTimeout = null;
            websocket.MessageReceived.Subscribe(msg => MessageReceived(msg.Text));
            websocket.StartOrFail().Wait();
            SetStatus(DisplayStatus.Connected);
        }
    }
}
