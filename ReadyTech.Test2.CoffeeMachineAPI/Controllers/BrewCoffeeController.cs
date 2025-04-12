using Microsoft.AspNetCore.Mvc;
using ReadyTech.Test2.CoffeeMachineAPI.Services;

namespace ReadyTech.Test2.CoffeeMachineAPI.Controllers
{
    [ApiController]
    [Route("brew-coffee")]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly IBrewCoffeeService _brewCoffeeService;

        public BrewCoffeeController(IBrewCoffeeService brewCoffeeService)
        {
            _brewCoffeeService = brewCoffeeService;
        }

        [HttpGet]
        public IActionResult BrewCoffee()
        {
            var result = _brewCoffeeService.BrewCoffee();

            // map the service response status to the appropriate HTTP result.
            return result.StatusCode switch
            {
                200 => Ok(new { message = result.Message, prepared = result.Prepared }),
                418 => StatusCode(418),
                503 => StatusCode(503),
                _ => StatusCode(500)
            };
        }
    }
}
