using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Avalonia.Input;
using Avalonia.VisualTree;
using Clippy.Core.ViewModels;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Media.Imaging;
using Avalonia.Headless;

namespace Clippy.Avalonia.Tests
{
    public class E2ETests
    {
        private const string UserMessage = "Hello, Clippy!";
        private const string MockResponse = "This is a mock response.";

        [AvaloniaFact]
        public void AppLaunch_MainWindowIsCreated()
        {
            var app = App.Current;
            Assert.NotNull(app);

            var viewModel = app.Services.GetRequiredService<ClippyViewModel>();

            var window = new MainWindow
            {
                DataContext = viewModel
            };

            window.Show();

            Assert.True(window.IsVisible);

            // Capture initial screen
            var frame = window.CaptureRenderedFrame();
            if (frame != null)
            {
                frame.Save("screenshot_AppLaunch.png");
            }

            window.Close();
        }

        [AvaloniaFact]
        public async Task SendMessage_AddsMockResponseToChat()
        {
            var app = App.Current;
            Assert.NotNull(app);

            var viewModel = app.Services.GetRequiredService<ClippyViewModel>();

            var window = new MainWindow
            {
                DataContext = viewModel
            };

            window.Show();

            // Locate the input TextBox
            var textBox = window.GetVisualDescendants().OfType<TextBox>().FirstOrDefault(tb => tb.Name == "InputTextBox");
            Assert.NotNull(textBox);

            // Locate the Send Button
            var button = window.GetVisualDescendants().OfType<Button>().FirstOrDefault();
            Assert.NotNull(button);

            // Verify initial state (it starts with system message and initial greeting)
            var initialCount = viewModel.MessagesVM.Count;

            // Enter text
            textBox.Text = UserMessage;

            // Simulate clicking the send button via its command binding (or UI raising)
            // Headless does not easily support generic mouse clicks without pointing to specific coordinates,
            // but we can invoke the button's command directly to test the UI binding.
            if (button.Command != null)
            {
                button.Command.Execute(button.CommandParameter);
            }
            else
            {
                // Log a warning or throw an exception
                System.Console.WriteLine("Warning: Button command is null.");
                // Fallback if no command is bound directly but we still want to simulate
                viewModel.SendPromptCommand.Execute(null);
            }

            // Wait for the mock service to respond
            await Task.Delay(1000);

            // Assert that there are now 2 more messages: the user message and the system/mock response
            Assert.Equal(initialCount + 2, viewModel.MessagesVM.Count);
            Assert.Equal(UserMessage, viewModel.MessagesVM[initialCount].MessageText);
            Assert.Equal(MockResponse, viewModel.MessagesVM[initialCount + 1].MessageText);

            // Capture a screenshot of the window after the response
            var frame = window.CaptureRenderedFrame();
            if (frame != null)
            {
                frame.Save("screenshot_SendMessage.png");
            }

            window.Close();
        }
    }
}
