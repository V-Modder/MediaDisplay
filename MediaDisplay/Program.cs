using CommandLine;
using System;
using System.Windows.Forms;

namespace MediaDisplay {
    static class Program {

        [STAThread]
        static void Main(string[] args) {
            bool useDummy = false;
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o => {
                       useDummy = o.Dummy;
                   });

            ExternalDisplay externalDisplay;
            if (useDummy) {
                externalDisplay = new ExternalDisplayDummy();
            }
            else {
                externalDisplay = new ExternalDisplayWebSocket();
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(externalDisplay));
        }
    }

    public class Options {
        [Option('d', "dummy", Required = false, HelpText = "Use dummy display")]
        public bool Dummy { get; set; }
    }
}
