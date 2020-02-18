using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoGenius
{
    class Keyboard
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        private static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        private static extern IntPtr GetClipboardData(int uFormat);

        [DllImport("user32.dll")]
        private static extern IntPtr SetClipboardData(int uFormat, IntPtr hMem);

        [DllImport("user32.dll")]
        private static extern bool EmptyClipboard();

        // 模拟按键按下
        const int KEYEVENTF_KEYDOWN = 0x0000;
        // 模拟按键弹起
        const int KEYEVENTF_KEYUP   = 0x0002;

        public static bool Proccess(NameValueCollection querys, HTTPResponse response)
        {
            var action = querys.Get("action");
            switch (action)
            {
                case "press":
                    return Press(querys, response);
                case "down":
                    return Down(querys, response);
                case "up":
                    return Up(querys, response);
                case "get_clipboard":
                    return GetClipboard(querys, response);
                case "set_clipboard":
                    return SetClipboard(querys, response);
            }
            return false;
        }

        private static bool Press(NameValueCollection querys, HTTPResponse response)
        {
            var keycode = querys.Get("keycode");
            if (keycode == null)
            {
                return false;
            }
            var key = (Keys)int.Parse(keycode);
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[键盘]按键 {0}", key.ToString()));
            return true;
        }

        private static bool Down(NameValueCollection querys, HTTPResponse response)
        {
            var keycode = querys.Get("keycode");
            if (keycode == null)
            {
                return false;
            }
            var key = (Keys)int.Parse(keycode);
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[键盘]按下 {0}", key.ToString()));
            return true;
        }

        private static bool Up(NameValueCollection querys, HTTPResponse response)
        {
            var keycode = querys.Get("keycode");
            if (keycode == null)
            {
                return false;
            }
            var key = (Keys)int.Parse(keycode);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[键盘]弹起 {0}", key.ToString()));
            return true;
        }

        private static bool GetClipboard(NameValueCollection querys, HTTPResponse response)
        {
            string text = string.Empty;
            OpenClipboard(IntPtr.Zero);
            IntPtr ptr = GetClipboardData(13);
            if (ptr != IntPtr.Zero)
            {
                text = Marshal.PtrToStringUni(ptr);
            }
            CloseClipboard();
            response.DataResponse(string.Format("\"{0}\"", text));
            Log.Info(string.Format("[键盘]获取剪切板文本 {0}", text));
            return true;
        }

        private static bool SetClipboard(NameValueCollection querys, HTTPResponse response)
        {
            var text = querys.Get("text");
            if (text == null) 
            {
                return false;
            }
            OpenClipboard(IntPtr.Zero);
            EmptyClipboard();
            SetClipboardData(13, Marshal.StringToHGlobalUni(text));
            CloseClipboard();
            response.DataResponse(string.Format("\"{0}\"", text));
            Log.Info(string.Format("[键盘]设置剪切板文本 {0}", text));
            return true;
        }
    }
}
