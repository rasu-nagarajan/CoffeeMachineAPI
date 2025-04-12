using ReadyTech.Test2.CoffeeMachineAPI.Models;

namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public interface IBrewCoffeeService
    {
        CoffeeResponse BrewCoffee();
    }
}