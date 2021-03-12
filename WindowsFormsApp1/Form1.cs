using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Control;
using Windows.Storage.Streams;
using WindowsMediaController;

namespace WindowsFormsApp1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            MediaManager.OnNewSource += MediaManager_OnNewSource;
            MediaManager.OnRemovedSource += MediaManager_OnRemovedSource;
            MediaManager.OnPlaybackStateChanged += MediaManager_OnPlaybackStateChanged;
            MediaManager.OnSongChanged += MediaManager_OnSongChanged;
            MediaManager.Start();
        }

        private void MediaManager_OnNewSource(MediaManager.MediaSession session) {
        }

        private void MediaManager_OnRemovedSource(MediaManager.MediaSession session) {
            //throw new NotImplementedException();
        }

        private void MediaManager_OnPlaybackStateChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionPlaybackInfo args) {
            Invoke(new MethodInvoker(
                   () => {
                        textBox1.Text = args.PlaybackStatus.ToString("g");
                   }
               ));
        }

        private void MediaManager_OnSongChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionMediaProperties args) {
            textBox2.Text = args.Title;
            textBox3.Text = args.Artist;
            if (args.Thumbnail != null) {
                //Task<Image> ti = GetImage(args.Thumbnail);
                //ti.Wait();
                label5.Image = GetImage(args.Thumbnail);
            }
        }

        private Image GetImage(IRandomAccessStreamReference imageRef) {
            var test = imageRef.OpenReadAsync();
            test.Cancel();
            while (test.Status != Windows.Foundation.AsyncStatus.Started) {
                Thread.Sleep(100);
            }
            IRandomAccessStreamWithContentType sourceStream = test.GetResults();
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream()) {
                int read;
                while ((read = sourceStream.AsStream().Read(buffer, 0, buffer.Length)) > 0) {
                    ms.Write(buffer, 0, read);
                }
                return Image.FromStream(ms);
            }
        }
    }
}
