using System;
using System.IO.Packaging;
using System.Text.Json;

namespace EntityWorld.Library
{
    public class StateExporter : IDisposable
    {
        private readonly Package _package;

        public StateExporter(string path)
        {
            _package = Package.Open(path, System.IO.FileMode.Create);
        }

        public void AddCycle(int generationIndex, int cycleIndex, WorldState state)
        {
            var partUri = PackUriHelper.CreatePartUri(new Uri($"snapshots/gen{generationIndex:00000}_cycle{cycleIndex:000000}.json", UriKind.Relative));

            PackagePart part = _package.CreatePart(partUri, "application/json", CompressionOption.Maximum);

            using (var partStream = part.GetStream(System.IO.FileMode.Create))
            {
                var jsonUtf8 = JsonSerializer.SerializeToUtf8Bytes(state);

                partStream.Write(jsonUtf8);
            }

            _package.CreateRelationship(part.Uri, TargetMode.Internal, "snapshot");
        }

        public void Dispose()
        {
            ((IDisposable)_package).Dispose();
        }
    }
}
