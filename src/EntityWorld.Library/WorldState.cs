using System.Drawing;

namespace EntityWorld.Library
{
    public class WorldState
    {
        public int CycleIndex { get; set; }

        public Rectangle WorldBounds { get; set; }

        public Rectangle FoodBounds { get; set; }

        public EntityState[] Entities { get; set; }
    }
}
