﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace AutoGenius
{
    class Mouse
    {
        [DllImport("user32.dll")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point pt);

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        // 模拟鼠标中键滚动
        const int MOUSEEVENTF_WHEEL = 0x0800;
        //标示是否采用绝对坐标 
        const int MOUSEEVENTF_ABSOLUTE = 0x8000;
        // 鼠标按键
        const int MOUSEBTN_LEFT   = 0;
        const int MOUSEBTN_RIGHT  = 1;
        const int MOUSEBTN_MIDDLE = 2;

        static readonly int SH = Screen.PrimaryScreen.Bounds.Height;
        static readonly int SW = Screen.PrimaryScreen.Bounds.Width;

        public static bool Proccess(NameValueCollection querys, HTTPResponse response)
        {
            var action = querys.Get("action");
            switch (action)
            {
                case "position":
                    return Position(querys, response);
                case "move":
                    return Move(querys, response);
                case "click":
                    return Click(querys, response);
                case "dbclick":
                    return DoubleClick(querys, response);
                case "wheel":
                    return Wheel(querys, response);
            }
            return false;
        }

        private static bool Position(NameValueCollection querys, HTTPResponse response)
        {
            GetCursorPos(out Point p);
            response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1} }}", p.X, p.Y));
            Log.Info(string.Format("[鼠标]位置 x: {0}, y: {1}", p.X, p.Y));
            return true;
        }

        private static bool Move(NameValueCollection querys, HTTPResponse response)
        {
            var x = querys.Get("x");
            var y = querys.Get("y");
            if (x == null || y == null)
            {
                return false;
            }
            SetCursorPos(int.Parse(x), int.Parse(y));
            response.SuccessResponse();
            Log.Info(string.Format("[鼠标]移动 x: {0}, y: {1}", x, y));
            return true;
        }

        private static bool Click(NameValueCollection querys, HTTPResponse response)
        {
            var btn = querys.Get("btn");
            if (btn == null)
            {
                return false;
            }
            int flags;
            string btnText;
            switch (int.Parse(btn))
            {
                case MOUSEBTN_LEFT:
                    flags = MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
                    btnText = "左键";
                    break;
                case MOUSEBTN_RIGHT:
                    flags = MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
                    btnText = "右键";
                    break;
                case MOUSEBTN_MIDDLE:
                    flags = MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP;
                    btnText = "中键";
                    break;
                default:
                    return false;
            }
            mouse_event(flags, 0, 0, 0, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[鼠标]{0} 单击", btnText));
            return true;
        }

        private static bool DoubleClick(NameValueCollection querys, HTTPResponse response)
        {
            var btn = querys.Get("btn");
            if (btn == null)
            {
                return false;
            }
            int flags;
            string btnText;
            switch (int.Parse(btn))
            {
                case MOUSEBTN_LEFT:
                    flags = MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
                    btnText = "左键";
                    break;
                case MOUSEBTN_RIGHT:
                    flags = MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP;
                    btnText = "右键";
                    break;
                case MOUSEBTN_MIDDLE:
                    flags = MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP;
                    btnText = "中键";
                    break;
                default:
                    return false;
            }
            mouse_event(flags, 0, 0, 0, 0);
            System.Threading.Thread.Sleep(100);
            mouse_event(flags, 0, 0, 0, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[鼠标]{0} 双击", btnText));
            return true;
        }

        private static bool Wheel(NameValueCollection querys, HTTPResponse response)
        {
            var y = querys.Get("y");
            if (y == null)
            {
                return false;
            }
            var dy = -int.Parse(y);
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, dy, 0);
            response.SuccessResponse();
            Log.Info(string.Format("[鼠标]滚动 {0}", dy));
            return true;
        }
    }
}
