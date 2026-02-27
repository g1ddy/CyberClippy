# Phase 2 Part B: Deferred UI Polish & Features

This document outlines the UI features and styling details that were deferred during the initial Phase 2 implementation of the Avalonia port. The goal of Phase 2 Part A was functional parity and cross-platform compatibility using native Avalonia controls.

## Deferred Visuals

1.  **ShineUITextblock**: The original WinUI app uses a custom control `ShineUITextblock` which likely includes a shimmer loading effect or a specific markdown rendering style. The current implementation uses standard `TextBlock`.
    *   *Action*: Implement `ShineUITextblock` in Avalonia, potentially using `Avalonia.Labs` for Shimmer effects if available, or a custom shader/animation.

2.  **Message Triangles**: The chat bubbles in WinUI have little triangles pointing to the sender (`MessageTriangle` control).
    *   *Action*: Create a `MessageTriangle` control in Avalonia using `Path` geometry, similar to the WinUI version.

3.  **Drop Shadows**: The WinUI app heavily uses `DropShadowPanel` from the Toolkit.
    *   *Action*: Implement drop shadows using Avalonia's `BoxShadow` property on Borders, or `DropShadowEffect` where appropriate.

4.  **Entrance Animations**: `ListView` in WinUI has `EntranceThemeTransition`.
    *   *Action*: Add entrance animations to the `ItemsControl` or switch to a `ListBox` with custom item container styles that include animations.

5.  **Mica Material**: While `TransparencyLevelHint="Mica"` is set, true Mica background support depends on the OS and might need more specific handling or a fallback for non-Windows platforms.

## Deferred Features

1.  **Markdown Rendering**: The WinUI app uses `MarkdownTextBlock`. The current port displays raw text.
    *   *Action*: Integrate a Markdown library for Avalonia (e.g., `Markdown.Avalonia`) to render formatted text in messages.

2.  **Auto-Scroll**: The chat should automatically scroll to the bottom when a new message arrives.
    *   *Action*: Implement an auto-scroll behavior or attached property for the `ScrollViewer` or `ItemsControl`.

3.  **Keyboard Accelerators**: "Shift+Enter" vs "Enter" logic in the TextBox.
    *   *Action*: Refine the `TextBox` input handling to support Shift+Enter for new lines and Enter for sending, if not fully covered by standard behavior.
