// Services/IDateTimeProvider.cs
namespace ReadyTech.Test2.CoffeeMachineAPI.Services
{
    public interface IDateTimeProvider
    {
        DateTimeOffset Now { get; }
    }
}
