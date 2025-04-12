namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
