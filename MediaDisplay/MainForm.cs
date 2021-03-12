using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MediaDisplay {
    public partial class MainForm : Form {

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        private static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private MediaDisplayServiceWorker worker;

        public MainForm(MediaDisplayServiceWorker worker) {
            InitializeComponent();
            this.worker = worker;
            worker.Listener += Worker_Listener;
            pictureBox1.Image = Icon.ToBitmap();
            trb_frames.Value = 1000 / Convert.ToInt32(worker.Refreshrate);
            lbl_frames_set.Text = Convert.ToString(1000 / Convert.ToInt32(worker.Refreshrate));
            editStatus(worker.ConnectionStatus);
            trb_display_brightness.Value = worker.GetBrightness() / 10;
            lbl_display_brightness_set.Text = Convert.ToString(worker.GetBrightness() / 10);
        }

        private void Worker_Listener(StateChangedArgs changes) {
            Invoke(new MethodInvoker(() => {
                if (changes.Brightness != null) {
                    trb_display_brightness.Value = Convert.ToInt32(changes.Brightness.Value) / 10;
                    trb_display_brightness.Enabled = true;
                    lbl_display_brightness_set.Text = Convert.ToString(changes.Brightness.Value);
                }
                if (changes.DisplayStatus != null) {
                    editStatus(changes.DisplayStatus.Value);
                }
                lbl_bandwidth.Text = changes.StreamBandwidthInKb + " kb/s";
            }
            ));
        }

        private void editStatus(DisplayStatus status) {
            if (status == DisplayStatus.Connected) {
                lbl_connection_status.ForeColor = Color.Green;
            }
            else if (status == DisplayStatus.Connecting) {
                lbl_connection_status.ForeColor = Color.Orange;
            }
            else {
                lbl_connection_status.ForeColor = Color.Red;
            }
            lbl_connection_status.Text = status.ToString("g");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            worker.Listener -= Worker_Listener;
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void TrackBar1_Scroll(object sender, EventArgs e) {
            lbl_frames_set.Text = trb_frames.Value.ToString();
            worker.Refreshrate = 1000 / trb_frames.Value;
        }

        private void trb_display_brightness_Scroll(object sender, EventArgs e) {
            int displayBrightness = trb_display_brightness.Value * 10;
            lbl_display_brightness_set.Text = displayBrightness.ToString();
            worker.SetBrightness(displayBrightness);
        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
        }

        private void btn_close_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
