using Avalonia;
using Avalonia.Controls;
using System.Collections.Specialized;
using System.Linq;
using Avalonia.LogicalTree;

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
            AutoScrollProperty.Changed.AddClassHandler<ScrollViewer>((x, e) => HandleAutoScrollChanged(x, e));
        }

        private static void HandleAutoScrollChanged(ScrollViewer scrollViewer, AvaloniaPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool autoScroll && autoScroll)
            {
                scrollViewer.AttachedToLogicalTree += (s, ev) => Attach(scrollViewer);
                // Check if Parent is not null as a simple proxy for being attached
                if (scrollViewer.Parent != null)
                {
                    Attach(scrollViewer);
                }
            }
            else
            {
                Detach(scrollViewer);
            }
        }

        private static void Attach(ScrollViewer scrollViewer)
        {
            var itemsControl = scrollViewer.GetLogicalDescendants().OfType<ItemsControl>().FirstOrDefault();
            if (itemsControl != null)
            {
                if (itemsControl.Items is INotifyCollectionChanged incc)
                {
                    incc.CollectionChanged += (s, e) =>
                    {
                        if (e.Action == NotifyCollectionChangedAction.Add)
                        {
                            global::Avalonia.Threading.Dispatcher.UIThread.Post(() =>
                            {
                                scrollViewer.ScrollToEnd();
                            });
                        }
                    };
                }
            }
        }

        private static void Detach(ScrollViewer scrollViewer)
        {
            // Implementation for detaching if necessary.
            // In a simple app, we might not strictly need to unhook if the scrollviewer lives forever.
        }
    }
}
