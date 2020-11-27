using CommandLine;
using CommandLine.Text;
using System;
using System.Windows.Forms;

namespace MediaDisplay {
    static class Program {

        [STAThread]
        static void Main(string[] args) {
            string addressStr = null;
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => {
                       addressStr = o.Address;
                   });

            ExternalDisplay externalDisplay;
            IpAddress address = IpAddress.TryParse(addressStr);
            if (address != null) {
                externalDisplay = new ExternalDisplayWebSocket(address);
            }
            else {
                externalDisplay = new ExternalDisplayDummy();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(externalDisplay));
        }
    }

    public class Options {
        [Option('a', "address", Required = false, HelpText = "Set external display id.")]
        public string Address { get; set; }
    }

}
