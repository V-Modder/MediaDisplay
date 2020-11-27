using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using static MediaDisplay.ExternalEventArgs;
using static MediaDisplay.SendInputWrapper;

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

        private List<Panel> panels;
        private NetworkMonitor networkMonitor;

        public MainForm(ExternalDisplay externalDisplay) {
            WindowState = FormWindowState.Minimized;
            this.externalDisplay = externalDisplay;
            this.externalDisplay.OnEventReceived += ExternalDisplay_EventReceived;
            InitializeComponent();
            panels = new List<Panel>() {
                pan_1,
                pan_2
            };
            Hide();

            temper = new Temper(Handle);
            initDevices();
        }

        private void initDevices() {
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
                sensor.Sensor = hw.Sensors.Where(s => s.Name == sensor.SensorName && s.SensorType == sensor.SensorType).FirstOrDefault();
            }

            networkMonitor = new NetworkMonitor();
            networkMonitor.StartMonitoring();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            networkMonitor.StopMonitoring();
            externalDisplay.Dispose();
        }

        private void ExternalDisplay_EventReceived(object sender, ExternalEventArgs e) {
            if(e.Action == ExternalAction.click) {
                BeginInvoke(new MethodInvoker(delegate {
                    Control panel = pan_preview;
                    int y = e.Y;
                    if (e.Y >=62) {
                        panel = panels.First(p => p.Visible);
                        y = e.Y - 62;
                    }

                    Control ctrl = panel.GetChildAtPoint(new Point(e.X, y));
                    if(ctrl != null) {
                        Type t = ctrl.GetType();
                        var evt = t.GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
                        if(evt != null) {
                            MouseEventArgs args = new MouseEventArgs(MouseButtons.Left, 1, e.X, y, 0);
                            evt.Invoke(ctrl, new object[] { args });
                        }
                    }
                }));
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
            Close();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e) {
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

        private void drawTimer_Tick(object sender, EventArgs e) {
            using (Bitmap bmp = new Bitmap(pan_preview.Width, pan_preview.Height)) {
                pan_preview.DrawToBitmap(bmp, new Rectangle(0, 0, pan_preview.Width, pan_preview.Height));
                externalDisplay.sendImage(bmp);
            }
        }

        private object getValueFromSensor(string sensorName) {
            var sensor = devices.Where(d => d.Name == sensorName).FirstOrDefault();
            if(sensor != null && sensor.Sensor != null) {
                return sensor.Sensor.Value;
            }

            return null;
        }

        private void clockTimer_Tick(object sender, EventArgs e) {
            lbl_time.Text = DateTime.Now.ToString("HH:mm:ss");

            new Thread(() => {
                string text = "-- °C";
                try {
                    text = String.Format("{0:0.0} °C", temper.getTemp());
                }
                catch (IOException) {
                }

                if (lbl_room_temp.InvokeRequired) {
                    lbl_room_temp.Invoke((MethodInvoker)delegate {
                        // Running on the UI thread
                        lbl_room_temp.Text = text;
                    });
                }
                else {
                    lbl_room_temp.Text = text;
                }
            }).Start();

            updateAllDevices();
            pgb_cpu_load_1.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_1"));
            pgb_cpu_load_2.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_2"));
            pgb_cpu_load_3.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_3"));
            pgb_cpu_load_4.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_4"));
            pgb_cpu_load_5.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_5"));
            pgb_cpu_load_6.Value = Convert.ToInt32(getValueFromSensor("CPU_LOAD_6"));
            lbl_cpu_temp_1.Text = getValueFromSensor("CPU_TEMP_1").ToString() + "°C";
            lbl_cpu_temp_2.Text = getValueFromSensor("CPU_TEMP_2").ToString() + "°C";
            lbl_cpu_temp_3.Text = getValueFromSensor("CPU_TEMP_3").ToString() + "°C";
            lbl_cpu_temp_4.Text = getValueFromSensor("CPU_TEMP_4").ToString() + "°C";
            lbl_cpu_temp_5.Text = getValueFromSensor("CPU_TEMP_5").ToString() + "°C";
            lbl_cpu_temp_6.Text = getValueFromSensor("CPU_TEMP_6").ToString() + "°C";

            pgb_memory_load.Value = Convert.ToInt32(getValueFromSensor("MEM_LOAD"));

            pgb_gpu_load.Value = Convert.ToInt32(getValueFromSensor("GPU_LOAD"));
            pgb_gpu_mem_load.Value = Convert.ToInt32(getValueFromSensor("GPU_MEM_LOAD"));
            lbl_gpu_temp.Text = getValueFromSensor("GPU_TEMP").ToString() + "°C";

            lbl_net_down.Text = networkMonitor.sumDownloadSpeed();
            lbl_net_up.Text = networkMonitor.sumUploadSpeed();
        }

        private void updateAllDevices() {
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

        private void trackBar1_Scroll(object sender, EventArgs e) {
            lbl_frames_set.Text = trb_frames.Value.ToString();
            drawTimer.Interval = 1000 / trb_frames.Value;
        }

        private void lbl_panel_1_Click(object sender, EventArgs e) {
            switch_panel(pan_1);
        }

        private void lbl_panel_2_Click(object sender, EventArgs e) {
            switch_panel(pan_2);
        }

        private void switch_panel(Panel panelToShow) {
            panels.ForEach(p => p.Visible = false);
            panelToShow.Visible = true;
        }

        private void lbl_play_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.MEDIA_PLAY_PAUSE);
        }

        private void lbl_stop_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.MEDIA_STOP);
        }

        private void lbl_previous_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.MEDIA_PREV_TRACK);
        }

        private void lbl_next_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.MEDIA_NEXT_TRACK);
        }

        private void lbl_volume_up_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.VOLUME_UP);
        }

        private void lbl_volume_down_Click(object sender, EventArgs e) {
            SendKeyPress(KeyCode.VOLUME_DOWN);
        }
    }
}
