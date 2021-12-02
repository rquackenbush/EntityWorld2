using EntityWorld.Library;
using System;
using System.Drawing;
using System.Linq;

namespace EntityWorld.ConsoleHost
{
    class Program
    {
        static void Main(string[] args)
        {
            var entityState = new EntityState
            {
                Id = Guid.NewGuid(),
                StomachContentsLevel = 1000,
                Instructions = new Instruction[]
                {
                    Instruction.SkipIfFoodNotLeft,
                    Instruction.MoveLeft,
                    Instruction.SkipIfFoodNotUp,
                    Instruction.MoveUp,
                    Instruction.SkipIfFoodNotRight,
                    Instruction.MoveRight,
                    Instruction.SkipIfFoodNotDown,
                    Instruction.MoveDown
                },
            };

            var worldState = new WorldState
            {
                Entities = new EntityState[]
                {
                    entityState
                },
                FoodLocation = new Rectangle(50, 50, 10, 10),
                WorldSize = new Rectangle(0, 0, 100, 100)
            };

            var world = new World(worldState);

            for(int cycleIndex = 0; cycleIndex < 2000; cycleIndex++)
            {
                world.Cycle();
            }

            var aliveEntities = worldState.Entities
                .Where(e => e.StomachContentsLevel > 0)
                .ToArray();

            Console.WriteLine($"{aliveEntities.Length} of {worldState.Entities.Length} are still alive.");

            foreach(var entity in worldState.Entities)
            {
                Console.WriteLine($"Entity {entity.Id} gen {entity.GenerationIndex}.");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine($"  Total distance traveled: {entity.TotalLinearDistanceMoved}");
                Console.WriteLine($"  Distance from start to end point: {entity.CalculateDistanceTraveled():0.0}");
                Console.WriteLine($"  Is in food? {worldState.FoodLocation.Contains(entity.CurrentPosition)}");
                Console.WriteLine($"  Instruction set:");

                for(int instructionIndex = 0; instructionIndex < entity.Instructions.Length; instructionIndex++)
                {
                    Console.WriteLine($"{instructionIndex:00} {entity.Instructions[instructionIndex]}");
                }

                Console.WriteLine();
            }
        }
    }
}
