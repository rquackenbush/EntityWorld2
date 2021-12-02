using System.Drawing;

namespace EntityWorld.Library
{
    public static class RectangleExtensions
    {
        public static bool IsLeftOfPoint(this Rectangle rectangle, Point point)
        {
            if (rectangle.Contains(point))
                return false;

            return rectangle.Right < point.X;
        }

        public static bool IsAbovePoint(this Rectangle rectangle, Point point)
        {
            if (rectangle.Contains(point))
                return false;

            return rectangle.Bottom < point.Y;
        }

        public static bool IsRightOfPoint(this Rectangle rectangle, Point point)
        {
            if (rectangle.Contains(point))
                return false;

            return rectangle.Left > point.X;
        }

        public static bool IsBelowPoint(this Rectangle rectangle, Point point)
        {
            if (rectangle.Contains(point))
                return false;

            return rectangle.Top > point.Y;
        }
    }
}
