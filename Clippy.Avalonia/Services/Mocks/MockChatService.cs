using Clippy.Core.Classes;
using Clippy.Core.Interfaces;
using Clippy.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clippy.Avalonia.Services.Mocks
{
    public class MockChatService : IChatService
    {
        public Task<string> SendChatAsync(IEnumerable<IMessage> Messages)
        {
            return Task.FromResult("This is a mock response from Clippy.");
        }

        public async IAsyncEnumerable<string> StreamChatAsync(IEnumerable<IMessage> _, CancellationToken cancellationToken = default)
        {
            await Task.Delay(100, cancellationToken);
            yield return "This ";
            await Task.Delay(100, cancellationToken);
            yield return "is ";
            await Task.Delay(100, cancellationToken);
            yield return "a ";
            await Task.Delay(100, cancellationToken);
            yield return "mock ";
            await Task.Delay(100, cancellationToken);
            yield return "response.";
        }
    }
}
