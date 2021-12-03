namespace EntityWorld.Library
{
    public static class InstructionGeneratorExtensions
    {
        public static Instruction[] GetNext(this InstructionGenerator instructionGenerator, int count)
        {
            var instructions = new Instruction[count];

            for(int index = 0; index < count; index++)
            {
                instructions[index] = instructionGenerator.GetNext();
            }

            return instructions;
        }
    }
}
