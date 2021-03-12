using Newtonsoft.Json;
using System;

namespace MediaDisplay {
    public class ExternalEventArgs : EventArgs {
        public static ExternalEventArgs FromJson(string json) => JsonConvert.DeserializeObject<ExternalEventArgs>(json);

        public string ToJson() {
            var settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Include;
            return JsonConvert.SerializeObject(this, settings);
        }

        [JsonProperty("action")]
        public ExternalAction Action { get; set; }

        [JsonProperty("command")]
        public object Command { get; set; }
    }

    public enum ExternalAction { Click=1, Brightness=2, ConnectionChanged=3 }
}
