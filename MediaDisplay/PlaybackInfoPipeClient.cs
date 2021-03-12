using System;
using PlaybackInfoDao;
using System.IO.Pipes;
using System.Threading;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace MediaDisplay {
    class PlaybackInfoPipeClient : IDisposable {
        private Thread pipeThread;
        private Process playbackServerProcess;
        private PlaybackInfo playbackInfo;
        private bool hasChanges;
        private NamedPipeClientStream client;

        public PlaybackInfoPipeClient() {
            pipeThread = new Thread(new ThreadStart(run));
            playbackInfo = new PlaybackInfo();
            string dir = getSubDirectory();
            playbackServerProcess = new Process();
            playbackServerProcess.StartInfo.FileName = dir + "\\PlaybackInfoService.exe";
            playbackServerProcess.StartInfo.CreateNoWindow = true;
            playbackServerProcess.StartInfo.ErrorDialog = false;
            playbackServerProcess.StartInfo.WorkingDirectory = dir;
            playbackServerProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            //playbackServerProcess.StartInfo.UseShellExecute = false;
            client = new NamedPipeClientStream("PipesOfPiece");
        }

        private string getSubDirectory() {
            string strExeFilePath = Assembly.GetExecutingAssembly().Location;
            string strWorkPath = Path.GetDirectoryName(strExeFilePath);
            return strWorkPath + "\\PlaybackInfoService";
        }

        public void Start() {
            if(pipeThread.IsAlive == false) {
                pipeThread.Start();
            }
            if (IsProcessRunning(playbackServerProcess) == false) {
                playbackServerProcess.Start();
            }
        }

        public void Stop() {
            if (pipeThread.IsAlive) {
                client.Close();
                pipeThread.Abort();
            }
            if(IsProcessRunning(playbackServerProcess)) {
                playbackServerProcess.Kill();
            }
        }

        private bool IsProcessRunning(Process process) {
            if (process == null)
                throw new ArgumentNullException("process");

            try {
                Process.GetProcessById(process.Id);
            }
            catch (Exception) {
                return false;
            }
            return true;
        }

        private void run() {
            StreamReader reader = null;
            try {
                while(!client.IsConnected) {
                    try {
                        client.Connect(100);
                    }
                    catch(Exception e) { }
                }
                reader = new StreamReader(client);
                while (true) {
                    string line = reader.ReadLine();
                    if (line != null && line.Length > 0) {
                        PlaybackInfo info = ConvertJson(line);
                        if (info.IsStatusOnly) {
                            SetPlaybackStatus(info.Status);
                        }
                        else {
                            SetPlaybackInfo(info);
                        }
                    }
                }
            }
            catch (ThreadAbortException) {
                if (IsProcessRunning(playbackServerProcess)) {
                    playbackServerProcess.Kill();
                }
                if(reader != null) {
                    reader.Close();
                }
                client.Close();
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetPlaybackInfo(PlaybackInfo info) {
            playbackInfo.Artist = info.Artist;
            playbackInfo.Image = info.Image;
            playbackInfo.Status = info.Status;
            playbackInfo.Title = info.Title;
            hasChanges = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void SetPlaybackStatus(PlaybackStatus status) {
            playbackInfo.Status = status;
            hasChanges = true;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public PlaybackInfo GetPlaybackInfo() {
            hasChanges = false;
            return playbackInfo;
        }

        public bool HasChanges { get { return hasChanges; } }

        private PlaybackInfo ConvertJson(string json) {
            return (PlaybackInfo)JsonConvert.DeserializeObject(json, typeof(PlaybackInfo));
        }

        public void Dispose() {
            Stop();
        }
    }
}
