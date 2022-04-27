using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Ru.Mail.AlexBasic.GUIPasswordManager.Infrastructure.WinApis
{
    public static class WinApi
    {
        [DllImport("User32.dll")]
        public static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);

        [DllImport("user32.dll")]
        static extern IntPtr AttachThreadInput(IntPtr idAttach,
                             IntPtr idAttachTo, bool fAttach);

        [DllImport("user32.dll")]
        static extern IntPtr GetFocus();

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);
        public enum GetWindow_Cmd : uint
        {
            GW_HWNDFIRST = 0,
            GW_HWNDLAST = 1,
            GW_HWNDNEXT = 2,
            GW_HWNDPREV = 3,
            GW_OWNER = 4,
            GW_CHILD = 5,
            GW_ENABLEDPOPUP = 6
        }
        [DllImport("user32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern IntPtr GetParent(IntPtr hWnd);



        public static void PasteToApplication(string appName)
        {
            var proc = Process.GetProcessesByName(appName).FirstOrDefault();
            if (proc != null)
            {
                var handle = proc.MainWindowHandle;
                SetForegroundWindow(handle);
                SendKeys.SendWait("^v");
            }
        }

        public static void PasteToWindow(IntPtr handle)
        {
            SetForegroundWindow(handle);
            SendKeys.SendWait("^v");
        }

        public static void SendToWindow(IntPtr handle, string value)
        {
            SetForegroundWindow(handle);
            SendKeys.SendWait(value);
        }

        //Вот это рабочий метод 100%
        public static IntPtr SetLastWindowForeground()
        {
            IntPtr lastWindowHandle = GetLastWindow();
            SetForegroundWindow(lastWindowHandle);

            return lastWindowHandle;
        }

        public static IntPtr GetLastWindow()
        {
            IntPtr lastWindowHandle = WinApi.GetWindow(Process.GetCurrentProcess().MainWindowHandle, (uint)WinApi.GetWindow_Cmd.GW_HWNDNEXT);
            while (true)
            {
                IntPtr temp = WinApi.GetParent(lastWindowHandle);
                if (temp.Equals(IntPtr.Zero)) break;
                lastWindowHandle = temp;
            }
            return lastWindowHandle;
        }

        public static string GetActiveWindowTitle()
        {
            IntPtr handle = GetForegroundWindow();
            int nChars = GetWindowTextLength(handle) + 1;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static string GetWindowTitle(IntPtr handle)
        {
            int nChars = GetWindowTextLength(handle) + 1;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        public static (IntPtr, string) GetActiveWindowTitleAndHandle()
        {
            IntPtr handle = GetForegroundWindow();
            int nChars = GetWindowTextLength(handle) + 1;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return (handle, Buff.ToString());
            }
            return (IntPtr.Zero, null);
        }

        //private static IntPtr FocusedControlInActiveWindow()
        //{
        //    IntPtr activeWindowHandle = GetForegroundWindow();

        //    IntPtr activeWindowThread =
        //      GetWindowThreadProcessId(activeWindowHandle, IntPtr.Zero);
        //    IntPtr thisWindowThread = GetWindowThreadProcessId(this.Handle, IntPtr.Zero);

        //    AttachThreadInput(activeWindowThread, thisWindowThread, true);
        //    IntPtr focusedControlHandle = GetFocus();
        //    AttachThreadInput(activeWindowThread, thisWindowThread, false);

        //    return focusedControlHandle;
        //}
    }
}
