using Avalonia.Controls;
using ReactiveUI;

namespace TestAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _output = "Waiting...";
        private object view = null;
        public object ContenView
        {
            get => view;
            set => this.RaiseAndSetIfChanged(ref view, value);
        }

        public string? Title
        {
            get => _output;
            set => this.RaiseAndSetIfChanged(ref _output, value);
        }
    }
}
