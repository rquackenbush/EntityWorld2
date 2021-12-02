using System.Drawing;

namespace EntityWorld.Library
{
    public static class PointExtensions
    {
        public static bool IsToLeftOfRectangle(this Point point, Rectangle rectangle)
        {
            if (rectangle.Contains(point))
                return false;

            return point.X < rectangle.Left;
        }

        public static bool IsAboveRectangle(this Point point, Rectangle rectangle)
        {
            if (rectangle.Contains(point))
                return false;

            return point.Y < rectangle.Top;
        }

        public static bool IsToRightOfRectangle(this Point point, Rectangle rectangle)
        {
            if (rectangle.Contains(point))
                return false;

            return point.X > rectangle.Right;
        }

        public static bool IsBelowRectangle(this Point point, Rectangle rectangle)
        {
            if (rectangle.Contains(point))
                return false;

            return point.Y > rectangle.Bottom;
        }
    }
}
