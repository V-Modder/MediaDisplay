using System;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using Windows.Media.Control;
using Windows.Storage.Streams;
using WindowsMediaController;
using Newtonsoft.Json;
using PlaybackInfoDao;

namespace PlaybackInfoService {
    class Program {
        private static NamedPipeServerStream server = new NamedPipeServerStream("PipesOfPiece");

        static void Main(string[] args) {
            server.WaitForConnection();
            //server.WriteTimeout = 3000;
            MediaManager.OnNewSource += MediaManager_OnNewSource;
            MediaManager.OnRemovedSource += MediaManager_OnRemovedSource;
            MediaManager.OnPlaybackStateChanged += MediaManager_OnPlaybackStateChanged;
            MediaManager.OnSongChanged += MediaManager_OnSongChanged;
            MediaManager.Start();
            while (server.IsConnected)
                Console.ReadLine();
        }

        private static void MediaManager_OnNewSource(MediaManager.MediaSession session) {
            WriteLineColor("-- New Source: " + session.ControlSession.SourceAppUserModelId, ConsoleColor.Green);
        }

        private static void MediaManager_OnRemovedSource(MediaManager.MediaSession session) {
            WriteLineColor("-- Removed Source: " + session.ControlSession.SourceAppUserModelId, ConsoleColor.Red);
        }

        private static void MediaManager_OnPlaybackStateChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionPlaybackInfo args) {
            WriteLineColor($"{sender.ControlSession.SourceAppUserModelId} is now {args.PlaybackStatus}", ConsoleColor.Magenta);
            int status = (int)args.PlaybackStatus;
            WrieObject(new PlaybackInfo() { Status = (PlaybackStatus)status, IsStatusOnly = true });
        }

        private static void MediaManager_OnSongChanged(MediaManager.MediaSession sender, GlobalSystemMediaTransportControlsSessionMediaProperties args) {
            WriteLineColor($"{sender.ControlSession.SourceAppUserModelId} is now playing {args.Title} {(string.IsNullOrEmpty(args.Artist) ? "" : $"by {args.Artist}")}", ConsoleColor.Cyan);
            string image = null; 
            if (args.Thumbnail != null) {
                Task<string> t = getImage(args.Thumbnail);
                t.Wait();
                image = t.Result;
            }
            WrieObject(new PlaybackInfo() { Title = args.Title, Artist = args.Artist, Status = PlaybackStatus.Playing, Image = image });
        }

        private static async Task<string> getImage(IRandomAccessStreamReference imageRef) {
            using (IRandomAccessStreamWithContentType sourceStream = await imageRef.OpenReadAsync()) {
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream()) {
                    int read;
                    while ((read = sourceStream.AsStream().Read(buffer, 0, buffer.Length)) > 0) {
                        ms.Write(buffer, 0, read);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        private static void WriteLineColor(object toprint, ConsoleColor color) {
            Console.ForegroundColor = color;
            string text = "[" + DateTime.Now.ToString("HH:mm:ss.fff") + "] " + toprint;
            Console.WriteLine(text);
        }

        private static void WrieObject(PlaybackInfo info) {
            string s = JsonConvert.SerializeObject(info, Formatting.None);
            StreamWriter writer = new StreamWriter(server);
            writer.WriteLine(s);
            writer.Flush();
        }
    }
}
