using FluentAssertions;
using Microsoft.AspNetCore.SignalR.Client;
using System.Threading.Tasks;
using Xunit;


namespace AdminApp.IntegrationTests
{
    public class SignalRTests : IClassFixture<MockWebApplicationFactory<MockStartup>>
    {
        private readonly MockWebApplicationFactory<MockStartup> _factory;

        public SignalRTests(MockWebApplicationFactory<MockStartup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async ValueTask Test_Echo()
        {
            var client = _factory.CreateClient();

            var hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost/signalR", options => options.HttpMessageHandlerFactory = _ => _factory.Server.CreateHandler())
                .Build();

            string message = "This is an echo";
            string echo = string.Empty;

            hubConnection.On<string>("Echo", message => echo = message);

            await hubConnection.StartAsync();

            await hubConnection.InvokeAsync("Echo", message);

            echo.Should().Be(message);
        }
    }
}
