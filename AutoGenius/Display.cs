using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;
using OpenCvSharp.CPlusPlus;
using CvPoint = OpenCvSharp.CPlusPlus.Point;

namespace AutoGenius
{
    class Display
    {
        static readonly int SH = Screen.PrimaryScreen.Bounds.Height;
        static readonly int SW = Screen.PrimaryScreen.Bounds.Width;

        public static bool Proccess(NameValueCollection querys, HTTPResponse response)
        {
            var action = querys.Get("action");
            switch (action)
            {
                case "find_image_from_screen":
                    return FindImageFromScreen(querys, response);
                case "find_image_from_screen_rect":
                    return FindImageFromScreenRect(querys, response);
                case "get_color_from_screen":
                    return GetColorFromScreen(querys, response);
                case "find_color_from_screen":
                    return FindColorFromScreen(querys, response);
                case "find_color_from_screen_rect":
                    return FindColorFromScreenRect(querys, response);
            }
            return false;
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

        private static bool GetColorFromScreen(NameValueCollection querys, HTTPResponse response)
        {
            var x = querys.Get("x");
            var y = querys.Get("y");
            if (x == null || y == null)
            {
                return false;
            }
            Mat screen = CaptureScreen();
            Vec3b p = screen.Get<Vec3b>(int.Parse(y), int.Parse(y));
            int pColor = p.Item2 << 16 | p.Item1 << 8 | p.Item0;
            response.DataResponse(string.Format("\"#{0:X}\"", pColor));
            Log.Info(string.Format("[屏幕]取色 X:{0},Y:{1},COLOR:#{2:X}", x, y, pColor));
            return true;
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
                return mat.SubMat(b, d, a, c);
            }
            return mat;
        }
    }
}
