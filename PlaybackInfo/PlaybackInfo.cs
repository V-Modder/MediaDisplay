using Newtonsoft.Json;

namespace PlaybackInfoDao
{
    public class PlaybackInfo {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("status")]
        public PlaybackStatus Status { get; set; }

        [JsonProperty("is_status_only")]
        public bool IsStatusOnly { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public enum PlaybackStatus {
        Closed = 0,
        Opened = 1,
        Changing = 2,
        Stopped = 3,
        Playing = 4,
        Paused = 5
    }
}
