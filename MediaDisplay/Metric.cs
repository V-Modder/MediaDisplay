using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaDisplay {
    public class Metric {
        public Metric() {
            Cpus = new List<Cpu>();
            Gpu = new Gpu();
            Network = new Network();
        }

        [JsonProperty("cpus")]
        public List<Cpu> Cpus { get; set; }

        [JsonProperty("gpu")]
        public Gpu Gpu { get; set; }

        [JsonProperty("memory_load")]
        public int MemoryLoad { get; set; }

        [JsonProperty("network")]
        public Network Network { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("room_temperature")]
        public double RoomTemperature { get; set; }

        public static Metric FromJson(string json) => JsonConvert.DeserializeObject<Metric>(json);
        public string ToJson() => JsonConvert.SerializeObject(this);
    }

    public class Cpu {
        public Cpu(int load, double temperatur) {
            Load = load;
            Temperature = temperatur;
        }

        [JsonProperty("load")]
        public int Load { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }

    public class Gpu {
        public Gpu() { }

        public Gpu(int load, int memoryLoad, double temperature) {
            Load = load;
            MemoryLoad = memoryLoad;
            Temperature = temperature;
        }

        [JsonProperty("load")]
        public int Load { get; set; }

        [JsonProperty("memory_load")]
        public int MemoryLoad { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }
    }

    public class Network {
        public Network() {
        }

        public Network(string up, string down) {
            Up = up;
            Down = down;
        }

        [JsonProperty("up")]
        public string Up { get; set; }
        [JsonProperty("down")]
        public string Down { get; set; }
    }
}
