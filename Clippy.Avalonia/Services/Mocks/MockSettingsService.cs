using Clippy.Core.Services;

namespace Clippy.Avalonia.Services.Mocks
{
    public class MockSettingsService : ISettingsService
    {
        public bool AutoPin { get; set; } = false;
        public bool TrayClippy { get; set; } = false;
        public bool TranslucentBackground { get; set; } = false;
        public bool KeyboardEnabled { get; set; } = false;
        public int Tokens { get; set; } = 1000;
    }
}
