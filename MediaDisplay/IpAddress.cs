using System;
using System.Linq;

namespace MediaDisplay {
    public class IpAddress {

        private IpAddress(string str) {
            Ip = str.Split(':')[0];
            if (str.Split(':').Length > 1) {
                Port = Convert.ToInt32(str.Split(':')[1]);
            }
        }

        public static IpAddress TryParse(string str) {
            try {
                return new IpAddress(str);
            }
            catch(Exception) {
                return null;
            } 
        }

        public static bool Validate(string ipString) {
            if (String.IsNullOrWhiteSpace(ipString)) {
                return false;
            }

            string[] splitValues = ipString.Split('.');
            if (splitValues.Length != 4) {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }

        public string Ip { get; }
        public int? Port { get; }

        public override string ToString() {
            string str = Ip;
            if (Port != null) {
                str += ":" + Convert.ToString(Port);
            }

            return str;
        }
    }
}
