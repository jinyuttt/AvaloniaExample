using Avalonia.Controls;
using ReactiveUI;
using System.Reactive;

namespace WindowAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private Window hostWindow;
        public ReactiveCommand<Unit, Unit> QuitProgramCommand { get; }

        public MainWindowViewModel(Window _hostWindow)
        {
            hostWindow = _hostWindow;

            QuitProgramCommand = ReactiveCommand.Create(() => { hostWindow.Close(); });
        }
    }
}
