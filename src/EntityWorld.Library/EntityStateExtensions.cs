namespace EntityWorld.Library
{
    public static class EntityStateExtensions
    {
        public static double CalculateDistanceTraveled(this EntityState entityState)
        {
            return DistanceCalculator.CalculateDistance(entityState.StartingPosition.X, entityState.StartingPosition.Y, entityState.CurrentPosition.X, entityState.CurrentPosition.Y);
        }
    }
}
