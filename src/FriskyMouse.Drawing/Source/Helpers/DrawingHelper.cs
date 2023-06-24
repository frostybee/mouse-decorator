﻿using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace FriskyMouse.Drawing.Helpers
{
    public class DrawingHelper
    {        
        public static Bitmap CreateBitmap(int width, int height, Color inColor)
        {
            if (width > 0 && height > 0)
            {
                Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);                
                using (Graphics graphics = Graphics.FromImage(bmp))
                {
                    graphics.Clear(inColor);                    
                }
                return bmp;
            }
            return null;
        }

        /// <summary>
        /// Creates a bounding rectangle for a ripple drawing.
        /// </summary>
        /// <param name="rippleRadius"></param>
        /// <returns></returns>
        public static Rectangle CreateRectangle(int width, int height, int radius)
        {
            return new Rectangle(width / 2 - radius, height / 2 - radius, radius * 2, radius * 2);
        }
        internal static PointF[] CreateStarShape(int width, int radius)
        {
            double middleX = width / 2;
            double middleY = width / 2;
            double min = 0.05f;            
            middleX -= radius;
            middleY -= radius;
            //TODO: put this in a helper class. Needs to be adjustable.
            // Create an array of points.
            PointF[] points =
                     {
                 new Point(Convert.ToInt32(middleX + radius * (0.5 + min)), Convert.ToInt32(middleY + radius * (0.84 + min))),
                 new Point(Convert.ToInt32(middleX + radius * (1.5f + min)), Convert.ToInt32(middleY + radius * (0.84f + min))),
                 new Point(Convert.ToInt32(middleX + radius * (0.68f + min)), Convert.ToInt32(middleY + radius * (1.45f + min))),
                 new Point(Convert.ToInt32(middleX + radius * (1.0f + min)), Convert.ToInt32(middleY + radius * (0.5f + min))),
                 new Point(Convert.ToInt32(middleX + radius * (1.32f + min)), Convert.ToInt32(middleY + radius * (1.45f + min))),
                 new Point(Convert.ToInt32(middleX + radius * (0.5f + min)), Convert.ToInt32(middleY + radius * (0.84f + min))),
             };
            return points;
        }

        internal static PointF[] CreateHexagon(int x, int y, int radius)
        {
            return GetPolygonPoints(x, y, radius, 6, 180f);
        }

        internal static PointF[] CreateDiamond(int x, int y, int radius)
        {
            return GetPolygonPoints(x, y, radius, 8, 120f);
        }

        internal static PointF[] GetPolygonPoints(int x, int y, int radius, int edgesCount, float degree)
        {
            PointF[] points = new PointF[edgesCount];
            for (int i = 0; i < edgesCount; i++)
            {
                points[i] = new PointF(
                    x + radius * (float)Math.Cos(i * 60 * Math.PI / degree),
                    y + radius * (float)Math.Sin(i * 60 * Math.PI / degree));
            }
            return points;
        }
        public static void DrawShadow(Graphics graphics, GraphicsPath gp, int d, int penWidth, Color pBackColor)
        {
            Color[] colors = GetColorVector(Color.Gray, pBackColor, d).ToArray();
            for (int i = 0; i < d; i++)
            {
                graphics.TranslateTransform(0.9f, 0.15f);                // <== shadow vector!
                //graphics.TranslateTransform(0.9f, 0.10f);                // <== shadow vector!
                //using (Pen pen = new Pen(colors[i], 2.75f))  // <== pen width (*)
                using (Pen pen = new Pen(colors[i], penWidth))  // <== pen width (*)
                    graphics.DrawPath(pen, gp);
            }
            graphics.ResetTransform();
        }
        public static GraphicsPath CreateCircle(int x, int y, int radius)
        {
            GraphicsPath gp = new GraphicsPath();            
            gp.AddEllipse(CreateRectangle(x, y, radius));
            return gp;
        }

        private static List<Color> GetColorVector(Color fc, Color bc, int depth)
        {
            List<Color> cv = new List<Color>();
            float dRed = 1f * (bc.R - fc.R) / depth;
            float dGreen = 1f * (bc.G - fc.G) / depth;
            float dBlue = 1f * (bc.B - fc.B) / depth;
            for (int d = 1; d <= depth; d++)
                cv.Add(Color.FromArgb(60, (int)(fc.R + dRed * d),
                  (int)(fc.G + dGreen * d), (int)(fc.B + dBlue * d)));
            return cv;
        }

        public static Color RandomColor()
        {
            return Color.FromArgb(RandomFast.Next(255), RandomFast.Next(255), RandomFast.Next(255));
        }
    }
}
