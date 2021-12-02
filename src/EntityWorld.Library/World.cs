using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityWorld.Library
{
    public class World
    {
        private readonly IList<Entity> _entities;

        public World(WorldState state)
        {
            State = state;

            _entities = state.Entities
                .Select(e => new Entity(e))
                .ToList();
        }

        public void Cycle()
        {
            Parallel.ForEach(_entities, e =>
            {
                e.Cycle(State);
            });

            State.CycleIndex++;
        }

        public WorldState State { get; }
    }
}
