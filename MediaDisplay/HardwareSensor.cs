using OpenHardwareMonitor.Hardware;

namespace MediaDisplay {
    class HardwareSensor {

        public HardwareSensor(string Name, string HardwareName, string SensorName, SensorType SensorType) {
            this.Name = Name;
            this.HardwareName = HardwareName;
            this.SensorName = SensorName;
            this.SensorType = SensorType;
        }

        public ISensor Sensor { get; set; }
        public string Name { get; set; }
        public string HardwareName { get; set; }
        public string SensorName { get; set; }
        public SensorType SensorType { get; set; }
    }
}
