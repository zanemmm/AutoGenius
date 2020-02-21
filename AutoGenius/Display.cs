using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using CvPoint = OpenCvSharp.CPlusPlus.Point;

namespace AutoGenius
{
    class Display
    {
        [DllImport("user32.dll")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out System.Windows.Rect lpRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        static readonly int SH = Screen.PrimaryScreen.Bounds.Height;
        static readonly int SW = Screen.PrimaryScreen.Bounds.Width;

        public static bool Proccess(NameValueCollection querys, HTTPResponse response)
        {
            var action = querys.Get("action");
            switch (action)
            {
                case "get_screen_size":
                    return GetScreenSize(querys, response);
                case "get_window_rect":
                    return GetWindowRect(querys, response);
                case "find_image_from_screen":
                    return FindImageFromScreen(querys, response);
                case "find_image_from_screen_rect":
                    return FindImageFromScreenRect(querys, response);
                case "find_image_from_window":
                    return FindImageFromWindow(querys, response);
                case "find_image_from_window_rect":
                    return FindImageFromWindowRect(querys, response);
                case "get_color_from_screen":
                    return GetColorFromScreen(querys, response);
                case "find_color_from_screen":
                    return FindColorFromScreen(querys, response);
                case "find_color_from_screen_rect":
                    return FindColorFromScreenRect(querys, response);
                case "get_window_handle_by_title":
                    return GetWindowHandleByTitle(querys, response);
            }
            return false;
        }

        private static bool GetScreenSize(NameValueCollection querys, HTTPResponse response)
        {
            response.DataResponse(string.Format("{{ \"w\": {0}, \"h\": {1} }}", SW, SH));
            Log.Info(string.Format("[屏幕]屏幕大小 宽:{0},高:{1}", SW, SH));
            return true;
        }

        private static bool GetWindowRect(NameValueCollection querys, HTTPResponse response)
        {
            var handle = querys.Get("handle");
            if (handle == null)
            {
                return false;
            }
            try
            {
                var sc = new ScreenCapture();
                var rc = sc.GetWindowRC(new IntPtr(int.Parse(handle)));
                response.DataResponse(string.Format("{{ \"w\": {0}, \"h\": {1}, \"l\": {2}, \"t\": {3} }}", rc.Width, rc.Height, rc.Left, rc.Top));
                Log.Info(string.Format("[屏幕]窗口大小位置 宽:{0},高:{1},左距:{2},顶距:{3}", rc.Width, rc.Height, rc.Left, rc.Top));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindImageFromScreen(NameValueCollection querys, HTTPResponse response)
        {
            var imagePath = querys.Get("image_path");
            if (imagePath == null)
            {
                return false;
            }
            try
            {
                Mat target = new Mat(imagePath, LoadMode.AnyColor);
                Mat screen = CaptureScreen();

                double[] res = FindImage(screen, target);
                int x      = (int)res[0];
                int y      = (int)res[1];
                double val = res[2];

                response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}, \"val\": {2} }}", x, y, val));
                Log.Info(string.Format("[屏幕]找图 {0} X:{1},Y:{2},VAL:{3:N}", imagePath, x, y, val));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindImageFromScreenRect(NameValueCollection querys, HTTPResponse response)
        {
            var imagePath = querys.Get("image_path");
            var a = querys.Get("a");
            var b = querys.Get("b");
            var c = querys.Get("c");
            var d = querys.Get("d");
            if (imagePath == null || a == null || b == null || c == null || d == null)
            {
                return false;
            }
            int x1 = int.Parse(a);
            int y1 = int.Parse(b);
            int x2 = int.Parse(c);
            int y2 = int.Parse(d);
            try
            {
                Mat target = new Mat(imagePath, LoadMode.AnyColor);
                Mat screen = CaptureScreen(x1, y1, x2, y2);

                double[] res = FindImage(screen, target);
                int x = (int)res[0] + x1;
                int y = (int)res[1] + y1;
                double val = res[2];

                response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}, \"val\": {2} }}", x, y, val));
                Log.Info(string.Format("[屏幕]区域({0},{1}),({2},{3})找图 {4} X:{5},Y:{6},VAL:{7:N}", a, b, c, d, imagePath, x, y, val));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindImageFromWindow(NameValueCollection querys, HTTPResponse response)
        {
            var handle = querys.Get("handle");
            var imagePath = querys.Get("image_path");
            if (handle == null || imagePath == null)
            {
                return false;
            }
            try
            {
                var sc = new ScreenCapture();
                var bitmap = sc.GetScreenshot(new IntPtr(int.Parse(handle)));
                if (bitmap == null)
                {
                    return false;
                }
                var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap).CvtColor(ColorConversion.RgbaToRgb);
                var target = new Mat(imagePath, LoadMode.AnyColor);
                double[] res = FindImage(mat, target);
                int x = (int)res[0] + sc.rc.Left;
                int y = (int)res[1] + sc.rc.Top;
                double val = res[2];
                if (x < 0 || y < 0)
                {
                    return false;
                }
                response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}, \"val\": {2} }}", x, y, val));
                Log.Info(string.Format("[屏幕]窗口{0}找图 {1} X:{2},Y:{3},VAL:{4:N}", handle, imagePath, x, y, val));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindImageFromWindowRect(NameValueCollection querys, HTTPResponse response)
        {
            var handle = querys.Get("handle");
            var imagePath = querys.Get("image_path");
            var a = querys.Get("a");
            var b = querys.Get("b");
            var c = querys.Get("c");
            var d = querys.Get("d");
            if (handle == null || imagePath == null || a == null || b == null || c == null || d == null)
            {
                return false;
            }
            int x1 = int.Parse(a);
            int y1 = int.Parse(b);
            int x2 = int.Parse(c);
            int y2 = int.Parse(d);

            try
            {
                var sc = new ScreenCapture();
                var bitmap = sc.GetScreenshot(new IntPtr(int.Parse(handle)));
                if (bitmap == null)
                {
                    return false;
                }
                var mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap).CvtColor(ColorConversion.RgbaToRgb);
                if (mat.Width < x2)
                {
                    x1 = mat.Width;
                }
                if (mat.Height < y2)
                {
                    y2 = mat.Height;
                }
                mat = mat.SubMat(y1, y2, x1, x2);
                var target = new Mat(imagePath, LoadMode.AnyColor);
                double[] res = FindImage(mat, target);
                int x = (int)res[0] + x1 + sc.rc.Left;
                int y = (int)res[1] + y1 + sc.rc.Top;
                double val = res[2];
                if (x < 0 || y < 0)
                {
                    return false;
                }
                response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}, \"val\": {2} }}", x, y, val));
                Log.Info(string.Format("[屏幕]窗口{0}区域({1},{2}),({3},{4})找图 {5} X:{6},Y:{7},VAL:{8:N}", handle, a, b, c, d, imagePath, x, y, val));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool GetWindowHandleByTitle(NameValueCollection querys, HTTPResponse response)
        {
            var title = querys.Get("title");
            if (title == null)
            {
                return false;
            }
            int handle = FindWindow(null, title).ToInt32();
            response.DataResponse(string.Format("\"{0}\"", handle));
            Log.Info(string.Format("[屏幕]取窗口句柄 标题:{0}, 句柄:{1}", title, handle));
            return true;
        }

        private static bool GetColorFromScreen(NameValueCollection querys, HTTPResponse response)
        {
            var x = querys.Get("x");
            var y = querys.Get("y");
            if (x == null || y == null)
            {
                return false;
            }
            try
            {
                Mat screen = CaptureScreen();
                Vec3b p = screen.Get<Vec3b>(int.Parse(y), int.Parse(y));
                int pColor = p.Item2 << 16 | p.Item1 << 8 | p.Item0;
                response.DataResponse(string.Format("\"#{0:X}\"", pColor));
                Log.Info(string.Format("[屏幕]取色 X:{0},Y:{1},COLOR:#{2:X}", x, y, pColor));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindColorFromScreen(NameValueCollection querys, HTTPResponse response)
        {
            var color = querys.Get("color");
            if (color == null)
            {
                return false;
            }
            try
            {
                var rgb = int.Parse(color.TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
                Mat screen = CaptureScreen();
                var point = FindColor(screen, rgb);
                if (point.X != -1 && point.Y != -1)
                {
                    response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}}}", point.X, point.Y));
                }
                else
                {
                    response.DataResponse("null");
                }
                Log.Info(string.Format("[屏幕]找色 {0}, X:{1},Y:{2}", color, point.X, point.Y));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool FindColorFromScreenRect(NameValueCollection querys, HTTPResponse response)
        {
            var color = querys.Get("color");
            var a = querys.Get("a");
            var b = querys.Get("b");
            var c = querys.Get("c");
            var d = querys.Get("d");
            if (color == null || a == null || b == null || c == null || d == null)
            {
                return false;
            }
            int x1 = int.Parse(a);
            int y1 = int.Parse(b);
            int x2 = int.Parse(c);
            int y2 = int.Parse(d);
            try
            {
                var rgb = int.Parse(color.TrimStart('#'), System.Globalization.NumberStyles.HexNumber);
                Mat screen = CaptureScreen(x1, y1, x2, y2);
                var point = FindColor(screen, rgb);
                if (point.X != -1 && point.Y != -1)
                {
                    point.X += x1;
                    point.Y += y1;
                    response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}}}", point.X, point.Y));
                }
                else
                {
                    response.DataResponse("null");
                }
                Log.Info(string.Format("[屏幕]区域({0},{1}),({2},{3})找色 {4}  X:{5}, Y:{6}", a, b, c, d, color, point.X, point.Y));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static double[] FindImage(Mat template, Mat target)
        {
            Mat result = new Mat();
            Cv2.MatchTemplate(template, target, result, MatchTemplateMethod.CCoeffNormed);
            Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out CvPoint minLoc, out CvPoint maxLoc);
            return new double[] { maxLoc.X, maxLoc.Y, maxVal };
        }

        private static CvPoint FindColor(Mat mat, int rgb)
        {
            var w = mat.Width;
            var h = mat.Height;
            for (int y = 0; y < h; ++y)
            {
                for (int x = 0; x < w; ++x)
                {
                    Vec3b p = mat.Get<Vec3b>(y, x);
                    int pColor = p.Item2 << 16 | p.Item1 << 8 | p.Item0;
                    if (pColor == rgb)
                    {
                        return new CvPoint(x, y);
                    }
                }
            }
            return new CvPoint(-1, -1);
        }

        private static Mat CaptureScreen(int a = -1, int b = -1, int c = -1, int d = -1)
        {
            Rectangle screenRect = new Rectangle(0, 0, SW, SH);
            Bitmap bitmap = new Bitmap(SW, SH);
            Graphics gh = Graphics.FromImage(bitmap);
            gh.CopyFromScreen(0, 0, 0, 0, screenRect.Size);
            Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap).CvtColor(ColorConversion.RgbaToRgb);
            if (a != -1 && b != -1 && c != -1 && d != -1)
            {
                if (mat.Width < c)
                {
                    c = mat.Width;
                }
                if (mat.Height < d)
                {
                    d = mat.Height;
                }
                return mat.SubMat(b, d, a, c);
            }
            return mat;
        }
    }

    public class ScreenCapture
    {
        [DllImport("user32.dll")]
        static extern int GetWindowRgn(IntPtr hWnd, IntPtr hRgn);

        //Region Flags - The return value specifies the type of the region that the function obtains. It can be one of the following values.
        const int ERROR = 0;
        const int NULLREGION = 1;
        const int SIMPLEREGION = 2;
        const int COMPLEXREGION = 3;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(HandleRef hWnd, out RECT lpRect);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(System.Drawing.Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom) { }

            public int X
            {
                get { return Left; }
                set { Right -= (Left - value); Left = value; }
            }

            public int Y
            {
                get { return Top; }
                set { Bottom -= (Top - value); Top = value; }
            }

            public int Height
            {
                get { return Bottom - Top; }
                set { Bottom = value + Top; }
            }

            public int Width
            {
                get { return Right - Left; }
                set { Right = value + Left; }
            }

            public System.Drawing.Point Location
            {
                get { return new System.Drawing.Point(Left, Top); }
                set { X = value.X; Y = value.Y; }
            }

            public System.Drawing.Size Size
            {
                get { return new System.Drawing.Size(Width, Height); }
                set { Width = value.Width; Height = value.Height; }
            }

            public static implicit operator System.Drawing.Rectangle(RECT r)
            {
                return new System.Drawing.Rectangle(r.Left, r.Top, r.Width, r.Height);
            }

            public static implicit operator RECT(System.Drawing.Rectangle r)
            {
                return new RECT(r);
            }

            public static bool operator ==(RECT r1, RECT r2)
            {
                return r1.Equals(r2);
            }

            public static bool operator !=(RECT r1, RECT r2)
            {
                return !r1.Equals(r2);
            }

            public bool Equals(RECT r)
            {
                return r.Left == Left && r.Top == Top && r.Right == Right && r.Bottom == Bottom;
            }

            public override bool Equals(object obj)
            {
                if (obj is RECT)
                    return Equals((RECT)obj);
                else if (obj is System.Drawing.Rectangle)
                    return Equals(new RECT((System.Drawing.Rectangle)obj));
                return false;
            }

            public override int GetHashCode()
            {
                return ((System.Drawing.Rectangle)this).GetHashCode();
            }

            public override string ToString()
            {
                return string.Format(System.Globalization.CultureInfo.CurrentCulture, "{{Left={0},Top={1},Right={2},Bottom={3}}}", Left, Top, Right, Bottom);
            }
        }

        public RECT rc;

        public RECT GetWindowRC(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return new RECT(0, 0, 0, 0);
            }
            GetWindowRect(new HandleRef(null, hwnd), out rc);
            return rc;
        }

        public Bitmap GetScreenshot(IntPtr hwnd)
        {
            if (hwnd == IntPtr.Zero)
            {
                return null;
            }

            GetWindowRect(new HandleRef(null, hwnd), out rc);

            Bitmap bmp = new Bitmap(rc.Right - rc.Left, rc.Bottom - rc.Top, PixelFormat.Format32bppArgb);
            Graphics gfxBmp = Graphics.FromImage(bmp);
            IntPtr hdcBitmap;
            try
            {
                hdcBitmap = gfxBmp.GetHdc();
            }
            catch
            {
                return null;
            }
            bool succeeded = PrintWindow(hwnd, hdcBitmap, 0);
            gfxBmp.ReleaseHdc(hdcBitmap);
            if (!succeeded)
            {
                gfxBmp.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(System.Drawing.Point.Empty, bmp.Size));
            }
            IntPtr hRgn = CreateRectRgn(0, 0, 0, 0);
            GetWindowRgn(hwnd, hRgn);
            Region region = Region.FromHrgn(hRgn);//err here once
            if (!region.IsEmpty(gfxBmp))
            {
                gfxBmp.ExcludeClip(region);
                gfxBmp.Clear(Color.Transparent);
            }
            gfxBmp.Dispose();
            return bmp;
        }

        public void WriteBitmapToFile(string filename, Bitmap bitmap)
        {
            bitmap.Save(filename, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
