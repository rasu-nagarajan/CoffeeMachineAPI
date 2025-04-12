namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public interface IBrewCoffeeCounter
    {
        // Increments and returns the current call count 
        int IncrementAndGet();
        // rest the call count 
        void Reset();
    }
}
