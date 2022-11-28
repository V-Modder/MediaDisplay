using CommandLine;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace MediaDisplay {
    class Program {

        private ToolStripMenuItem showToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip;
        private NotifyIcon icon;
        private ExternalDisplay externalDisplay;
        private MediaDisplayServiceWorker worker;
        private MainForm mainForm;

        [STAThread]
        static void Main(string[] args) {
            bool useDummy = false;
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed(o => {
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

            Program program = new Program(externalDisplay);
            program.Start();

            Application.Run();
            Console.WriteLine("test");
        }

        public Program(ExternalDisplay externalDisplay) {
            this.externalDisplay = externalDisplay;
        }

        public void Start() {
            worker = new MediaDisplayServiceWorker(externalDisplay);
            worker.Start();

            showToolStripMenuItem = new ToolStripMenuItem();
            showToolStripMenuItem.Name = "showToolStripMenuItem";
            showToolStripMenuItem.Size = new Size(103, 22);
            showToolStripMenuItem.Text = "Show";
            showToolStripMenuItem.Click += new EventHandler(ShowToolStripMenuItem_Click);

            exitToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(103, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += new EventHandler(ExitToolStripMenuItem_Click);

            contextMenuStrip = new ContextMenuStrip();
            contextMenuStrip.Items.AddRange(new ToolStripItem[] {
                showToolStripMenuItem,
                exitToolStripMenuItem});
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(104, 48);

            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            icon = new NotifyIcon();
            icon.ContextMenuStrip = contextMenuStrip;
            icon.Icon = (Icon)resources.GetObject("$this.Icon");
            icon.Text = "Media-Display";
            icon.Visible = true;
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e) {
            if (mainForm == null) {
                showToolStripMenuItem.Text = "Hide";
                mainForm = new MainForm(worker);
                mainForm.Show();
            }
            else {
                showToolStripMenuItem.Text = "Show";
                mainForm.Close();
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            if (mainForm != null) {
                mainForm.Close();
            }

            worker.Stop();
            externalDisplay.Dispose();
            Application.Exit();
        }
    }

    public class Options {
        [Option('d', "dummy", Required = false, HelpText = "Use dummy display")]
        public bool Dummy { get; set; }
    }
}
