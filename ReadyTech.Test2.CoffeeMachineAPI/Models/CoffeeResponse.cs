namespace ReadyTech.Test2.CoffeeMachineAPI.Models
{
    public class CoffeeResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } = string.Empty;
        public string? Prepared { get; set; } = string.Empty;
    }
}
