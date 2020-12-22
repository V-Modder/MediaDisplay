using System;

namespace MediaDisplay {
    class ExternalDisplayDummy : ExternalDisplay {
        public override void Dispose() {
            Console.WriteLine("ExternalDisplayDummy Dispose() called");
        }

        protected override void callService(string data) {
            Console.WriteLine($"ExternalDisplayDummy callService(), with {data} called");
        }

        protected override bool isConnected() {
            return true;
        }
    }
}
