using System;
using System.IO;
using System.Management;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using HidLibrary;
using System.Diagnostics;

namespace MediaDisplay {
    //internal sealed class EntryAPI {
    //    [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    //    public static extern int EMyDetectDevice(long myhwnd);

    //    [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    //    public static extern void EMyCloseDevice();

    //    [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    //    public static extern void EMySetCurrentDev(int nCurDev);

    //    [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    //    public static extern double EMyReadTemp(bool flag);

    //    [DllImport("HidFTDll.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    //    public static extern bool EMyInitConfig(bool dOrc);
    //}

    //class Temper : IDisposable {
    //    private const int VID = 0x1130;
    //    private readonly int[] PID = { 0x660C };

    //    private IntPtr hwnd;
    //    private bool connected;

    //    [STAThread]
    //    static void Main(string[] args) {
    //        Temper tmp = new Temper(IntPtr.Zero);
    //        Stopwatch sw = new Stopwatch();
    //        while (true) {
    //            sw.Start();
    //            double temp = tmp.GetTemp();
    //            sw.Stop();
    //            Console.WriteLine("Temp: " + temp + "°C " + sw.ElapsedMilliseconds + "ms");
    //            sw.Reset();
    //        }
    //    }

    //    public Temper(IntPtr handle) {
    //        hwnd = handle;
    //        connected = false;
    //        InitDevice();
    //    }

    //    ~Temper() {
    //        Dispose();
    //    }

    //    public void Dispose() {
    //        EntryAPI.EMyCloseDevice();
    //    }

    //    private void InitDevice() {
    //        if(DeviceConnected()) {
    //            //int numberOfDevices = EntryAPI.EMyDetectDevice((long)hwnd);
    //            //if(numberOfDevices > 0) {
    //            //    EntryAPI.EMySetCurrentDev(0);
    //            //    Thread.Sleep(100);
    //            //    EntryAPI.EMyInitConfig(true);
    //                connected = true;
    //            //}

    //        }
    //        else {
    //            connected = false;
    //        }
    //    }

    //    private bool DeviceConnected() {
    //        foreach(HidDevice device in HidDevices.Enumerate(VID, PID)) {
    //            return true;
    //        }

    //        return false;
    //    }

    //    public double GetTemp() {
    //        if(!connected) {
    //            InitDevice();
    //        }
    //        int i = EntryAPI.EMyDetectDevice((long)hwnd);
    //        EntryAPI.EMySetCurrentDev(0);
    //        //Thread.Sleep(100);
    //        //EntryAPI.EMyInitConfig(true);
    //        double value = EntryAPI.EMyReadTemp(true);

    //        if (value == -100) {
    //            connected = false;
    //            throw new IOException("No sensor");
    //        }
    //        else if (value == 888) {
    //            connected = false;
    //            throw new IOException("Device error");
    //        }
    //        else if (value == 999) {
    //            connected = false;
    //            throw new IOException("Device error");
    //        }
    //        else {
    //            return Math.Round(value, 2);
    //        }
    //    }
    //}
}
