using System.Drawing;
using Xunit;

namespace EntityWorld.Library.Tests
{
    public class GeometryTests
    {
        /* 
           000000000011111111112 
           012345678901234567890
         00
         01
         02
         03
         04
         05
         06 
         07
         08
         09
         10          **********    
         11          *        *
         12          *        *
         13          *        *
         14          *        *
         15          *        *
         16          *        *
         17          *        *
         18          *        *
         19          **********
         20          

        */

        private readonly Rectangle _rectangle = new Rectangle(10, 10, 10, 10);

        [Theory]
        [InlineData(0, 0)]
        [InlineData(0, 10)]
        [InlineData(10, 10)]
        [InlineData(5, 30)]
        public void IsRectangleToLeftOfPointNegative(int pointX, int pointY)
        {
            var result = _rectangle.IsLeftOfPoint(new Point(pointX, pointY));

            Assert.False(result);
        }

        [Theory]
        [InlineData(30, 0)]
        public void IsRectangleToLeftOfPointPositive(int pointX, int pointY)
        {
            var result = _rectangle.IsLeftOfPoint(new Point(pointX, pointY));

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 9)]
        public void IsRectangleAbovePointNegative(int pointX, int pointY)
        {
            var result = _rectangle.IsAbovePoint(new Point(pointX, pointY));

            Assert.False(result);
        }

        [Theory]
        [InlineData(0, 30)]
        public void IsRectangleAbovePointPositive(int pointX, int pointY)
        {
            var result = _rectangle.IsAbovePoint(new Point(pointX, pointY));

            Assert.True(result);
        }

        [Theory]
        [InlineData(0, 60)]
        public void IsRectangleBelowPointNegative(int pointX, int pointY)
        {
            var result = _rectangle.IsBelowPoint(new Point(pointX, pointY));

            Assert.False(result);
        }

        [Theory]
        [InlineData(0, 0)]
        public void IsRectangleBelowPointPositive(int pointX, int pointY)
        {
            var result = _rectangle.IsBelowPoint(new Point(pointX, pointY));

            Assert.True(result);
        }

        [Theory]
        [InlineData(50, 0)]
        public void IsRectangleRightOfPointNegative(int pointX, int pointY)
        {
            var result = _rectangle.IsRightOfPoint(new Point(pointX, pointY));

            Assert.False(result);
        }

        [Theory]
        [InlineData(0, 0)]
        public void IsRectangleRightOfPointPositive(int pointX, int pointY)
        {
            var result = _rectangle.IsRightOfPoint(new Point(pointX, pointY));

            Assert.True(result);
        }
    }
}
