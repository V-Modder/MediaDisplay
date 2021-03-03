using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;
using static MediaDisplay.SendInputWrapper;

namespace MediaDisplay {
    public partial class MainForm : Form {
        private ExternalDisplay externalDisplay;
        private Temper temper;
        private NetworkMonitor networkMonitor;
        private PlaybackInfoPipeClient playbackInfoPipeClient;
        private bool brightnessChanged;
        private int displayBrightness;
        private bool getBrightness;
        private List<HardwareSensor> devices = new List<HardwareSensor>() {
            new HardwareSensor("CPU_LOAD_1", "Intel Core i5-10600K", "CPU Core #1", SensorType.Load),
            new HardwareSensor("CPU_LOAD_2", "Intel Core i5-10600K", "CPU Core #2", SensorType.Load),
            new HardwareSensor("CPU_LOAD_3", "Intel Core i5-10600K", "CPU Core #3", SensorType.Load),
            new HardwareSensor("CPU_LOAD_4", "Intel Core i5-10600K", "CPU Core #4", SensorType.Load),
            new HardwareSensor("CPU_LOAD_5", "Intel Core i5-10600K", "CPU Core #5", SensorType.Load),
            new HardwareSensor("CPU_LOAD_6", "Intel Core i5-10600K", "CPU Core #6", SensorType.Load),
            new HardwareSensor("CPU_TEMP_1", "Intel Core i5-10600K", "CPU Core #1", SensorType.Temperature),
            new HardwareSensor("CPU_TEMP_2", "Intel Core i5-10600K", "CPU Core #2", SensorType.Temperature),
            new HardwareSensor("CPU_TEMP_3", "Intel Core i5-10600K", "CPU Core #3", SensorType.Temperature),
            new HardwareSensor("CPU_TEMP_4", "Intel Core i5-10600K", "CPU Core #4", SensorType.Temperature),
            new HardwareSensor("CPU_TEMP_5", "Intel Core i5-10600K", "CPU Core #5", SensorType.Temperature),
            new HardwareSensor("CPU_TEMP_6", "Intel Core i5-10600K", "CPU Core #6", SensorType.Temperature),

            new HardwareSensor("MEM_LOAD", "Generic Memory", "Memory", SensorType.Load),
            new HardwareSensor("MEM_LOAD", "Generic Memory", "Used Memory", SensorType.Data),
            new HardwareSensor("MEM_LOAD", "Generic Memory", "Available Memory", SensorType.Data),

            new HardwareSensor("GPU_TEMP", "NVIDIA GeForce RTX 2070", "GPU Core", SensorType.Temperature),
            new HardwareSensor("GPU_LOAD", "NVIDIA GeForce RTX 2070", "GPU Core", SensorType.Load),
            new HardwareSensor("GPU_MEM_LOAD", "NVIDIA GeForce RTX 2070", "GPU Memory", SensorType.Load)
        };

        public MainForm(ExternalDisplay externalDisplay) {
            this.externalDisplay = externalDisplay;
            this.externalDisplay.OnEventReceived += ExternalDisplay_EventReceived;

            InitializeComponent();
            pictureBox1.Image = Icon.ToBitmap();
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            externalDisplay.Connect();
            temper = new Temper(Handle);
            InitDevices();
            brightnessChanged = false;
            getBrightness = false;
        }

        private void InitDevices() {
            Computer computer = new Computer();
            computer.CPUEnabled = true;
            computer.FanControllerEnabled = true;
            computer.GPUEnabled = true;
            computer.HDDEnabled = false;
            computer.MainboardEnabled = false;
            computer.RAMEnabled = true;
            computer.Open();

            foreach (HardwareSensor sensor in devices) {
                IHardware hw = computer.Hardware.Where(h => h.Name == sensor.HardwareName).First();
                sensor.Sensor = hw.Sensors
                    .Where(s => s.Name == sensor.SensorName && s.SensorType == sensor.SensorType)
                    .FirstOrDefault();
            }

            networkMonitor = new NetworkMonitor();
            networkMonitor.StartMonitoring();
            playbackInfoPipeClient = new PlaybackInfoPipeClient();
            playbackInfoPipeClient.Start();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            refreshTimer.Stop();
            networkMonitor.StopMonitoring();
            externalDisplay.Dispose();
            playbackInfoPipeClient.Stop();
        }

