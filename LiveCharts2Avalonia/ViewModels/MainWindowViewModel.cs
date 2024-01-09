using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;

namespace LiveCharts2Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {

        public ISeries[] Series { get; set; }
            = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = new double[] { 2, 1, 3, 5, 3, 4, 6 },
                    Fill = null
                }
            };


    }
}
