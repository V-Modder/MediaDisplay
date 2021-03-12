namespace MediaDisplay {
    public class StateChangedArgs {
        public int? Brightness { get; set; }
        public DisplayStatus? DisplayStatus { get; set; }
        public int StreamBandwidthInKb { get; set; }
    }
}