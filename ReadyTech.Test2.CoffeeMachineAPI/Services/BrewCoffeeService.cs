using ReadyTech.Test2.CoffeeMachineAPI.Constants;
using ReadyTech.Test2.CoffeeMachineAPI.Models;

namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public class BrewCoffeeService : IBrewCoffeeService
    {
        private readonly IBrewCoffeeCounter _callCounter;
        private readonly IDateTimeProvider _dateTimeProvider;

        public BrewCoffeeService(IBrewCoffeeCounter callCounter,
                                    IDateTimeProvider dateTimeProvider)
        {
            _callCounter = callCounter;
            _dateTimeProvider = dateTimeProvider;
        }

        public CoffeeResponse BrewCoffee()
        {
            // Use the injected date/time provider.
            DateTimeOffset now = _dateTimeProvider.Now;

            // Requirement #3: If today is April 1, return 418 (I'm a teapot) with an empty response.
            if (now.Month == 4 && now.Day == 1)
                return new CoffeeResponse { StatusCode = 418 };

            // Increment the call counter.
            int currentCall = _callCounter.IncrementAndGet();

            // Requirement #2: every fifth call returns a 503 Service Unavailable.
            if (currentCall % 5 == 0)
                return new CoffeeResponse { StatusCode = 503 };

            // Requirement #1: Return the normal response.
            var response = new CoffeeResponse
            {
                StatusCode = 200,
                Message = CoffeeMessages.HotCoffee,
                Prepared = $"{now:yyyy-MM-ddTHH:mm:ss}{now.ToString("zzz").Replace(":", "")}"
            };

            return response;
        }
    }
}