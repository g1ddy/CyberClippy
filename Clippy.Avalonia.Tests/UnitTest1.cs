using Xunit;
using Clippy.Avalonia.Services.Mocks;

namespace Clippy.Avalonia.Tests
{
    public class BasicTests
    {
        [Fact]
        public async System.Threading.Tasks.Task MockChatService_Returns_Mock_Response()
        {
            // Arrange
            var service = new MockChatService();

            // Act
            var result = await service.SendChatAsync(new System.Collections.Generic.List<Clippy.Core.Interfaces.IMessage>());

            // Assert
            Assert.Equal("This is a mock response from Clippy.", result);
        }

        [Fact]
        public void MockSettingsService_Has_Default_Values()
        {
            // Arrange
            var service = new MockSettingsService();

            // Act
            var tokens = service.Tokens;

            // Assert
            Assert.Equal(1000, tokens);
        }
    }
}
