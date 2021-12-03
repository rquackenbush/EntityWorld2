using System;
using System.Security.Cryptography;

namespace EntityWorld.Library
{
    public class InstructionGenerator : IDisposable
    {
        private readonly Random _random = new Random();

        public Instruction GetNext()
        {
            return (Instruction)_random.Next(0, 7);
        }

        public void Dispose()
        {
        }
    }
}
