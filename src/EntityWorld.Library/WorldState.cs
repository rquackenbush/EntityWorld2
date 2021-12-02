using System.Drawing;

namespace EntityWorld.Library
{
    public class WorldState
    {
        public int CycleIndex { get; set; }

        public Rectangle WorldSize { get; set; }

        public Rectangle FoodLocation { get; set; }

        public EntityState[] Entities { get; set; }
    }
}
