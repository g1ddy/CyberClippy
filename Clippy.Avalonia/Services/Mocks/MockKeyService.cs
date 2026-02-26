using Clippy.Core.Services;

namespace Clippy.Avalonia.Services.Mocks
{
    public class MockKeyService : IKeyService
    {
        public string GetKey() => "mock-key";
        public void SetKey(string key) { }
    }
}
