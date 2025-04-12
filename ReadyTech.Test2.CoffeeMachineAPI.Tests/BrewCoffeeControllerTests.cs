using System;
using Xunit;
using ReadyTech.Test2.CoffeeMachineAPI.Services;
using ReadyTech.Test2.CoffeeMachineAPI.Constants;

namespace ReadyTech.Test2.CoffeeMachineAPI.Tests
{
    // Fake implementations for services.
    public class FakeDateTimeProvider : IDateTimeProvider
    {
        public DateTimeOffset Now { get; set; }
    }

    public class FakeCallCounterService : IBrewCoffeeCounter
    {
        private int _callCount;
        public int IncrementAndGet() => ++_callCount;
        public void Reset() => _callCount = 0;
    }

    public class BrewCoffeeServiceTests
    {
        [Fact]
        public void BrewCoffee_ReturnsTeapotOnAprilFirst()
        {
            // Arrange: Set the date to April 1.
            var fakeDateTime = new FakeDateTimeProvider
            {
                Now = new DateTimeOffset(new DateTime(2025, 4, 1, 12, 0, 0), TimeSpan.Zero)
            };
            var fakeCounter = new FakeCallCounterService();
            var service = new BrewCoffeeService(fakeCounter, fakeDateTime);
            var response = service.BrewCoffee();
            Assert.Equal(418, response.StatusCode);
        }

        [Fact]
        public void BrewCoffee_Returns503OnEveryFifthCall()
        {
            // Arrange: Use any date that isn’t April 1.
            var fakeDateTime = new FakeDateTimeProvider
            {
                Now = new DateTimeOffset(new DateTime(2025, 3, 15, 12, 0, 0), TimeSpan.Zero)
            };
            var fakeCounter = new FakeCallCounterService();
            var service = new BrewCoffeeService(fakeCounter, fakeDateTime);

            // Simulate 4 successful calls.
            for (int i = 0; i < 4; i++)
            {
                var resp = service.BrewCoffee();
                Assert.Equal(200, resp.StatusCode);
            }

            // Act: 5th call should result in a 503.
            var response = service.BrewCoffee();

            // Assert
            Assert.Equal(503, response.StatusCode);
        }

        [Fact]
        public void BrewCoffee_ReturnsOkResponseForNonSpecialDay()
        {
            // Arrange: Use an arbitrary non-special day.
            var fakeDateTime = new FakeDateTimeProvider
            {
                Now = new DateTimeOffset(new DateTime(2025, 3, 15, 12, 0, 0), TimeSpan.FromHours(1))
            };
            var fakeCounter = new FakeCallCounterService();
            var service = new BrewCoffeeService(fakeCounter, fakeDateTime);

            // Act: On the 1st call, expect a normal 200 OK response.
            var response = service.BrewCoffee();
            Assert.Equal(200, response.StatusCode);
            Assert.Equal(CoffeeMessages.HotCoffee, response.Message);
            string expectedPrepared = $"{fakeDateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss")}{fakeDateTime.Now.ToString("zzz").Replace(":", "")}";
            Assert.Equal(expectedPrepared, response.Prepared);
        }

        [Fact]
        public void BrewCoffee_Returns503OnMultiplesOf5()
        {
            // Arrange: Use a non-special day.
            var fakeDateTime = new FakeDateTimeProvider
            {
                Now = new DateTimeOffset(new DateTime(2025, 3, 15, 12, 0, 0), TimeSpan.Zero)
            };
            var fakeCounter = new FakeCallCounterService();
            var service = new BrewCoffeeService(fakeCounter, fakeDateTime);

            // Simulate 14 calls and check that the 5th, 10th calls return 503.
            for (int i = 1; i <= 14; i++)
            {
                var response = service.BrewCoffee();
                if (i % 5 == 0)
                    Assert.Equal(503, response.StatusCode);
                else
                    Assert.Equal(200, response.StatusCode);
            }
        }

        [Fact]
        public void BrewCoffee_ReturnsEmptyBodyOn503()
        {
            // Arrange
            var fakeDateTime = new FakeDateTimeProvider { Now = new DateTimeOffset(new DateTime(2025, 3, 15), TimeSpan.Zero) };
            var fakeCounter = new FakeCallCounterService();
            var service = new BrewCoffeeService(fakeCounter, fakeDateTime);

            // Simulate 5th call
            for (int i = 1; i < 5; i++) service.BrewCoffee();
            var result = service.BrewCoffee();

            Assert.Equal(503, result.StatusCode);
            Assert.True(string.IsNullOrWhiteSpace(result.Message));
            Assert.True(string.IsNullOrWhiteSpace(result.Prepared));
        }

        [Theory]
        [InlineData(2025, 1, 1)]
        [InlineData(2025, 3, 15)]
        [InlineData(2025, 12, 31)]
        public void BrewCoffee_AlwaysReturns200ExceptSpecialCases(int year, int month, int day)
        {
            // tests for various dates
            var date = new DateTimeOffset(new DateTime(year, month, day, 10, 0, 0), TimeSpan.Zero);
            var service = new BrewCoffeeService(new FakeCallCounterService(), new FakeDateTimeProvider { Now = date });

            var result = service.BrewCoffee();
            Assert.Equal(200, result.StatusCode);
        }

    }
}
