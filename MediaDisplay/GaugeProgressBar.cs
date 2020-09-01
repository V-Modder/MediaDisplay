using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace MediaDisplay {
    public partial class GaugeProgressBar : Control {
        private int value;
        private Color colorNormal;
        private Color colorHigh;
        private Color colorLimit;
        private Color colorShade;
        private int highThreshold;
        private int limitThreshold;

        public GaugeProgressBar() : base() {
            Value = 50;
            ColorNormal = Color.FromArgb(86, 255, 79);
            ColorHigh = Color.FromArgb(255, 195, 74);
            ColorLimit = Color.FromArgb(255, 90, 90);
            ColorShade = Color.FromArgb(68, 68, 68);
            BackColor = Color.FromArgb(60, 60, 65);
            ForeColor = Color.White;
            Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
            HighThreshold = 80;
            LimitThreshold = 90;
        }

        protected override Size DefaultSize {
            get { return new Size(150, 150); }
        }

        protected override void OnPaint(PaintEventArgs pe) {
            Graphics g = pe.Graphics;
            g.SmoothingMode =  SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            g.FillRectangle(new SolidBrush(BackColor), 0, 0, Width, Height);
            DrawOuterRing(g);
            DrawBackgroundRing(g);
            DrawValueRing(g, DetermineColor());
            DrawText(g);
        }

        private void DrawOuterRing(Graphics g) {
            int arcWidth = Width - 8;
            int arcHeight = Height -8;
            int normalPercent = HighThreshold;
            int highPercent = LimitThreshold - HighThreshold;
            int limitPercent = 100 - LimitThreshold;

            float normalAngel = 220.0f / 100.0f * normalPercent;
            float highAngel = 220.0f / 100.0f * highPercent;
            float limitAngel = 220.0f / 100.0f * limitPercent;

            g.DrawArc(new Pen(ColorNormal, 5), 4, 2, arcWidth, arcHeight, 160, normalAngel);
            g.DrawArc(new Pen(ColorHigh, 5), 4, 2, arcWidth, arcHeight, 160 + normalAngel, highAngel);
            g.DrawArc(new Pen(ColorLimit, 5), 4, 2, arcWidth, arcHeight, 160 + normalAngel + highAngel, limitAngel);
        }

        private void DrawBackgroundRing(Graphics g) {
            var obj = this;
            g.DrawArc(new Pen(ColorShade, 35), 27, 27, Width - 54, Height - 54, 160, 220);
        }

        private void DrawValueRing(Graphics g, Color color) {
            int percent = value;
            if(percent != 100) {
                percent += 1;
            }
            float degree = 220.0f / 100.0f * (float)Convert.ToDecimal(percent);
            g.DrawArc(new Pen(color, 35), 27, 27, Width - 54, Height - 54, 160, degree);
        }

        private void DrawText(Graphics g) {
            string str = string.Format("{0}%", Value);
            Brush b = new SolidBrush(ForeColor);
            SizeF size = g.MeasureString(str, Font);
            g.DrawString(str, Font, b, (Width - size.Width) / 2, ((Height - size.Height) / 2) + (int)(Height * 0.10));
        }

        private Color DetermineColor() {
            if(Value < HighThreshold) {
                return ColorNormal;
            }
            else if(Value < LimitThreshold) {
                return ColorHigh;
            }
            else {
                return ColorLimit;
            }
        }

        public int Value { 
            get {
                return value; 
            } 
            set {
                if (value >= 0 && value <= 100) {
                    this.value = value;
                    Invalidate();
                }
            }
        }

        [Category("Appearance")]
        public Color ColorNormal {
            get {
                return colorNormal;
            }
            set {
                colorNormal = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color ColorHigh {
            get {
                return colorHigh;
            }
            set {
                colorHigh = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color ColorLimit {
            get {
                return colorLimit;
            }
            set {
                colorLimit = value;
                Invalidate();
            }
        }

        [Category("Appearance")]
        public Color ColorShade {
            get {
                return colorShade;
            }
            set {
                colorShade = value;
                Invalidate();
            }
        }

        [Category("Behavior")]
        public int HighThreshold {
            get {
                return highThreshold;
            }
            set {
                if (value >= 0 && value <= limitThreshold) { 
                    highThreshold = value;
                    Invalidate();
                }
            }
        }

        [Category("Behavior")]
        public int LimitThreshold {
            get {
                return limitThreshold;
            }
            set {
                if (value > highThreshold && value <= 100) {
                    limitThreshold = value;
                    Invalidate();
                }
            }
        }
    }
}