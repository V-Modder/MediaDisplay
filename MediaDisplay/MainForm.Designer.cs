namespace MediaDisplay {
    partial class MainForm {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drawTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_preview = new System.Windows.Forms.Label();
            this.trb_frames = new System.Windows.Forms.TrackBar();
            this.lbl_frames = new System.Windows.Forms.Label();
            this.lbl_frames_set = new System.Windows.Forms.Label();
            this.clockTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_cpu = new System.Windows.Forms.Label();
            this.pan_2 = new System.Windows.Forms.Panel();
            this.lbl_volume_down = new System.Windows.Forms.Label();
            this.lbl_volume_up = new System.Windows.Forms.Label();
            this.lbl_pic = new System.Windows.Forms.Label();
            this.lbl_stop = new System.Windows.Forms.Label();
            this.lbl_previous = new System.Windows.Forms.Label();
            this.lbl_next = new System.Windows.Forms.Label();
            this.lbl_play = new System.Windows.Forms.Label();
            this.pan_1 = new System.Windows.Forms.Panel();
            this.lbl_cpu_temp_1 = new System.Windows.Forms.Label();
            this.lbl_cpu_temp_2 = new System.Windows.Forms.Label();
            this.lbl_cpu_temp_3 = new System.Windows.Forms.Label();
            this.lbl_cpu_temp_4 = new System.Windows.Forms.Label();
            this.lbl_cpu_temp_5 = new System.Windows.Forms.Label();
            this.lbl_cpu_temp_6 = new System.Windows.Forms.Label();
            this.lbl_net_up = new System.Windows.Forms.Label();
            this.lbl_net_down = new System.Windows.Forms.Label();
            this.pgb_gpu_mem_load = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_1 = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_2 = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_3 = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_4 = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_5 = new MediaDisplay.GaugeProgressBar();
            this.pgb_cpu_load_6 = new MediaDisplay.GaugeProgressBar();
            this.pgb_memory_load = new MediaDisplay.GaugeProgressBar();
            this.pgb_gpu_load = new MediaDisplay.GaugeProgressBar();
            this.lbl_gpu_temp = new System.Windows.Forms.Label();
            this.lbl_gpu = new System.Windows.Forms.Label();
            this.lbl_memory = new System.Windows.Forms.Label();
            this.lbl_panel_next = new System.Windows.Forms.Label();
            this.lbl_panel_previous = new System.Windows.Forms.Label();
            this.lbl_room_temp = new System.Windows.Forms.Label();
            this.lbl_time = new System.Windows.Forms.Label();
            this.pan_preview = new System.Windows.Forms.Panel();
            this.lbl_net = new System.Windows.Forms.Label();
            this.lbl_net_in = new System.Windows.Forms.Label();
            this.lbl_net_out = new System.Windows.Forms.Label();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).BeginInit();
            this.pan_2.SuspendLayout();
            this.pan_1.SuspendLayout();
            this.pan_preview.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(104, 48);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.showToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // drawTimer
            // 
            this.drawTimer.Enabled = true;
            this.drawTimer.Interval = 1000;
            this.drawTimer.Tick += new System.EventHandler(this.drawTimer_Tick);
            // 
            // lbl_preview
            // 
            this.lbl_preview.AutoSize = true;
            this.lbl_preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_preview.Location = new System.Drawing.Point(12, 103);
            this.lbl_preview.Name = "lbl_preview";
            this.lbl_preview.Size = new System.Drawing.Size(77, 24);
            this.lbl_preview.TabIndex = 2;
            this.lbl_preview.Text = "Preview";
            // 
            // trb_frames
            // 
            this.trb_frames.Location = new System.Drawing.Point(12, 36);
            this.trb_frames.Maximum = 60;
            this.trb_frames.Minimum = 1;
            this.trb_frames.Name = "trb_frames";
            this.trb_frames.Size = new System.Drawing.Size(169, 45);
            this.trb_frames.SmallChange = 5;
            this.trb_frames.TabIndex = 3;
            this.trb_frames.Value = 2;
            this.trb_frames.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // lbl_frames
            // 
            this.lbl_frames.AutoSize = true;
            this.lbl_frames.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_frames.Location = new System.Drawing.Point(8, 9);
            this.lbl_frames.Name = "lbl_frames";
            this.lbl_frames.Size = new System.Drawing.Size(74, 24);
            this.lbl_frames.TabIndex = 4;
            this.lbl_frames.Text = "Frames";
            // 
            // lbl_frames_set
            // 
            this.lbl_frames_set.AutoSize = true;
            this.lbl_frames_set.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_frames_set.Location = new System.Drawing.Point(187, 36);
            this.lbl_frames_set.Name = "lbl_frames_set";
            this.lbl_frames_set.Size = new System.Drawing.Size(20, 24);
            this.lbl_frames_set.TabIndex = 5;
            this.lbl_frames_set.Text = "2";
            // 
            // clockTimer
            // 
            this.clockTimer.Enabled = true;
            this.clockTimer.Interval = 1000;
            this.clockTimer.Tick += new System.EventHandler(this.clockTimer_Tick);
            // 
            // lbl_cpu
            // 
            this.lbl_cpu.AutoSize = true;
            this.lbl_cpu.BackColor = System.Drawing.Color.Transparent;
            this.lbl_cpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu.Location = new System.Drawing.Point(45, 25);
            this.lbl_cpu.Name = "lbl_cpu";
            this.lbl_cpu.Size = new System.Drawing.Size(56, 25);
            this.lbl_cpu.TabIndex = 12;
            this.lbl_cpu.Text = "CPU";
            // 
            // pan_2
            // 
            this.pan_2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.pan_2.Controls.Add(this.lbl_volume_down);
            this.pan_2.Controls.Add(this.lbl_volume_up);
            this.pan_2.Controls.Add(this.lbl_pic);
            this.pan_2.Controls.Add(this.lbl_stop);
            this.pan_2.Controls.Add(this.lbl_previous);
            this.pan_2.Controls.Add(this.lbl_next);
            this.pan_2.Controls.Add(this.lbl_play);
            this.pan_2.Location = new System.Drawing.Point(0, 62);
            this.pan_2.Name = "pan_2";
            this.pan_2.Size = new System.Drawing.Size(800, 417);
            this.pan_2.TabIndex = 21;
            this.pan_2.Visible = false;
            // 
            // lbl_volume_down
            // 
            this.lbl_volume_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_volume_down.ForeColor = System.Drawing.Color.White;
            this.lbl_volume_down.Image = global::MediaDisplay.Properties.Resources.play;
            this.lbl_volume_down.Location = new System.Drawing.Point(28, 282);
            this.lbl_volume_down.Name = "lbl_volume_down";
            this.lbl_volume_down.Size = new System.Drawing.Size(100, 100);
            this.lbl_volume_down.TabIndex = 17;
            this.lbl_volume_down.Click += new System.EventHandler(this.lbl_volume_down_Click);
            // 
            // lbl_volume_up
            // 
            this.lbl_volume_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_volume_up.ForeColor = System.Drawing.Color.White;
            this.lbl_volume_up.Image = global::MediaDisplay.Properties.Resources.play;
            this.lbl_volume_up.Location = new System.Drawing.Point(28, 159);
            this.lbl_volume_up.Name = "lbl_volume_up";
            this.lbl_volume_up.Size = new System.Drawing.Size(100, 100);
            this.lbl_volume_up.TabIndex = 16;
            this.lbl_volume_up.Click += new System.EventHandler(this.lbl_volume_up_Click);
            // 
            // lbl_pic
            // 
            this.lbl_pic.AutoSize = true;
            this.lbl_pic.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_pic.ForeColor = System.Drawing.Color.White;
            this.lbl_pic.Location = new System.Drawing.Point(365, 110);
            this.lbl_pic.Name = "lbl_pic";
            this.lbl_pic.Size = new System.Drawing.Size(53, 24);
            this.lbl_pic.TabIndex = 15;
            this.lbl_pic.Text = "Hallo";
            // 
            // lbl_stop
            // 
            this.lbl_stop.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_stop.ForeColor = System.Drawing.Color.White;
            this.lbl_stop.Image = global::MediaDisplay.Properties.Resources.stop;
            this.lbl_stop.Location = new System.Drawing.Point(350, 282);
            this.lbl_stop.Name = "lbl_stop";
            this.lbl_stop.Size = new System.Drawing.Size(100, 100);
            this.lbl_stop.TabIndex = 14;
            this.lbl_stop.Click += new System.EventHandler(this.lbl_stop_Click);
            // 
            // lbl_previous
            // 
            this.lbl_previous.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_previous.ForeColor = System.Drawing.Color.White;
            this.lbl_previous.Image = global::MediaDisplay.Properties.Resources.prev;
            this.lbl_previous.Location = new System.Drawing.Point(244, 220);
            this.lbl_previous.Name = "lbl_previous";
            this.lbl_previous.Size = new System.Drawing.Size(100, 100);
            this.lbl_previous.TabIndex = 13;
            this.lbl_previous.Click += new System.EventHandler(this.lbl_previous_Click);
            // 
            // lbl_next
            // 
            this.lbl_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_next.ForeColor = System.Drawing.Color.White;
            this.lbl_next.Image = global::MediaDisplay.Properties.Resources.next;
            this.lbl_next.Location = new System.Drawing.Point(454, 220);
            this.lbl_next.Name = "lbl_next";
            this.lbl_next.Size = new System.Drawing.Size(100, 100);
            this.lbl_next.TabIndex = 12;
            this.lbl_next.Click += new System.EventHandler(this.lbl_next_Click);
            // 
            // lbl_play
            // 
            this.lbl_play.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_play.ForeColor = System.Drawing.Color.White;
            this.lbl_play.Image = global::MediaDisplay.Properties.Resources.play;
            this.lbl_play.Location = new System.Drawing.Point(350, 159);
            this.lbl_play.Name = "lbl_play";
            this.lbl_play.Size = new System.Drawing.Size(100, 100);
            this.lbl_play.TabIndex = 11;
            this.lbl_play.Click += new System.EventHandler(this.lbl_play_Click);
            // 
            // pan_1
            // 
            this.pan_1.BackColor = System.Drawing.Color.Transparent;
            this.pan_1.Controls.Add(this.lbl_net_out);
            this.pan_1.Controls.Add(this.lbl_net_in);
            this.pan_1.Controls.Add(this.lbl_net);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_1);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_2);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_3);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_4);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_5);
            this.pan_1.Controls.Add(this.lbl_cpu_temp_6);
            this.pan_1.Controls.Add(this.lbl_net_up);
            this.pan_1.Controls.Add(this.lbl_net_down);
            this.pan_1.Controls.Add(this.pgb_gpu_mem_load);
            this.pan_1.Controls.Add(this.lbl_cpu);
            this.pan_1.Controls.Add(this.pgb_cpu_load_1);
            this.pan_1.Controls.Add(this.pgb_cpu_load_2);
            this.pan_1.Controls.Add(this.pgb_cpu_load_3);
            this.pan_1.Controls.Add(this.pgb_cpu_load_4);
            this.pan_1.Controls.Add(this.pgb_cpu_load_5);
            this.pan_1.Controls.Add(this.pgb_cpu_load_6);
            this.pan_1.Controls.Add(this.pgb_memory_load);
            this.pan_1.Controls.Add(this.pgb_gpu_load);
            this.pan_1.Controls.Add(this.lbl_gpu_temp);
            this.pan_1.Controls.Add(this.lbl_gpu);
            this.pan_1.Controls.Add(this.lbl_memory);
            this.pan_1.Location = new System.Drawing.Point(0, 3);
            this.pan_1.Name = "pan_1";
            this.pan_1.Size = new System.Drawing.Size(800, 476);
            this.pan_1.TabIndex = 2;
            // 
            // lbl_cpu_temp_1
            // 
            this.lbl_cpu_temp_1.AutoSize = true;
            this.lbl_cpu_temp_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_1.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_1.Location = new System.Drawing.Point(134, 164);
            this.lbl_cpu_temp_1.Name = "lbl_cpu_temp_1";
            this.lbl_cpu_temp_1.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_1.TabIndex = 6;
            this.lbl_cpu_temp_1.Text = "label1";
            // 
            // lbl_cpu_temp_2
            // 
            this.lbl_cpu_temp_2.AutoSize = true;
            this.lbl_cpu_temp_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_2.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_2.Location = new System.Drawing.Point(374, 164);
            this.lbl_cpu_temp_2.Name = "lbl_cpu_temp_2";
            this.lbl_cpu_temp_2.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_2.TabIndex = 7;
            this.lbl_cpu_temp_2.Text = "label2";
            // 
            // lbl_cpu_temp_3
            // 
            this.lbl_cpu_temp_3.AutoSize = true;
            this.lbl_cpu_temp_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_3.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_3.Location = new System.Drawing.Point(619, 164);
            this.lbl_cpu_temp_3.Name = "lbl_cpu_temp_3";
            this.lbl_cpu_temp_3.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_3.TabIndex = 8;
            this.lbl_cpu_temp_3.Text = "label3";
            // 
            // lbl_cpu_temp_4
            // 
            this.lbl_cpu_temp_4.AutoSize = true;
            this.lbl_cpu_temp_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_4.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_4.Location = new System.Drawing.Point(134, 331);
            this.lbl_cpu_temp_4.Name = "lbl_cpu_temp_4";
            this.lbl_cpu_temp_4.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_4.TabIndex = 9;
            this.lbl_cpu_temp_4.Text = "label4";
            // 
            // lbl_cpu_temp_5
            // 
            this.lbl_cpu_temp_5.AutoSize = true;
            this.lbl_cpu_temp_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_5.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_5.Location = new System.Drawing.Point(374, 331);
            this.lbl_cpu_temp_5.Name = "lbl_cpu_temp_5";
            this.lbl_cpu_temp_5.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_5.TabIndex = 10;
            this.lbl_cpu_temp_5.Text = "label5";
            // 
            // lbl_cpu_temp_6
            // 
            this.lbl_cpu_temp_6.AutoSize = true;
            this.lbl_cpu_temp_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_cpu_temp_6.ForeColor = System.Drawing.Color.White;
            this.lbl_cpu_temp_6.Location = new System.Drawing.Point(619, 331);
            this.lbl_cpu_temp_6.Name = "lbl_cpu_temp_6";
            this.lbl_cpu_temp_6.Size = new System.Drawing.Size(60, 24);
            this.lbl_cpu_temp_6.TabIndex = 11;
            this.lbl_cpu_temp_6.Text = "label6";
            // 
            // lbl_net_up
            // 
            this.lbl_net_up.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_net_up.ForeColor = System.Drawing.Color.White;
            this.lbl_net_up.Location = new System.Drawing.Point(430, 414);
            this.lbl_net_up.Name = "lbl_net_up";
            this.lbl_net_up.Size = new System.Drawing.Size(99, 24);
            this.lbl_net_up.TabIndex = 20;
            this.lbl_net_up.Text = "label11";
            this.lbl_net_up.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_net_down
            // 
            this.lbl_net_down.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_net_down.ForeColor = System.Drawing.Color.White;
            this.lbl_net_down.Location = new System.Drawing.Point(430, 390);
            this.lbl_net_down.Name = "lbl_net_down";
            this.lbl_net_down.Size = new System.Drawing.Size(99, 24);
            this.lbl_net_down.TabIndex = 19;
            this.lbl_net_down.Text = "label10";
            this.lbl_net_down.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // pgb_gpu_mem_load
            // 
            this.pgb_gpu_mem_load.BackColor = System.Drawing.Color.Transparent;
            this.pgb_gpu_mem_load.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_gpu_mem_load.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_gpu_mem_load.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_gpu_mem_load.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_gpu_mem_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_gpu_mem_load.ForeColor = System.Drawing.Color.White;
            this.pgb_gpu_mem_load.HighThreshold = 80;
            this.pgb_gpu_mem_load.LimitThreshold = 90;
            this.pgb_gpu_mem_load.Location = new System.Drawing.Point(95, 420);
            this.pgb_gpu_mem_load.Name = "pgb_gpu_mem_load";
            this.pgb_gpu_mem_load.Size = new System.Drawing.Size(174, 20);
            this.pgb_gpu_mem_load.Style = MediaDisplay.GaugeProgressBar.StyleType.Bar;
            this.pgb_gpu_mem_load.TabIndex = 17;
            this.pgb_gpu_mem_load.Text = "gaugeProgressBar9";
            this.pgb_gpu_mem_load.Value = 50;
            // 
            // pgb_cpu_load_1
            // 
            this.pgb_cpu_load_1.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_1.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_1.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_1.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_1.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_1.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_1.HighThreshold = 80;
            this.pgb_cpu_load_1.LimitThreshold = 90;
            this.pgb_cpu_load_1.Location = new System.Drawing.Point(95, 63);
            this.pgb_cpu_load_1.Name = "pgb_cpu_load_1";
            this.pgb_cpu_load_1.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_1.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_1.TabIndex = 0;
            this.pgb_cpu_load_1.Text = "gaugeProgressBar1";
            this.pgb_cpu_load_1.Value = 50;
            // 
            // pgb_cpu_load_2
            // 
            this.pgb_cpu_load_2.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_2.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_2.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_2.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_2.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_2.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_2.HighThreshold = 80;
            this.pgb_cpu_load_2.LimitThreshold = 90;
            this.pgb_cpu_load_2.Location = new System.Drawing.Point(580, 230);
            this.pgb_cpu_load_2.Name = "pgb_cpu_load_2";
            this.pgb_cpu_load_2.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_2.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_2.TabIndex = 1;
            this.pgb_cpu_load_2.Text = "gaugeProgressBar2";
            this.pgb_cpu_load_2.Value = 50;
            // 
            // pgb_cpu_load_3
            // 
            this.pgb_cpu_load_3.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_3.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_3.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_3.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_3.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_3.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_3.HighThreshold = 80;
            this.pgb_cpu_load_3.LimitThreshold = 90;
            this.pgb_cpu_load_3.Location = new System.Drawing.Point(335, 63);
            this.pgb_cpu_load_3.Name = "pgb_cpu_load_3";
            this.pgb_cpu_load_3.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_3.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_3.TabIndex = 2;
            this.pgb_cpu_load_3.Text = "gaugeProgressBar3";
            this.pgb_cpu_load_3.Value = 50;
            // 
            // pgb_cpu_load_4
            // 
            this.pgb_cpu_load_4.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_4.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_4.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_4.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_4.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_4.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_4.HighThreshold = 80;
            this.pgb_cpu_load_4.LimitThreshold = 90;
            this.pgb_cpu_load_4.Location = new System.Drawing.Point(580, 63);
            this.pgb_cpu_load_4.Name = "pgb_cpu_load_4";
            this.pgb_cpu_load_4.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_4.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_4.TabIndex = 3;
            this.pgb_cpu_load_4.Text = "gaugeProgressBar4";
            this.pgb_cpu_load_4.Value = 50;
            // 
            // pgb_cpu_load_5
            // 
            this.pgb_cpu_load_5.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_5.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_5.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_5.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_5.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_5.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_5.HighThreshold = 80;
            this.pgb_cpu_load_5.LimitThreshold = 90;
            this.pgb_cpu_load_5.Location = new System.Drawing.Point(95, 230);
            this.pgb_cpu_load_5.Name = "pgb_cpu_load_5";
            this.pgb_cpu_load_5.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_5.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_5.TabIndex = 4;
            this.pgb_cpu_load_5.Text = "gaugeProgressBar5";
            this.pgb_cpu_load_5.Value = 50;
            // 
            // pgb_cpu_load_6
            // 
            this.pgb_cpu_load_6.BackColor = System.Drawing.Color.Transparent;
            this.pgb_cpu_load_6.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_cpu_load_6.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_cpu_load_6.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_cpu_load_6.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_cpu_load_6.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_cpu_load_6.ForeColor = System.Drawing.Color.White;
            this.pgb_cpu_load_6.HighThreshold = 80;
            this.pgb_cpu_load_6.LimitThreshold = 90;
            this.pgb_cpu_load_6.Location = new System.Drawing.Point(335, 230);
            this.pgb_cpu_load_6.Name = "pgb_cpu_load_6";
            this.pgb_cpu_load_6.Size = new System.Drawing.Size(130, 130);
            this.pgb_cpu_load_6.Style = MediaDisplay.GaugeProgressBar.StyleType.Gauge;
            this.pgb_cpu_load_6.TabIndex = 5;
            this.pgb_cpu_load_6.Text = "gaugeProgressBar6";
            this.pgb_cpu_load_6.Value = 50;
            // 
            // pgb_memory_load
            // 
            this.pgb_memory_load.BackColor = System.Drawing.Color.Transparent;
            this.pgb_memory_load.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_memory_load.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_memory_load.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_memory_load.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_memory_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_memory_load.ForeColor = System.Drawing.Color.White;
            this.pgb_memory_load.HighThreshold = 80;
            this.pgb_memory_load.LimitThreshold = 90;
            this.pgb_memory_load.Location = new System.Drawing.Point(551, 407);
            this.pgb_memory_load.Name = "pgb_memory_load";
            this.pgb_memory_load.Size = new System.Drawing.Size(203, 34);
            this.pgb_memory_load.Style = MediaDisplay.GaugeProgressBar.StyleType.Bar;
            this.pgb_memory_load.TabIndex = 13;
            this.pgb_memory_load.Text = "gaugeProgressBar7";
            this.pgb_memory_load.Value = 50;
            // 
            // pgb_gpu_load
            // 
            this.pgb_gpu_load.BackColor = System.Drawing.Color.Transparent;
            this.pgb_gpu_load.ColorHigh = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(74)))));
            this.pgb_gpu_load.ColorLimit = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.pgb_gpu_load.ColorNormal = System.Drawing.Color.Cyan;
            this.pgb_gpu_load.ColorShade = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pgb_gpu_load.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.pgb_gpu_load.ForeColor = System.Drawing.Color.White;
            this.pgb_gpu_load.HighThreshold = 80;
            this.pgb_gpu_load.LimitThreshold = 90;
            this.pgb_gpu_load.Location = new System.Drawing.Point(95, 390);
            this.pgb_gpu_load.Name = "pgb_gpu_load";
            this.pgb_gpu_load.Size = new System.Drawing.Size(174, 20);
            this.pgb_gpu_load.Style = MediaDisplay.GaugeProgressBar.StyleType.Bar;
            this.pgb_gpu_load.TabIndex = 16;
            this.pgb_gpu_load.Text = "gaugeProgressBar8";
            this.pgb_gpu_load.Value = 50;
            // 
            // lbl_gpu_temp
            // 
            this.lbl_gpu_temp.AutoSize = true;
            this.lbl_gpu_temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_gpu_temp.ForeColor = System.Drawing.Color.White;
            this.lbl_gpu_temp.Location = new System.Drawing.Point(29, 417);
            this.lbl_gpu_temp.Name = "lbl_gpu_temp";
            this.lbl_gpu_temp.Size = new System.Drawing.Size(60, 24);
            this.lbl_gpu_temp.TabIndex = 18;
            this.lbl_gpu_temp.Text = "label9";
            // 
            // lbl_gpu
            // 
            this.lbl_gpu.AutoSize = true;
            this.lbl_gpu.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_gpu.ForeColor = System.Drawing.Color.White;
            this.lbl_gpu.Location = new System.Drawing.Point(32, 384);
            this.lbl_gpu.Name = "lbl_gpu";
            this.lbl_gpu.Size = new System.Drawing.Size(57, 25);
            this.lbl_gpu.TabIndex = 15;
            this.lbl_gpu.Text = "GPU";
            // 
            // lbl_memory
            // 
            this.lbl_memory.AutoSize = true;
            this.lbl_memory.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_memory.ForeColor = System.Drawing.Color.White;
            this.lbl_memory.Location = new System.Drawing.Point(546, 379);
            this.lbl_memory.Name = "lbl_memory";
            this.lbl_memory.Size = new System.Drawing.Size(89, 25);
            this.lbl_memory.TabIndex = 14;
            this.lbl_memory.Text = "Memory";
            // 
            // lbl_panel_next
            // 
            this.lbl_panel_next.BackColor = System.Drawing.Color.Transparent;
            this.lbl_panel_next.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_panel_next.ForeColor = System.Drawing.Color.White;
            this.lbl_panel_next.Location = new System.Drawing.Point(773, 115);
            this.lbl_panel_next.Name = "lbl_panel_next";
            this.lbl_panel_next.Size = new System.Drawing.Size(27, 240);
            this.lbl_panel_next.TabIndex = 1;
            this.lbl_panel_next.Click += new System.EventHandler(this.lbl_panel_next_Click);
            // 
            // lbl_panel_previous
            // 
            this.lbl_panel_previous.BackColor = System.Drawing.Color.Transparent;
            this.lbl_panel_previous.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_panel_previous.ForeColor = System.Drawing.Color.White;
            this.lbl_panel_previous.Location = new System.Drawing.Point(0, 115);
            this.lbl_panel_previous.Name = "lbl_panel_previous";
            this.lbl_panel_previous.Size = new System.Drawing.Size(29, 240);
            this.lbl_panel_previous.TabIndex = 0;
            this.lbl_panel_previous.Click += new System.EventHandler(this.lbl_panel_previous_Click);
            // 
            // lbl_room_temp
            // 
            this.lbl_room_temp.BackColor = System.Drawing.Color.Transparent;
            this.lbl_room_temp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_room_temp.ForeColor = System.Drawing.Color.White;
            this.lbl_room_temp.Location = new System.Drawing.Point(93, -3);
            this.lbl_room_temp.Name = "lbl_room_temp";
            this.lbl_room_temp.Size = new System.Drawing.Size(132, 31);
            this.lbl_room_temp.TabIndex = 22;
            this.lbl_room_temp.Text = "-- °C";
            this.lbl_room_temp.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lbl_time
            // 
            this.lbl_time.AutoSize = true;
            this.lbl_time.BackColor = System.Drawing.Color.Transparent;
            this.lbl_time.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_time.ForeColor = System.Drawing.Color.White;
            this.lbl_time.Location = new System.Drawing.Point(578, -3);
            this.lbl_time.Name = "lbl_time";
            this.lbl_time.Size = new System.Drawing.Size(128, 31);
            this.lbl_time.TabIndex = 2;
            this.lbl_time.Text = "00:00:00";
            // 
            // pan_preview
            // 
            this.pan_preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(65)))));
            this.pan_preview.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pan_preview.BackgroundImage")));
            this.pan_preview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pan_preview.Controls.Add(this.lbl_panel_previous);
            this.pan_preview.Controls.Add(this.lbl_panel_next);
            this.pan_preview.Controls.Add(this.lbl_time);
            this.pan_preview.Controls.Add(this.lbl_room_temp);
            this.pan_preview.Controls.Add(this.pan_1);
            this.pan_preview.Controls.Add(this.pan_2);
            this.pan_preview.Location = new System.Drawing.Point(12, 131);
            this.pan_preview.Name = "pan_preview";
            this.pan_preview.Size = new System.Drawing.Size(800, 480);
            this.pan_preview.TabIndex = 1;
            // 
            // lbl_net
            // 
            this.lbl_net.AutoSize = true;
            this.lbl_net.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_net.ForeColor = System.Drawing.Color.White;
            this.lbl_net.Location = new System.Drawing.Point(275, 384);
            this.lbl_net.Name = "lbl_net";
            this.lbl_net.Size = new System.Drawing.Size(90, 25);
            this.lbl_net.TabIndex = 21;
            this.lbl_net.Text = "Network";
            // 
            // lbl_net_in
            // 
            this.lbl_net_in.AutoSize = true;
            this.lbl_net_in.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_net_in.ForeColor = System.Drawing.Color.White;
            this.lbl_net_in.Location = new System.Drawing.Point(365, 390);
            this.lbl_net_in.Name = "lbl_net_in";
            this.lbl_net_in.Size = new System.Drawing.Size(59, 24);
            this.lbl_net_in.TabIndex = 22;
            this.lbl_net_in.Text = "Down";
            // 
            // lbl_net_out
            // 
            this.lbl_net_out.AutoSize = true;
            this.lbl_net_out.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_net_out.ForeColor = System.Drawing.Color.White;
            this.lbl_net_out.Location = new System.Drawing.Point(365, 414);
            this.lbl_net_out.Name = "lbl_net_out";
            this.lbl_net_out.Size = new System.Drawing.Size(34, 24);
            this.lbl_net_out.TabIndex = 23;
            this.lbl_net_out.Text = "Up";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(820, 621);
            this.Controls.Add(this.lbl_frames_set);
            this.Controls.Add(this.lbl_frames);
            this.Controls.Add(this.trb_frames);
            this.Controls.Add(this.lbl_preview);
            this.Controls.Add(this.pan_preview);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Media Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).EndInit();
            this.pan_2.ResumeLayout(false);
            this.pan_2.PerformLayout();
            this.pan_1.ResumeLayout(false);
            this.pan_1.PerformLayout();
            this.pan_preview.ResumeLayout(false);
            this.pan_preview.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.Timer drawTimer;
        private System.Windows.Forms.Label lbl_preview;
        private System.Windows.Forms.TrackBar trb_frames;
        private System.Windows.Forms.Label lbl_frames;
        private System.Windows.Forms.Label lbl_frames_set;
        private System.Windows.Forms.Timer clockTimer;
        private System.Windows.Forms.Label lbl_cpu;
        private System.Windows.Forms.Panel pan_2;
        private System.Windows.Forms.Label lbl_volume_down;
        private System.Windows.Forms.Label lbl_volume_up;
        private System.Windows.Forms.Label lbl_pic;
        private System.Windows.Forms.Label lbl_stop;
        private System.Windows.Forms.Label lbl_previous;
        private System.Windows.Forms.Label lbl_next;
        private System.Windows.Forms.Label lbl_play;
        private System.Windows.Forms.Panel pan_1;
        private System.Windows.Forms.Label lbl_room_temp;
        private System.Windows.Forms.Label lbl_cpu_temp_6;
        private System.Windows.Forms.Label lbl_time;
        private System.Windows.Forms.Label lbl_cpu_temp_5;
        private System.Windows.Forms.Label lbl_cpu_temp_4;
        private System.Windows.Forms.Label lbl_cpu_temp_2;
        private System.Windows.Forms.Label lbl_panel_next;
        private System.Windows.Forms.Label lbl_cpu_temp_3;
        private System.Windows.Forms.Label lbl_panel_previous;
        private System.Windows.Forms.Label lbl_net_up;
        private System.Windows.Forms.Label lbl_net_down;
        private System.Windows.Forms.Label lbl_cpu_temp_1;
        private GaugeProgressBar pgb_gpu_mem_load;
        private GaugeProgressBar pgb_cpu_load_1;
        private GaugeProgressBar pgb_cpu_load_2;
        private GaugeProgressBar pgb_cpu_load_3;
        private GaugeProgressBar pgb_cpu_load_4;
        private GaugeProgressBar pgb_cpu_load_5;
        private GaugeProgressBar pgb_cpu_load_6;
        private GaugeProgressBar pgb_memory_load;
        private GaugeProgressBar pgb_gpu_load;
        private System.Windows.Forms.Label lbl_gpu_temp;
        private System.Windows.Forms.Label lbl_gpu;
        private System.Windows.Forms.Label lbl_memory;
        private System.Windows.Forms.Panel pan_preview;
        private System.Windows.Forms.Label lbl_net_out;
        private System.Windows.Forms.Label lbl_net_in;
        private System.Windows.Forms.Label lbl_net;
    }
}

