using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using HidLibrary;

namespace MediaDisplay {
    internal sealed class EntryAPI {
        [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern int EMyDetectDevice(long myhwnd);

        [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void EMyCloseDevice();

        [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void EMySetCurrentDev(int nCurDev);

        [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern double EMyReadTemp(bool flag);

        [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool EMyInitConfig(bool dOrc);
    }

    class Temper : IDisposable {
        private const int VID = 0x1130;
        private readonly int[] PID = { 0x660C };

        private IntPtr hwnd;
        private bool connected;

        public Temper(IntPtr handle) {
            hwnd = handle;
            connected = false;
            InitDevice();
        }

        ~Temper() {
            Dispose();
        }

        public void Dispose() {
            EntryAPI.EMyCloseDevice();
        }

        private void InitDevice() {
            if(DeviceConnected()) {
                int numberOfDevices = EntryAPI.EMyDetectDevice((long)hwnd);
                if(numberOfDevices > 0) {
                    EntryAPI.EMySetCurrentDev(0);
                    Thread.Sleep(100);
                    EntryAPI.EMyInitConfig(true);
                    connected = true;
                }

            }
            else {
                connected = false;
            }
        }

        private bool DeviceConnected() {
            foreach(HidDevice device in HidDevices.Enumerate(VID, PID)) {
                return true;
            }

            return true;
        }

        public double GetTemp() {
            if(!connected) {
                InitDevice();
            }

            double value = EntryAPI.EMyReadTemp(true);

            if (value == -100) {
                connected = false;
                throw new IOException("No sensor");
            }
            else if (value == 888) {
                connected = false;
                throw new IOException("Device error");
            }
            else if (value == 999) {
                connected = false;
                throw new IOException("Device error");
            }
            else {
                return Math.Round(value, 2);
            }
        }
    }
}
