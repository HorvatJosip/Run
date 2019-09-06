using System.Windows.Input;

namespace Run.Core
{
    public class MainWindowViewModel : BaseViewModel
    {
        public ICommand MinimizeCommand { get; }
        public ICommand MaximizeCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DragCommand { get; }

        public Page Page { get; set; }

        public MainWindowViewModel()
        {
            var windowService = Core.Get<IWindowService>();
            windowService.PageChanged += (sender, page) => Page = page;

            MinimizeCommand = new RelayCommand(() => windowService.Minimize());
            MaximizeCommand = new RelayCommand(() => windowService.MaximizeOrRestore());
            CloseCommand = new RelayCommand(() => windowService.Close());
            DragCommand = new RelayCommand(() => windowService.DragMove());
        }
    }
}
