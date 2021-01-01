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
            this.trb_frames = new System.Windows.Forms.TrackBar();
            this.lbl_frames = new System.Windows.Forms.Label();
            this.lbl_frames_set = new System.Windows.Forms.Label();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.lbl_connection_status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_minimize = new MediaDisplay.WindowButton();
            this.btn_close = new MediaDisplay.WindowButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Media-Display";
            this.notifyIcon.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // showToolStripMenuItem
            // 
            this.showToolStripMenuItem.Name = "showToolStripMenuItem";
            this.showToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.showToolStripMenuItem.Text = "Show";
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // trb_frames
            // 
            this.trb_frames.Location = new System.Drawing.Point(16, 80);
            this.trb_frames.Minimum = 1;
            this.trb_frames.Name = "trb_frames";
            this.trb_frames.Size = new System.Drawing.Size(169, 45);
            this.trb_frames.SmallChange = 5;
            this.trb_frames.TabIndex = 3;
            this.trb_frames.Value = 2;
            this.trb_frames.Scroll += new System.EventHandler(this.TrackBar1_Scroll);
            // 
            // lbl_frames
            // 
            this.lbl_frames.AutoSize = true;
            this.lbl_frames.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_frames.ForeColor = System.Drawing.Color.White;
            this.lbl_frames.Location = new System.Drawing.Point(12, 53);
            this.lbl_frames.Name = "lbl_frames";
            this.lbl_frames.Size = new System.Drawing.Size(74, 24);
            this.lbl_frames.TabIndex = 4;
            this.lbl_frames.Text = "Frames";
            // 
            // lbl_frames_set
            // 
            this.lbl_frames_set.AutoSize = true;
            this.lbl_frames_set.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_frames_set.ForeColor = System.Drawing.Color.White;
            this.lbl_frames_set.Location = new System.Drawing.Point(191, 80);
            this.lbl_frames_set.Name = "lbl_frames_set";
            this.lbl_frames_set.Size = new System.Drawing.Size(20, 24);
            this.lbl_frames_set.TabIndex = 5;
            this.lbl_frames_set.Text = "2";
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // lbl_connection_status
            // 
            this.lbl_connection_status.AutoSize = true;
            this.lbl_connection_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_connection_status.ForeColor = System.Drawing.Color.Red;
            this.lbl_connection_status.Location = new System.Drawing.Point(332, 76);
            this.lbl_connection_status.Name = "lbl_connection_status";
            this.lbl_connection_status.Size = new System.Drawing.Size(172, 29);
            this.lbl_connection_status.TabIndex = 6;
            this.lbl_connection_status.Text = "Disconencted";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(190, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 25);
            this.label1.TabIndex = 7;
            this.label1.Text = "Media-Display";
            // 
            // btn_minimize
            // 
            this.btn_minimize.BackColor = System.Drawing.Color.Black;
            this.btn_minimize.ForeColor = System.Drawing.Color.Gray;
            this.btn_minimize.HighlightBackColor = System.Drawing.Color.Gray;
            this.btn_minimize.HighlightForeColor = System.Drawing.Color.White;
            this.btn_minimize.Location = new System.Drawing.Point(486, 0);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(32, 32);
            this.btn_minimize.TabIndex = 8;
            this.btn_minimize.Text = "windowButton1";
            this.btn_minimize.UseVisualStyleBackColor = false;
            this.btn_minimize.WindowButtonType = MediaDisplay.WindowButtonType.Minimize;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_close
            // 
            this.btn_close.BackColor = System.Drawing.Color.Black;
            this.btn_close.ForeColor = System.Drawing.Color.Gray;
            this.btn_close.HighlightBackColor = System.Drawing.Color.Red;
            this.btn_close.HighlightForeColor = System.Drawing.Color.White;
            this.btn_close.Location = new System.Drawing.Point(518, 0);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(32, 32);
            this.btn_close.TabIndex = 9;
            this.btn_close.Text = "windowButton2";
            this.btn_close.UseVisualStyleBackColor = false;
            this.btn_close.WindowButtonType = MediaDisplay.WindowButtonType.Close;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = global::MediaDisplay.Properties.Resources.icon175x175;
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(550, 130);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_minimize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_connection_status);
            this.Controls.Add(this.lbl_frames_set);
            this.Controls.Add(this.lbl_frames);
            this.Controls.Add(this.trb_frames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Media Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showToolStripMenuItem;
        private System.Windows.Forms.TrackBar trb_frames;
        private System.Windows.Forms.Label lbl_frames;
        private System.Windows.Forms.Label lbl_frames_set;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Label lbl_connection_status;
        private System.Windows.Forms.Label label1;
        private WindowButton btn_minimize;
        private WindowButton btn_close;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

