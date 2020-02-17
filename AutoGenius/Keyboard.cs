using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace AutoGenius
{
    class Keyboard
    {
        [DllImport("user32.dll")]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

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
            }
            return false;
        }

        public static bool Press(NameValueCollection querys, HTTPResponse response)
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
            Log.Info(string.Format("[键盘] 按键 {0}", key.ToString()));
            return true;
        }

        public static bool Down(NameValueCollection querys, HTTPResponse response)
        {
            var keycode = querys.Get("keycode");
            if (keycode == null)
            {
                return false;
            }
            var key = (Keys)int.Parse(keycode);
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[键盘] 按下 {0}", key.ToString()));
            return true;
        }

        public static bool Up(NameValueCollection querys, HTTPResponse response)
        {
            var keycode = querys.Get("keycode");
            if (keycode == null)
            {
                return false;
            }
            var key = (Keys)int.Parse(keycode);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[键盘] 弹起 {0}", key.ToString()));
            return true;
        }
    }
}
