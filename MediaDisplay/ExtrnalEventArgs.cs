using System;

namespace MediaDisplay {
    public class ExternalEventArgs : EventArgs {
        public enum ExternalAction { click=1 }
        public ExternalEventArgs() { }

        public ExternalEventArgs(JsonAction action) {
            this.Action = (ExternalAction) Enum.Parse(typeof(ExternalAction), action.Action);
            this.X = action.X;
            this.Y = action.Y;
        }

        public ExternalAction Action { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