        private void ExternalDisplay_EventReceived(object sender, ExternalEventArgs e) {
            if (e.Action == ExternalAction.Click) {
                KeyCode? key;
                switch(e.Command) {
                    case "key_next":
                        key = KeyCode.MEDIA_NEXT_TRACK;
                        break;
                    case "key_play_pause":
                        key = KeyCode.MEDIA_PLAY_PAUSE;
                        break;
                    case "key_previous":
                        key = KeyCode.MEDIA_PREV_TRACK;
                        break;
                    case "key_stop":
                        key = KeyCode.MEDIA_STOP;
                        break;
                    case "key_volume_down":
                        key = KeyCode.VOLUME_DOWN;
                        break;
                    case "key_volume_up":
                        key = KeyCode.VOLUME_UP;
                        break;
                    default:
                        key = null;
                        break;
                }
                if(key != null) {
                    SendKeyPress(key.Value);
                }
            }
            else if(e.Action == ExternalAction.Brightness) {
                Invoke(new MethodInvoker(
                    () => {
                        trb_display_brightness.Value = Convert.ToInt32(e.Command) / 10;
                        trb_display_brightness.Enabled = true;
                        lbl_display_brightness_set.Text = e.Command;
                    }
                ));
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void ShowToolStripMenuItem_Click(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized) {
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
                Show();
                showToolStripMenuItem.Text = "Hide";
            }
            else {
                WindowState = FormWindowState.Minimized;
                showToolStripMenuItem.Text = "Show";
            }
        }

        private object GetValueFromSensor(string sensorName) {
            var sensor = devices.Where(d => d.Name == sensorName).FirstOrDefault();
            if(sensor != null && sensor.Sensor != null) {
                return sensor.Sensor.Value;
            }

            return null;
        }

        private void RefreshTimer_Tick(object sender, EventArgs e) {
            if (externalDisplay.IsConnected) {
                if (lbl_connection_status.Text != "Connected") {
                    lbl_connection_status.Text = "Connected";
                    lbl_connection_status.ForeColor = Color.Green;
                    getBrightness = true;
                }

                new Thread(() => RefreshDataToDisplay()).Start();
                lbl_bandwidth.Text = string.Format("{0} kb/s", externalDisplay.StreamBandwidthInKb);
            }
            else if (externalDisplay.Status == DisplayStatus.Disconnected && lbl_connection_status.Text != "Disconnected"){
                lbl_connection_status.Text = "Disconnected";
                lbl_connection_status.ForeColor = Color.Red;
                trb_display_brightness.Enabled = false;
            }
            else if (externalDisplay.Status == DisplayStatus.Connecting && lbl_connection_status.Text != "Connecting") {
                lbl_connection_status.Text = "Connecting";
                lbl_connection_status.ForeColor = Color.Orange;
                trb_display_brightness.Enabled = false;
            }
        }

        private void RefreshDataToDisplay() {
            UpdateAllDevices();

            Metric metric = new Metric();

            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_1")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_1"))));
            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_2")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_2"))));
            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_3")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_3"))));
            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_4")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_4"))));
            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_5")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_5"))));
            metric.Cpus.Add(new Cpu(Convert.ToInt32(GetValueFromSensor("CPU_LOAD_6")), Convert.ToDouble(GetValueFromSensor("CPU_TEMP_6"))));

            metric.MemoryLoad = Convert.ToInt32(GetValueFromSensor("MEM_LOAD"));

            metric.Gpu.Load = Convert.ToInt32(GetValueFromSensor("GPU_LOAD"));
            metric.Gpu.MemoryLoad = Convert.ToInt32(GetValueFromSensor("GPU_MEM_LOAD"));
            metric.Gpu.Temperature = Convert.ToDouble(GetValueFromSensor("GPU_TEMP"));

            metric.Network.Down = networkMonitor.sumDownloadSpeed();
            metric.Network.Up = networkMonitor.sumUploadSpeed();

            try {
                metric.RoomTemperature = temper.GetTemp();
            }
            catch(Exception e) {
                metric.RoomTemperature = 0;
            }
            metric.Time = DateTime.Now.ToString("HH:mm");

            if(playbackInfoPipeClient.HasChanges) {
                metric.PlaybackInfo = playbackInfoPipeClient.GetPlaybackInfo();
            }

            if(trb_display_brightness.Enabled && brightnessChanged) {
                metric.DisplayBrightness = displayBrightness;
            }

            if(getBrightness) {
                getBrightness = false;
                metric.SendDisplayBrightness = true;
            }

            externalDisplay.SendMetric(metric);
        }

        private void UpdateAllDevices() {
            devices.GroupBy(d => d.Sensor.Hardware)
                .ToList()
                .ForEach(h => h.Key.Update());
        }

        private void MainForm_Resize(object sender, EventArgs e) {
            if (WindowState == FormWindowState.Minimized && ShowInTaskbar) {
                ShowInTaskbar = false;
                Hide();
            }
        }

        private void TrackBar1_Scroll(object sender, EventArgs e) {
            lbl_frames_set.Text = trb_frames.Value.ToString();
            refreshTimer.Interval = 1000 / trb_frames.Value;
        }

        private void trb_display_brightness_Scroll(object sender, EventArgs e) {
            displayBrightness = trb_display_brightness.Value * 10;
            lbl_display_brightness_set.Text = displayBrightness.ToString();
            brightnessChanged = true;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        private void MainForm_MouseDown(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void btn_minimize_Click(object sender, EventArgs e) {
            WindowState = FormWindowState.Minimized;
            showToolStripMenuItem.Text = "Show";
        }

        private void btn_close_Click(object sender, EventArgs e) {
            Close();
        }
    }
}
