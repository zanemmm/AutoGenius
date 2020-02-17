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
                case "find_color_from_screen":
                    return FindColorFromScreen(querys, response);
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
                Mat result = new Mat();

                Cv2.MatchTemplate(screen, target, result, MatchTemplateMethod.CCoeffNormed);
                Cv2.MinMaxLoc(result, out double minVal, out double maxVal, out OpenCvSharp.CPlusPlus.Point minLoc, out OpenCvSharp.CPlusPlus.Point maxLoc);

                response.DataResponse(string.Format("{{ \"x\": {0}, \"y\": {1}, \"val\": {2} }}", maxLoc.X, maxLoc.Y, maxVal));
                Log.Info(string.Format("[屏幕] 找图 {0} X:{1}, Y:{2}, VAL:{3:N}", imagePath, maxLoc.X, maxLoc.Y, maxVal));
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
                Log.Info(string.Format("[屏幕] 找色 {0}, X:{1} Y:{2}", color, point.X, point.Y));
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static CvPoint FindColor(Mat mat, int rgb)
        {
            var w = mat.Width;
            var h = mat.Height;
            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
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

        private static Mat CaptureScreen()
        {
            Rectangle screenRect = new Rectangle(0, 0, SW, SH);
            Bitmap bitmap = new Bitmap(SW, SH);
            Graphics gh = Graphics.FromImage(bitmap);
            gh.CopyFromScreen(0, 0, 0, 0, screenRect.Size);
            Mat mat = OpenCvSharp.Extensions.BitmapConverter.ToMat(bitmap).CvtColor(ColorConversion.RgbaToRgb);
            return mat;
        }
    }
}
