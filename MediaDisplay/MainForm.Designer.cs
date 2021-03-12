using System;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.trb_frames = new System.Windows.Forms.TrackBar();
            this.lbl_frames = new System.Windows.Forms.Label();
            this.lbl_frames_set = new System.Windows.Forms.Label();
            this.lbl_connection_status = new System.Windows.Forms.Label();
            this.lbl_title = new System.Windows.Forms.Label();
            this.btn_close = new MediaDisplay.WindowButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.trb_display_brightness = new System.Windows.Forms.TrackBar();
            this.lbl_display_brightness_set = new System.Windows.Forms.Label();
            this.lbl_brightness = new System.Windows.Forms.Label();
            this.lbl_bandwidth = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_display_brightness)).BeginInit();
            this.SuspendLayout();
            // 
            // trb_frames
            // 
            this.trb_frames.Location = new System.Drawing.Point(12, 80);
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
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(190, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(162, 25);
            this.lbl_title.TabIndex = 7;
            this.lbl_title.Text = "Media-Display";
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
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // trb_display_brightness
            // 
            this.trb_display_brightness.Location = new System.Drawing.Point(16, 172);
            this.trb_display_brightness.Name = "trb_display_brightness";
            this.trb_display_brightness.Size = new System.Drawing.Size(169, 45);
            this.trb_display_brightness.TabIndex = 11;
            this.trb_display_brightness.Value = 10;
            this.trb_display_brightness.Scroll += new System.EventHandler(this.trb_display_brightness_Scroll);
            // 
            // lbl_display_brightness_set
            // 
            this.lbl_display_brightness_set.AutoSize = true;
            this.lbl_display_brightness_set.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_display_brightness_set.ForeColor = System.Drawing.Color.White;
            this.lbl_display_brightness_set.Location = new System.Drawing.Point(195, 172);
            this.lbl_display_brightness_set.Name = "lbl_display_brightness_set";
            this.lbl_display_brightness_set.Size = new System.Drawing.Size(40, 24);
            this.lbl_display_brightness_set.TabIndex = 12;
            this.lbl_display_brightness_set.Text = "100";
            // 
            // lbl_brightness
            // 
            this.lbl_brightness.AutoSize = true;
            this.lbl_brightness.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_brightness.ForeColor = System.Drawing.Color.White;
            this.lbl_brightness.Location = new System.Drawing.Point(12, 145);
            this.lbl_brightness.Name = "lbl_brightness";
            this.lbl_brightness.Size = new System.Drawing.Size(98, 24);
            this.lbl_brightness.TabIndex = 13;
            this.lbl_brightness.Text = "Brightness";
            // 
            // lbl_bandwidth
            // 
            this.lbl_bandwidth.AutoSize = true;
            this.lbl_bandwidth.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_bandwidth.ForeColor = System.Drawing.Color.White;
            this.lbl_bandwidth.Location = new System.Drawing.Point(333, 172);
            this.lbl_bandwidth.Name = "lbl_bandwidth";
            this.lbl_bandwidth.Size = new System.Drawing.Size(0, 24);
            this.lbl_bandwidth.TabIndex = 14;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(550, 232);
            this.Controls.Add(this.lbl_bandwidth);
            this.Controls.Add(this.lbl_brightness);
            this.Controls.Add(this.lbl_display_brightness_set);
            this.Controls.Add(this.trb_display_brightness);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.lbl_title);
            this.Controls.Add(this.lbl_connection_status);
            this.Controls.Add(this.lbl_frames_set);
            this.Controls.Add(this.lbl_frames);
            this.Controls.Add(this.trb_frames);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.Text = "Media Display";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.trb_frames)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_display_brightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void RefreshTimer_Tick(object sender, EventArgs e) {
            throw new NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.TrackBar trb_frames;
        private System.Windows.Forms.Label lbl_frames;
        private System.Windows.Forms.Label lbl_frames_set;
        private System.Windows.Forms.Label lbl_connection_status;
        private System.Windows.Forms.Label lbl_title;
        private WindowButton btn_close;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TrackBar trb_display_brightness;
        private System.Windows.Forms.Label lbl_display_brightness_set;
        private System.Windows.Forms.Label lbl_brightness;
        private System.Windows.Forms.Label lbl_bandwidth;
    }
}

