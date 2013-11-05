using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Syringe;
using System.Runtime.InteropServices;

namespace Tester
{
    class Program
    {
        [StructLayout(LayoutKind.Sequential)]
        struct MessageStruct
        {
            public int X;
            public int Y;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Trying to inject dll into TestGetCursorPosApp.exe");
            MessageStruct messageData = new MessageStruct() { X = 100, Y = 100 };
            using (Injector syringe = new Injector(Process.GetProcessesByName("TestGetCursorPosApp")[0]))
            {
                syringe.InjectLibrary("Stub.dll");
                syringe.CallExport("Stub.dll", "ApplyHook");
                syringe.CallExport("Stub.dll", "SetPoint", messageData);
                syringe.CallExport("Stub.dll", "RemoveHook");
            }
            Console.WriteLine("Stub.dll should be ejected");
            Console.ReadLine();
        }
    }
}
