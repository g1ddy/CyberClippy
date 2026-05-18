using Xunit;
using Clippy.Avalonia.Services.Mocks;
using Clippy.Core.Services;

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
        public void SettingsService_Has_Default_Values()
        {
            // Arrange
            var tempPath = System.IO.Path.GetTempFileName();
            var service = new SettingsService(tempPath);

            // Act
            var tokens = service.Tokens;

            // Assert
            Assert.Equal(100, tokens);

            // Cleanup
            System.IO.File.Delete(tempPath);
        }
    }
}
