namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public class BrewCoffeeCounter : IBrewCoffeeCounter
    {
        private int _callCount = 0;

        public int IncrementAndGet()
        {
            // Thread-safe increment.
            return Interlocked.Increment(ref _callCount);
        }

        public void Reset()
        {
            Interlocked.Exchange(ref _callCount, 0);
        }
    }
}
