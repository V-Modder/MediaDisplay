using OpenHardwareMonitor.Hardware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
// System.Threading;
using System.Threading.Tasks;
using System.Timers;
using static MediaDisplay.SendInputWrapper;

namespace MediaDisplay {
    public class MediaDisplayServiceWorker {
        public delegate void StateChangedEventHandler(StateChangedArgs changes);

        private ExternalDisplay externalDisplay;
        private DisplayStatus displayStatus;
        private Temper temper;
        private NetworkMonitor networkMonitor;
        private PlaybackInfoPipeClient playbackInfoPipeClient;
        private bool brightnessChanged;
        private int displayBrightness;
        private bool getBrightness;
        //private Thread thread;
        private System.Timers.Timer timer;
        private StateChangedEventHandler listeners;
        private bool running;

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

        public MediaDisplayServiceWorker(ExternalDisplay externalDisplay) {
            this.externalDisplay = externalDisplay;
            this.externalDisplay.OnEventReceived += ExternalDisplay_EventReceived;
            running = false;
            temper = new Temper(IntPtr.Zero);
            brightnessChanged = false;
            getBrightness = true;
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += RefreshTimer_Tick;
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
            playbackInfoPipeClient = new PlaybackInfoPipeClient();
        }

        public event StateChangedEventHandler Listener {
            add { listeners += value; }
            remove { listeners -= value; }
        }

        public void Start() {
            if (!IsRunning) {
                running = true;
                networkMonitor.StartMonitoring();
                playbackInfoPipeClient.Start();
                externalDisplay.Connect();
                timer.Start();
            }
        }

        public void Stop() {
            if (IsRunning) {
                if (timer.Enabled) {
                    timer.Stop();
                    timer.Close();
                }
                networkMonitor.StopMonitoring();
                playbackInfoPipeClient.Stop();
            }
        }

        public bool IsRunning {
            get{ return running; }
        }

        private void ExternalDisplay_EventReceived(object sender, ExternalEventArgs e) {
            if (e.Action == ExternalAction.Click) {
                KeyCode? key;
                switch (e.Command) {
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
                if (key != null) {
                    SendKeyPress(key.Value);
                }
            }
            else {
                StateChangedArgs args = new StateChangedArgs();
                if (e.Action == ExternalAction.Brightness) {
                    displayBrightness = Convert.ToInt32(e.Command);
                    args.Brightness = displayBrightness;
                }
                else if(e.Action == ExternalAction.ConnectionChanged) {
                    args.DisplayStatus = (DisplayStatus)e.Command;
                }
                listeners?.BeginInvoke(args, (ar) => { }, null);  
            }
        }

        private object GetValueFromSensor(string sensorName) {
            var sensor = devices.Where(d => d.Name == sensorName).FirstOrDefault();
            if (sensor != null && sensor.Sensor != null) {
                return sensor.Sensor.Value;
            }

            return null;
        }

        private void RefreshTimer_Tick(object sender, ElapsedEventArgs e) {
            StateChangedArgs args = new StateChangedArgs();
            if(displayStatus != externalDisplay.Status) {
                displayStatus = externalDisplay.Status;
                args.DisplayStatus = displayStatus;
            }

            if (displayStatus == DisplayStatus.Connected) {
                RefreshDataToDisplay();
            }
            args.StreamBandwidthInKb = externalDisplay.StreamBandwidthInKb;
            listeners?.BeginInvoke(args, (ar) => { }, null);
        }

        private void UpdateAllDevices() {
            devices.GroupBy(d => d.Sensor.Hardware)
                .ToList()
                .ForEach(h => h.Key.Update());
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
            catch (Exception e) {
                metric.RoomTemperature = 0;
            }
            metric.Time = DateTime.Now.ToString("HH:mm");

            if (playbackInfoPipeClient.HasChanges) {
                metric.PlaybackInfo = playbackInfoPipeClient.GetPlaybackInfo();
            }

            if (brightnessChanged) {
                metric.DisplayBrightness = displayBrightness;
            }

            if (getBrightness) {
                getBrightness = false;
                metric.SendDisplayBrightness = true;
            }

            externalDisplay.SendMetric(metric);
        }

        public void GetBrightnessByNextEvent() {
            getBrightness = true;
        }

        public int GetBrightness() {
            return displayBrightness;
        }

        public void SetBrightness(int brightness) {
            displayBrightness = brightness;
            brightnessChanged = true;
        }

        public double Refreshrate{
            get { return timer.Interval; }
            set { timer.Interval = value; }
        }

        public DisplayStatus ConnectionStatus {
            get { return externalDisplay.Status; }
        }
    }
}
