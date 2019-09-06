using System.Windows.Controls;

namespace Run.DesktopClient
{
    public class BasePage<VM> : Page where VM : Core.BaseViewModel, new()
    {
        public BasePage()
        {
            DataContext = Core.Core.Get<VM>() ?? new VM();
        }
    }
}
