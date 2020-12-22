using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using OpenHardwareMonitor.Hardware;

namespace MediaDisplay {
    public partial class MainForm : Form {
        private ExternalDisplay externalDisplay;
        private Temper temper;
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

        private NetworkMonitor networkMonitor;

        public MainForm(ExternalDisplay externalDisplay) {
            this.externalDisplay = externalDisplay;
            this.externalDisplay.OnEventReceived += ExternalDisplay_EventReceived;

            Hide();
            InitializeComponent();
            temper = new Temper(Handle);
            InitDevices();
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
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            networkMonitor.StopMonitoring();
            externalDisplay.Dispose();
        }

        private void ExternalDisplay_EventReceived(object sender, ExternalEventArgs e) {
            //if(e.Action == ExternalAction.click) {
            //    BeginInvoke(new MethodInvoker(delegate {
            //        Control panel = pan_preview;
            //        int y = e.Y;
            //        if (e.Y >=62) {
            //            panel = panels.First(p => p.Visible);
            //            y = e.Y - 62;
            //        }

            //        Control ctrl = panel.GetChildAtPoint(new Point(e.X, y));
            //        if(ctrl != null) {
            //            Type t = ctrl.GetType();
            //            var evt = t.GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            //            if(evt != null) {
            //                MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 1, e.X, y, 0);
            //                evt.Invoke(ctrl, new object[] { args });
            //            }
            //        }
            //    }));
            //}
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
                }

                new Thread(() => RefreshDataToDisplay()).Start();
            }
            else if (!externalDisplay.IsConnected && lbl_connection_status.Text != "Disconnected"){
                lbl_connection_status.Text = "Disconnected";
                lbl_connection_status.ForeColor = Color.Red;
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

            metric.RoomTemperature = temper.getTemp();
            metric.Time = DateTime.Now.ToString("HH:mm");

            externalDisplay.sendMetric(metric);
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

        //private void lbl_play_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.MEDIA_PLAY_PAUSE);
        //}

        //private void lbl_stop_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.MEDIA_STOP);
        //}

        //private void lbl_previous_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.MEDIA_PREV_TRACK);
        //}

        //private void lbl_next_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.MEDIA_NEXT_TRACK);
        //}

        //private void lbl_volume_up_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.VOLUME_UP);
        //}

        //private void lbl_volume_down_Click(object sender, EventArgs e) {
        //    SendKeyPress(KeyCode.VOLUME_DOWN);
        //}
    }
}
