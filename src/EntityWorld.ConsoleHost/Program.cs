using EntityWorld.Library;
using System;
using System.Drawing;
using System.IO;
using System.Linq;

namespace EntityWorld.ConsoleHost
{
    class Program
    {
        const int NumberOfEntities = 1;
        const int NumberOfInstructions = 8;
        private static readonly Size FoodSize = new Size(10, 10);
        private static readonly Size WorldSize = new Size(100, 100);
        const int CycleCount = 2000;
        //const int GenerationCount = 100;
        const int GenerationCount = 3;
        //const string RenderDirectory = @"c:\scratch\entityrender";
        const string ExportDirectory = @"c:\scratch\entityexport";


        static void Main(string[] args)
        {
            //if (Directory.Exists(RenderDirectory))
            //{
            //    Directory.Delete(RenderDirectory, true);
            //}

            //Directory.CreateDirectory(RenderDirectory);

            Directory.CreateDirectory(ExportDirectory);

            var now = DateTime.Now;

            var filename = $"{now:G}.hisx"
                .Replace(@"/", "_")
                .Replace(@":", "-");

            using (var exporter = new StateExporter(Path.Join(ExportDirectory, filename)))
            {

                //This will stay the same (for now)
                var worldBounds = new Rectangle(new Point(0, 0), WorldSize);
                var pointGenerator = new PointGenerator();
                var renderer = new WorldRenderer();

                //The first time, we won't have any surviging entities.
                EntityState[] survivingEntities = new EntityState[]
                {
                new EntityState
                {
                    Id = Guid.Empty,
                    Instructions = new Instruction[]
                    {
                        Instruction.SkipIfFoodNotLeft,
                        Instruction.MoveLeft,
                        Instruction.SkipIfFoodNotUp,
                        Instruction.MoveUp,
                        Instruction.SkipIfFoodNotRight,
                        Instruction.MoveRight,
                        Instruction.SkipIfFoodNotDown,
                        Instruction.MoveDown,
                    }
                }
                };

                //Iterate through the generations
                for (int generationIndex = 0; generationIndex < GenerationCount; generationIndex++)
                {
                    //Keep track of surviving entities.
                    foreach (var survivingEntity in survivingEntities)
                    {
                        survivingEntity.GenerationIndex++;
                    }

                    EntityState[] entityStates = new EntityState[NumberOfEntities];

                    //Copy the surviving entities
                    Array.Copy(survivingEntities, entityStates, survivingEntities.Length);

                    var foodBounds = new Rectangle(pointGenerator.GetNext(worldBounds), FoodSize);

                    using (var instructionGenerator = new InstructionGenerator())
                    {
                        for (int entityIndex = survivingEntities.Length; entityIndex < NumberOfEntities; entityIndex++)
                        {
                            entityStates[entityIndex] = new EntityState
                            {
                                Id = Guid.NewGuid(),

                                Instructions = instructionGenerator.GetNext(NumberOfInstructions),
                            };
                        }
                    }

                    //Set a new starting position for everyone
                    foreach (var entity in entityStates)
                    {
                        var startingPosition = pointGenerator.GetNext(worldBounds, foodBounds);

                        entity.StartingPosition = startingPosition;
                        entity.CurrentPosition = startingPosition;

                        entity.StomachContentsLevel = 1000;

                        entity.ProgramCounter = 0;
                    }

                    var worldState = new WorldState
                    {
                        Entities = entityStates,
                        FoodBounds = foodBounds,
                        WorldBounds = worldBounds
                    };

                    var world = new World(worldState);

                    for (int cycleIndex = 0; cycleIndex < CycleCount; cycleIndex++)
                    {
                        world.Cycle();

                        //renderer.Render(world.State, Path.Combine(RenderDirectory, $"Gen{generationIndex:000}_Cycle{cycleIndex:0000}.png"));

                        exporter.AddCycle(generationIndex, cycleIndex, worldState);
                    }

                    survivingEntities = worldState.Entities
                        .Where(e => e.StomachContentsLevel > 0)
                        .ToArray();

                    Console.WriteLine($"Generation {generationIndex}: {survivingEntities.Length} of {worldState.Entities.Length} survived.");
                }

                foreach (var entity in survivingEntities)
                {
                    Console.WriteLine($"Entity {entity.Id} gen {entity.GenerationIndex}.");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine($"  Total distance traveled: {entity.TotalLinearDistanceMoved}");
                    Console.WriteLine($"  Distance from start to end point: {entity.CalculateDistanceTraveled():0.0}");
                    Console.WriteLine($"  Instruction set:");

                    for (int instructionIndex = 0; instructionIndex < entity.Instructions.Length; instructionIndex++)
                    {
                        Console.WriteLine($"{instructionIndex:00} {entity.Instructions[instructionIndex]}");
                    }

                    Console.WriteLine();
                }
            }
        }
    }
}
