using ReactiveUI;

namespace GaugeAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private double _value;
        public double Angle
        {
            get { return _value; }
            set { this.RaiseAndSetIfChanged(ref _value, value); }
        }
    }
}
