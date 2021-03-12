using System;
using System.Threading;

namespace MediaDisplay {
    class ExternalDisplayDummy : ExternalDisplay {
        public override void Dispose() {
            Console.WriteLine("ExternalDisplayDummy Dispose() called");
        }

        protected override void CallService(string data) {
            //Console.WriteLine($"ExternalDisplayDummy callService(), with {data} called");
        }

        protected override void InitConnection() {
            SetStatus(DisplayStatus.Connecting);
            Thread.Sleep(10000);
            SetStatus(DisplayStatus.Connected);
        }
    }
}
