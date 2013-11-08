using Syringe;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Winnster.Interop.LibHook;

namespace SendInputLib
{
    public class SendMessageService : ISendMessageService
    {
        [DllImport("kernel32.dll")]
        public static extern uint GetLastError();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll", EntryPoint = "PostMessageA")]
        private static extern int PostMessage(IntPtr hwnd, int wMsg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, uint wParam, uint lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out Rectangle lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct MessageStruct
        {
            public int X;
            public int Y;
        }

        public const int MIN_KEYSTROKE_DELAY = 35;

        private static uint MakeLParam(int LoWord, int HiWord)
        {
            return (uint)((HiWord << 16) | (LoWord & 0xffff));
        }

        public enum WMessages : int
        {
            WM_LBUTTONDOWN = 0x201, //Left mousebutton down
            WM_LBUTTONUP = 0x202, //Left mousebutton up
            WM_LBUTTONDBLCLK = 0x203, //Left mousebutton doubleclick
            WM_RBUTTONDOWN = 0x204, //Right mousebutton down
            WM_RBUTTONUP = 0x205,  //Right mousebutton up
            WM_RBUTTONDBLCLK = 0x206, //Right mousebutton doubleclick
            WM_KEYDOWN = 0x100, //Key down
            WM_KEYUP = 0x101,  //Key up
            WM_MOUSEMOVE = 0x0200, // Mouse move
            WM_SETCURSOR = 0x0020, // Set Cursor
            MK_LBUTTON = 0x0001
        }

        /// <summary>
        /// Gets or sets the process.
        /// </summary>
        /// <value>
        /// The process.
        /// </value>
        public Process Process { get; set; }

        /// <summary>
        /// Gets or sets the random number generator.
        /// </summary>
        /// <value>
        /// The random gen.
        /// </value>
        public Random RandomGen { get; set; }

        /// <summary>
        /// Gets or sets the window rectangle.
        /// </summary>
        /// <value>
        /// The window rectangle.
        /// </value>
        public Rectangle WinRectangle
        {
            get
            {
                var rectangle = new Rectangle();
                var ret = GetWindowRect(this.Process.MainWindowHandle, out rectangle);
                return rectangle;
            }
        }

        /// <summary>
        /// Gets or sets the injector.
        /// </summary>
        /// <value>
        /// The injector.
        /// </value>
        public Injector PInjector { get; set; }

        /// <summary>
        /// Maps the memory.
        /// </summary>
        /// <param name="process">The process.</param>
        /// <param name="valueConfigFile">The value configuration file.</param>
        /// <returns></returns>
        public SendMessageService(Process process)
        {
            this.Process = process;
            this.RandomGen = new Random();
            this.PInjector = new Injector(process);
        }

        /// <summary>
        /// Gets the screen coordinates.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns></returns>
        public MessageStruct GetScreenCoordinates(int x, int y)
        {
            if (x == -1 && y == -1)
            {
                return new MessageStruct() { X = -1, Y = -1 };
            }
            else
            {
                return new MessageStruct() { X = x + this.WinRectangle.Left, Y = y + this.WinRectangle.Top };
            }            
        }

        /// <summary>
        /// Sends the key stroke.
        /// </summary>
        /// <param name="k">The k.</param>
        /// <param name="delay">The delay.</param>
        public void SendKeyStroke(uint k, int delay)
        {
            var resultKeyDown = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_KEYDOWN, k, 0);
            Thread.Sleep(delay + this.RandomGen.Next(-5,5));
            var resultKeyUp = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_KEYUP, k, 0xC0000000);
        }

        /// <summary>
        /// Sends the key stroke.
        /// </summary>
        /// <param name="k">The k.</param>
        public void SendKeyStroke(uint k)
        {
            SendKeyStroke(k, MIN_KEYSTROKE_DELAY);
        }

        /// <summary>
        /// Sends the left click.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SendLeftClick(int x, int y)
        {
            var delay = MIN_KEYSTROKE_DELAY;
            this.PInjector.CallExport("Stub.dll", "SetPoint", GetScreenCoordinates(x,y));
            var resultMove = PostMessage(this.Process.MainWindowHandle, (int)0x000, (uint)0x000, MakeLParam(x, y));
            var resultMoveSM = SendMessage(this.Process.MainWindowHandle, (int)WMessages.WM_SETCURSOR, (uint)WMessages.WM_MOUSEMOVE, 0x000);
            Thread.Sleep(delay + this.RandomGen.Next(-5, 5));
            var resultKeyDown = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_LBUTTONDOWN, (uint)WMessages.MK_LBUTTON, MakeLParam(x, y));
            Thread.Sleep(delay + this.RandomGen.Next(-5, 5));
            var resultKeyUp = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_LBUTTONUP, (uint)0x000, MakeLParam(x, y));
        }

        /// <summary>
        /// Sends the mouse move.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SendMouseMove(int x, int y)
        {
            var delay = MIN_KEYSTROKE_DELAY;
            this.PInjector.CallExport("Stub.dll", "SetPoint", GetScreenCoordinates(x, y));
            var resultMovePM = PostMessage(this.Process.MainWindowHandle, (int)0x000, (uint)0x000, MakeLParam(x, y));
            var resultMoveSM = SendMessage(this.Process.MainWindowHandle, (int)WMessages.WM_SETCURSOR, (uint)WMessages.WM_MOUSEMOVE, 0x000);
            Thread.Sleep(delay + this.RandomGen.Next(-5, 5));
        }

        /// <summary>
        /// Sends the right click.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void SendRightClick(int x, int y)
        {
            var delay = MIN_KEYSTROKE_DELAY;
            var resultKeyDown = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_RBUTTONDOWN, (uint)0x0004, MakeLParam(x, y));
            Thread.Sleep(delay + this.RandomGen.Next(-5, 5));
            var resultKeyUp = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_RBUTTONUP, (uint)0x0002, MakeLParam(x, y));
        }

        /// <summary>
        /// Clears the cursor.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public void ClearCursor()
        {
            this.PInjector.CallExport("Stub.dll", "SetPoint", GetScreenCoordinates(-1, -1));
        }


        /// <summary>
        /// Sets the keyboard modifier.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void SetKeyboardModifier(uint k)
        {
            var resultKeyDown = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_KEYDOWN, k, 0);            
        }

        /// <summary>
        /// Unsets the keyboard modifier.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void UnsetKeyboardModifier(uint k)
        {
            var resultKeyUp = PostMessage(this.Process.MainWindowHandle, (int)WMessages.WM_KEYUP, k, 0xC0000000);
        }
    }
}
