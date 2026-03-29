# Clippy to Avalonia Porting Phases

This document outlines the strategy for porting the Clippy application from WinUI 3 to Avalonia UI for cross-platform support.

## Phase 1: Project Initialization & Setup
**Goal:** Establish the new project structure and ensure basic build capability. **(COMPLETED)**

1.  **Create Avalonia Project:**
    -   Add a new Avalonia Desktop project named `Clippy.Avalonia` to the solution.
    -   Target .NET 10.0.
2.  **Dependencies:**
    -   Add reference to `Clippy.Core` (shared logic).
    -   Configure DI (Dependency Injection) via `Microsoft.Extensions.DependencyInjection` using `MockChatService`, `MockKeyService`, and `MockSettingsService`.
3.  **Basic Window:**
    -   Create a main window that launches and displays a "Hello World" or basic placeholder to verify the setup.

## Phase 2: Core UI & MVVM Integration
**Goal:** Replicate the main chat interface using Avalonia controls. **(COMPLETED)**

1.  **Port ViewModels:**
    -   Reuse `Clippy.Core` ViewModels (`ClippyViewModel`, etc.).
    -   Ensure `RelayCommand` and property notification mechanisms work seamlessly with Avalonia (compatible via `CommunityToolkit.Mvvm`).
2.  **Main Window Layout:**
    -   Recreate `MainWindow.axaml` in Avalonia XAML.
    -   Implement `ItemsControl` for chat messages with `DataTemplate` for User/System/Clippy messages.
    -   Port the input area (TextBox, Send button).
    -   Implement `Clippy.png` toggle functionality bound to `IsClippyEnabled` to show/hide the chat UI.
3.  **Styles & Resources:**
    -   Port `CubeKit.UI` styles to native Avalonia Styles/Themes for maximum cross-platform compatibility.
    -   Migrate assets (images, icons) to Avalonia resources.
4.  **UI Polish & Deferred Features:**
    -   Integrate `Markdown.Avalonia` for chat message rendering.
    -   Implement chat auto-scrolling natively via `ItemsControl` and `StackPanel VerticalAlignment="Bottom"`.
    -   Refine "Shift+Enter" vs "Enter" in the chat input.

## Phase 3: Platform Specific Features
**Goal:** Implement OS-specific behaviors like global hotkeys and window transparency. **(COMPLETED)**

1.  **Window Management:**
    -   Implement "Always on Top" (Pinning) bound to `IsPinned`.
    -   Implement Transparent/Translucent backgrounds (`TransparencyLevelHint="Mica, AcrylicBlur, Blur, Transparent, None"`).
    -   Use `ShutdownMode.OnExplicitShutdown` to support running in the background.
2.  **Global Hotkeys:**
    -   Integrate `SharpHook` for a cross-platform global hotkey (`Win/Cmd+C` to toggle application visibility).
3.  **System Tray:**
    -   Implement system tray and background execution using compilation constants (`PHASE3_TRAY_ICON`).
    -   Provide handlers to open UI, view settings, or exit from the background tray.

## Phase 4: Settings & Polish
**Goal:** Complete the feature set and ensure a polished user experience.

1.  **Settings Window:**
    -   Port `SettingsWindow` functionality to `Clippy.Avalonia`.
    -   Implement a true `SettingsService` bound to the UI to configure backend parameters (e.g., API keys, theme preferences).
    -   Connect "Settings" buttons from `MainWindow` and the System Tray to open the new settings view.
2.  **UI Enhancements & Animations:**
    -   Implement native Avalonia animations for `ShimmerControl` and message incoming effects.
    -   Design cross-platform equivalents for Windows-specific aesthetics where necessary (e.g., `DropShadowPanel` via `BoxShadow`).

## Phase 5: Testing (Headless)
**Goal:** Establish automated end-to-end testing. **(COMPLETED)**

1.  **Test Project:**
    -   Created `Clippy.Avalonia.Tests` targeting .NET 10.0.
2.  **Headless Support:**
    -   Configure testing using `Avalonia.Headless.XUnit` for headless UI automation.
    -   Implemented UI verification by directly simulating command executions.
    -   Automated capturing screenshots to `docs/images/` for verifying UI logic without manual intervention.

## Phase 6: Release & CI/CD
**Goal:** Package the application for distribution.

1.  **Build Scripts:**
    -   Update build scripts to publish for Windows, macOS, and Linux (requires `/p:EnableWindowsTargeting=true`).
    -   Create self-contained single-file executables for seamless deployment.
2.  **CI Pipeline:**
    -   Integrate build and test steps (`dotnet test`) into the CI workflow (GitHub Actions).
    -   Configure CI to run X11/Xvfb on Linux for Avalonia headless testing using `SkiaSharp.NativeAssets.Linux`.
