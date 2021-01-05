using System;
using System.Drawing;
using System.Windows.Forms;

namespace MediaDisplay {
    class WindowButton : Button {

        private Color drawingBackColor;
        private Color drawingForeColor;

        public WindowButton() {
            HighlightBackColor = Color.Gray;
            HighlightForeColor = Color.White;
            BackColor = Color.Black;
            drawingBackColor = BackColor;
            ForeColor = Color.Gray;
            drawingForeColor = ForeColor;
            WindowButtonType = WindowButtonType.Minimize;
            Size = new Size(32, 32);
        }

        public Color HighlightBackColor { get; set; }
        public Color HighlightForeColor { get; set; }
        public WindowButtonType WindowButtonType { get; set; }

        protected override void OnPaint(PaintEventArgs pe) {
            pe.Graphics.FillRectangle(new SolidBrush(drawingBackColor), 0, 0, Width, Height);
            switch(WindowButtonType) {
                case WindowButtonType.Minimize: PaintMinimize(pe.Graphics); break;
                case WindowButtonType.Maximize: PaintMaximize(pe.Graphics); break;
                case WindowButtonType.Close: PaintClose(pe.Graphics);  break;
            }
        }

        private void PaintMinimize(Graphics g) {
            Pen pen = new Pen(drawingForeColor, 3);
            g.DrawLine(pen, 11, 21, 21, 21);
        }

        private void PaintMaximize(Graphics g) {
            Pen pen = new Pen(drawingForeColor, 3);
            g.DrawRectangle(pen, 11, 11, 21, 21);
        }

        private void PaintClose(Graphics g) {
            Pen pen = new Pen(drawingForeColor, 3);
            g.DrawLine(pen, 11, 11, 21, 21);
            g.DrawLine(pen, 21, 11, 11, 21);
        }

        protected override void OnMouseEnter(EventArgs e) {
            drawingBackColor = HighlightBackColor;
            drawingForeColor = HighlightForeColor;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e) {
            drawingBackColor = BackColor;
            drawingForeColor = ForeColor;
            base.OnMouseLeave(e);
        }
    }

    public enum WindowButtonType {
        Minimize, Maximize, Close
    }
}
