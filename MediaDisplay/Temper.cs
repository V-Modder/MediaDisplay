using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

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

        public Temper(IntPtr handle) {
            int numberOfDevices = EntryAPI.EMyDetectDevice((long)handle);
            EntryAPI.EMySetCurrentDev(0);
            Thread.Sleep(100);
            EntryAPI.EMyInitConfig(true);
        }

        ~Temper() {
            Dispose();
        }

        public void Dispose() {
            EntryAPI.EMyCloseDevice();
        }

        public double getTemp() {
            double value = EntryAPI.EMyReadTemp(true);
            //double value2 = EntryAPI.EMyReadTemp(true);
            if (value == -100) {
                throw new IOException("No sensor");
            }
            else if (value == 888) {
                throw new IOException("Device error");
            }
            else if (value == 999) {
                throw new IOException("Device error");
            }
            else {
                return Math.Round(value, 2);
            }
        }
    }
}
