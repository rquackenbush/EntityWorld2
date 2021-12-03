using System;
using System.Drawing;
using System.Linq;

namespace EntityWorld.Library
{
    public class PointGenerator
    {
        private readonly Random _random = new Random();

        public Point GetNext(Rectangle bounds, params Rectangle[] prohibitedZones)
        {
            const int Limit = 5000;

            var x = _random.Next(bounds.Left, bounds.Right);
            var y = _random.Next(bounds.Top, bounds.Bottom);

            int counter = 0;

            while(prohibitedZones.Any(z => z.Contains(x, y)))
            {
                if (counter > Limit)
                    throw new InvalidOperationException($"Unable to find a suitable random point after {Limit} attempts.");

                x = _random.Next(bounds.Left, bounds.Right);
                y = _random.Next(bounds.Top, bounds.Bottom);

                counter++;
            }

            return new Point(x, y);
        }
    }
}
