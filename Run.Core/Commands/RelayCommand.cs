using System;
using System.Windows.Input;

namespace Run.Core
{
    /// <summary>
    /// Command used for executing an action.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;

        /// <summary>
        /// Creates a relay command with specific action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public RelayCommand(Action execute) => this.execute = obj => execute();

        /// <summary>
        /// Creates a relay command with specific action.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        public RelayCommand(Action<object> execute) => this.execute = execute;

        #region Implementation

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter) => execute?.Invoke(parameter); 

        #endregion
    }
}
