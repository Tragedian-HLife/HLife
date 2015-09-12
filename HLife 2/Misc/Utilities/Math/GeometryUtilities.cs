using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HLife_2
{
    public static class GeometryUtilities
    {
        public static Point Subtract(Point left, Point right)
        {
            return new Point(left.X - right.X, left.Y - right.Y);
        }

        public static double Distance(Point left, Point right)
        {
            return Math.Sqrt(Math.Pow((double)(left.X - right.X), 2) + Math.Pow((double)(left.Y - right.Y), 2));
        }
    }
}
