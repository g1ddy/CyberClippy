# Clippy to Avalonia Porting Phases

This document outlines the strategy for porting the Clippy application from WinUI 3 to Avalonia UI for cross-platform support.

## Phase 1: Project Initialization & Setup
**Goal:** Establish the new project structure and ensure basic build capability.

1.  **Create Avalonia Project:**
    -   Add a new Avalonia Desktop project named `Clippy.Avalonia` to the solution.
    -   Target .NET 9.0.
2.  **Dependencies:**
    -   Add reference to `Clippy.Core` (shared logic).
    -   Install `Avalonia.CommunityToolkit` or similar libraries to match WinUI functionality where possible.
    -   Configure DI (Dependency Injection) similar to the existing app.
3.  **Basic Window:**
    -   Create a main window that launches and displays a "Hello World" or basic placeholder to verify the setup.

## Phase 2: Core UI & MVVM Integration
**Goal:** Replicate the main chat interface using Avalonia controls.

1.  **Port ViewModels:**
    -   Reuse `Clippy.Core` ViewModels (`ClippyViewModel`, etc.).
    -   Ensure `RelayCommand` and property notification mechanisms work seamlessly with Avalonia (should be compatible via CommunityToolkit.Mvvm).
2.  **Main Window Layout:**
    -   Recreate `MainWindow.xaml` in Avalonia XAML.
    -   Implement `ListView` for chat messages with `DataTemplateSelector` for User/System/Clippy messages.
    -   Port the input area (TextBox, Send button).
3.  **Styles & Resources:**
    -   Port `CubeKit.UI` styles to Avalonia Styles/Themes.
    -   Migrate assets (images, icons) to Avalonia resources.

## Phase 3: Platform Specific Features
**Goal:** Implement OS-specific behaviors like global hotkeys and window transparency.

1.  **Window Management:**
    -   Implement "Always on Top" (Pinning).
    -   Implement Transparent/Translucent backgrounds (Platform-specific hints in Avalonia).
    -   Implement "Click-through" behavior (might require native platform code or specific window flags).
2.  **Global Hotkeys:**
    -   Replace WinUI/Windows P/Invoke hotkeys with a cross-platform library (e.g., `SharpHook` or `HotAvalonia`) or implement platform-specific handlers.
    -   Target behavior: `Cmd+C` (macOS) / `Win+C` (Windows/Linux) to toggle visibility.
3.  **System Tray:**
    -   Implement Tray Icon using Avalonia's `TrayIcon` API.
    -   Add context menu for Tray (Show, Settings, Exit).

## Phase 4: Settings & Polish
**Goal:** Complete the feature set and ensure a polished user experience.

1.  **Settings Window:**
    -   Port `SettingsWindow.xaml`.
    -   Bind to `SettingsService`.
2.  **Animations:**
    -   Replicate entrance/exit animations for messages and the window itself.
3.  **Input Handling:**
    -   Ensure "Shift+Enter" vs "Enter" behavior in the chat box works correctly.

## Phase 5: Testing (Appium & Headless)
**Goal:** Establish automated end-to-end testing.

1.  **Test Project:**
    -   Create `Clippy.E2E.Tests` project.
    -   Add Appium WebDriver dependencies.
2.  **Headless Support:**
    -   Configure the Avalonia app to run in headless mode (using `Avalonia.Headless` or Xvfb on Linux for CI).
    -   Write tests to verify:
        -   App launch.
        -   Sending a message.
        -   Receiving a response (mocked backend).
        -   Settings persistence.

## Phase 6: Release & CI/CD
**Goal:** Package the application for distribution.

1.  **Build Scripts:**
    -   Update build scripts to publish for Windows, macOS, and Linux.
    -   Create self-contained single-file executables if desired.
2.  **CI Pipeline:**
    -   Integrate build and test steps into the CI workflow.
