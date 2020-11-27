using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaDisplay {
    class ExternalDisplayDummy : ExternalDisplay {
        public override void Dispose() {
            Console.WriteLine("ExternalDisplayDummy Dispose() called");
        }

        protected override void callService(byte[] data) {
            Console.WriteLine($"ExternalDisplayDummy callService(), with {data.Length}bytes called");
        }
    }
}
