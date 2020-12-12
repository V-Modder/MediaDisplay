using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zeroconf;

namespace MediaDisplay {
    class ServerDiscoverer {
        private const string SERVER_NAME = "_mediadisplay"; 

        public IpAddress discover() {
            for(int i=0;i<5;i++) {
                IpAddress address = FindServer();
                if(address != null) {
                    return address;
                }
            }

            return null;
        }

        private IpAddress FindServer() {
            Task<IReadOnlyList<IZeroconfHost>> t = ZeroconfResolver.ResolveAsync("_mediadisplay._tcp.local.", scanTime: new TimeSpan(0,0,6));
            t.Wait();
            return IsMediaDisplayServer(t.Result);
        }

        private IpAddress IsMediaDisplayServer(IReadOnlyList<IZeroconfHost> searchResults) {
            IZeroconfHost host = searchResults.FirstOrDefault(r => r.DisplayName == SERVER_NAME);
            if(host != null) {
                return IpAddress.TryParse(String.Format("{0}:{1}", host.IPAddress, host.Services["_mediadisplay._tcp.local."].Port));
            }

            return null;
        }
    }
}
