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
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).BeginInit();
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
            this.showToolStripMenuItem.Click += new System.EventHandler(this.ShowToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // trb_frames
            // 
            this.trb_frames.Location = new System.Drawing.Point(12, 36);
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
            this.lbl_connection_status.Location = new System.Drawing.Point(328, 32);
            this.lbl_connection_status.Name = "lbl_connection_status";
            this.lbl_connection_status.Size = new System.Drawing.Size(172, 29);
            this.lbl_connection_status.TabIndex = 6;
            this.lbl_connection_status.Text = "Disconencted";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(526, 103);
            this.Controls.Add(this.lbl_connection_status);
            this.Controls.Add(this.lbl_frames_set);
            this.Controls.Add(this.lbl_frames);
            this.Controls.Add(this.trb_frames);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Media Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).EndInit();
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
    }
}

