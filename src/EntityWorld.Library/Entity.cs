using System;
using System.Drawing;

namespace EntityWorld.Library
{
    public class Entity
    {
        public Entity(EntityState state)
        {
            State = state;
        }

        public void Cycle(WorldState worldState)
        {
            switch(State.Instructions[State.ProgramCounter])
            {
                case Instruction.MoveLeft:
                    MoveLeft(worldState.WorldBounds);
                    break;

                case Instruction.MoveUp:
                    MoveUp(worldState.WorldBounds);
                    break;

                case Instruction.MoveRight:
                    MoveRight(worldState.WorldBounds);
                    break;

                case Instruction.MoveDown:
                    MoveDown(worldState.WorldBounds);
                    break;

                case Instruction.SkipIfFoodNotLeft:

                    if (!worldState.FoodBounds.IsLeftOfPoint(State.CurrentPosition))
                    {
                        State.ProgramCounter++;
                    }

                    break;

                case Instruction.SkipIfFoodNotUp:

                    if (!worldState.FoodBounds.IsAbovePoint(State.CurrentPosition))
                    {
                        State.ProgramCounter++;
                    }

                    break;

                case Instruction.SkipIfFoodNotRight:

                    if (!worldState.FoodBounds.IsRightOfPoint(State.CurrentPosition))
                    {
                        State.ProgramCounter++;
                    }

                    break;

                case Instruction.SkipIfFoodNotDown:

                    if (!worldState.FoodBounds.IsBelowPoint(State.CurrentPosition))
                    {
                        State.ProgramCounter++;
                    }

                    break;

                default:
                    throw new NotSupportedException($"Didn't expect instruction '{State.Instructions[State.ProgramCounter]}'.");
            }

            State.ProgramCounter++;

            if (State.ProgramCounter >= State.Instructions.Length)
            {
                State.ProgramCounter = 0;
            }

            if (worldState.FoodBounds.Contains(State.CurrentPosition))
            {
                State.StomachContentsLevel++;
            }
            else
            {
                State.StomachContentsLevel--;
            }

            if (State.StomachContentsLevel < 0)
            {
                State.StomachContentsLevel = 0;
            }
        }

        public EntityState State { get; }

        private void Move(Size vector, Rectangle worldSize)
        {
            //Calculate the new position of the entity.
            var newPosition = State.CurrentPosition + vector;

            if (worldSize.Contains(newPosition))
            {
                State.CurrentPosition = newPosition;
                State.TotalLinearDistanceMoved++;
            }
        }

        private void MoveLeft(Rectangle worldSize)
        {
            Move(new Size(-1, 0), worldSize);
        }

        private void MoveUp(Rectangle worldSize)
        {
           Move(new Size(0, -1), worldSize);
        }

        private void MoveRight(Rectangle worldSize)
        {
            Move(new Size(1, 0), worldSize);
        }

        private void MoveDown(Rectangle worldSize)
        {
            Move(new Size(0, 1), worldSize);
        }
    }
}
