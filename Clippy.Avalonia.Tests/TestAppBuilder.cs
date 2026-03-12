using Avalonia;
using Avalonia.Headless;

[assembly: AvaloniaTestApplication(typeof(Clippy.Avalonia.Tests.TestAppBuilder))]

namespace Clippy.Avalonia.Tests
{
    public class TestAppBuilder
    {
        public static AppBuilder BuildAvaloniaApp() =>
            AppBuilder.Configure<App>()
                .UseSkia()
                .UseHeadless(new AvaloniaHeadlessPlatformOptions() { UseHeadlessDrawing = false });
    }
}
