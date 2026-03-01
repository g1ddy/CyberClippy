using Avalonia;
using Avalonia.Controls;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.LogicalTree;
using System;
using System.Collections.Generic;

namespace Clippy.Avalonia.Controls
{
    public class AutoScrollBehavior
    {
        public static readonly AttachedProperty<bool> AutoScrollProperty =
            AvaloniaProperty.RegisterAttached<AutoScrollBehavior, ScrollViewer, bool>("AutoScroll", false);

        public static bool GetAutoScroll(ScrollViewer element)
        {
            return element.GetValue(AutoScrollProperty);
        }

        public static void SetAutoScroll(ScrollViewer element, bool value)
        {
            element.SetValue(AutoScrollProperty, value);
        }

        static AutoScrollBehavior()
        {
            AutoScrollProperty.Changed.AddClassHandler<ScrollViewer>(HandleAutoScrollChanged);
        }

        private static readonly Dictionary<ScrollViewer, NotifyCollectionChangedEventHandler> _handlers = new();

        private static void HandleAutoScrollChanged(ScrollViewer scrollViewer, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool autoScroll && autoScroll)
            {
                scrollViewer.AttachedToLogicalTree += ScrollViewer_AttachedToLogicalTree;
                scrollViewer.DetachedFromLogicalTree += ScrollViewer_DetachedFromLogicalTree;

                if (scrollViewer.Parent != null)
                {
                    Attach(scrollViewer);
                }
            }
            else
            {
                scrollViewer.AttachedToLogicalTree -= ScrollViewer_AttachedToLogicalTree;
                scrollViewer.DetachedFromLogicalTree -= ScrollViewer_DetachedFromLogicalTree;
                Detach(scrollViewer);
            }
        }

        private static void ScrollViewer_AttachedToLogicalTree(object? sender, LogicalTreeAttachmentEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                Attach(scrollViewer);
            }
        }

        private static void ScrollViewer_DetachedFromLogicalTree(object? sender, LogicalTreeAttachmentEventArgs e)
        {
            if (sender is ScrollViewer scrollViewer)
            {
                Detach(scrollViewer);
            }
        }

        private static void Attach(ScrollViewer scrollViewer)
        {
            // Avoid double attach
            if (_handlers.ContainsKey(scrollViewer)) return;

            var itemsControl = scrollViewer.GetLogicalDescendants().OfType<ItemsControl>().FirstOrDefault();
            if (itemsControl?.Items is INotifyCollectionChanged incc)
            {
                NotifyCollectionChangedEventHandler handler = (s, e) =>
                {
                    if (e.Action == NotifyCollectionChangedAction.Add)
                    {
                        global::Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                        {
                            scrollViewer.ScrollToEnd();
                        });
                    }
                };

                incc.CollectionChanged += handler;
                _handlers[scrollViewer] = handler;
            }
        }

        private static void Detach(ScrollViewer scrollViewer)
        {
            if (_handlers.TryGetValue(scrollViewer, out var handler))
            {
                var itemsControl = scrollViewer.GetLogicalDescendants().OfType<ItemsControl>().FirstOrDefault();
                if (itemsControl?.Items is INotifyCollectionChanged incc)
                {
                    incc.CollectionChanged -= handler;
                }
                _handlers.Remove(scrollViewer);
            }
        }
    }
}
