using Run.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Run.DesktopClient
{
    /// <summary>
    /// Default implementation of the <see cref="IWindowService"/>.
    /// </summary>
    public class WindowService : IWindowService
    {
        private System.Windows.WindowState windowState;

        private List<Page> pageNavigator = new List<Page>();
        private List<Window> windowNavigator = new List<Window>();

        private List<System.Windows.Window> windows = new List<System.Windows.Window>();

        public event EventHandler<Page> PageChanged;
        public event EventHandler<Window> WindowChanged;

        public Page Page
        {
            get => pageNavigator.Last();
            set
            {
                var index = pageNavigator.IndexOf(value);

                if (index == -1)
                    pageNavigator.Add(value);
                else
                {
                    var removeStart = index + 1;

                    if (removeStart == pageNavigator.Count)
                        return;

                    pageNavigator.RemoveRange(removeStart, pageNavigator.Count - removeStart);
                }

                PageChanged?.Invoke(this, value);
            }
        }

        public Window Window
        {
            get => windowNavigator.Last();
            set
            {
                var index = windowNavigator.IndexOf(value);

                if (index == -1)
                {
                    windowNavigator.Add(value);

                    if (!Try.To(() =>
                     {
                         var windowType = Type.GetType($"{GetType().Namespace}.{value}");

                         windows.Add((System.Windows.Window)Activator.CreateInstance(windowType));
                     }))
                        return;
                }
                else
                {
                    var removeStart = index + 1;

                    if (removeStart == windowNavigator.Count)
                        return;

                    var removeCount = windowNavigator.Count - removeStart;

                    windowNavigator.RemoveRange(removeStart, removeCount);
                    windows.RemoveRange(removeStart, removeCount);
                }

                WindowChanged?.Invoke(this, value);
            }
        }

        public System.Windows.Window CurrentWindow => windows.Last();

        public void Close()
        {
            CurrentWindow.Close();
        }

        public void DragMove()
        {
            Try.To(CurrentWindow.DragMove);
        }

        public void MaximizeOrRestore()
        {
            var window = CurrentWindow;

            if (window.WindowState == System.Windows.WindowState.Normal)
            {
                windowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                windowState = System.Windows.WindowState.Normal;
            }

            window.WindowState = windowState;
        }

        public void Minimize()
            => CurrentWindow.WindowState = System.Windows.WindowState.Minimized;
    }
}
