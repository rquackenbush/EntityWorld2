using System;
using System.Drawing;

namespace EntityWorld.Library
{
    public class EntityState
    {
        public Guid Id { get; set; }

        public Point CurrentPosition { get; set; }

        public int StomachContentsLevel { get; set; }

        public bool IsAlive => StomachContentsLevel > 0;

        public Point StartingPosition { get; set; }

        public int TotalLinearDistanceMoved { get; set; }

        public Instruction[] Instructions { get; set; }

        public int ProgramCounter { get; set; }

        public int GenerationIndex { get; set; }
    }
}
