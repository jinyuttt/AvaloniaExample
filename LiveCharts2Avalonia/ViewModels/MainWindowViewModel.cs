using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;

namespace LiveCharts2Avalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public IEnumerable<ISeries> StateSeries { get; set; } = new ISeries[]
      {
             new PieSeries<int> { Values = new int[] { 2 }, Name = "正常" , InnerRadius=15, Fill=  new SolidColorPaint(SKColor.Parse("#008000"))},
             new PieSeries<int> { Values = new int[] { 4 }, Name = "轻伤" , InnerRadius=15, Fill=  new SolidColorPaint(SKColor.Parse("#D2C86B")) },
             new PieSeries<int> { Values = new int[] { 1 }, Name = "中伤" , InnerRadius=15, Fill=  new SolidColorPaint(SKColor.Parse("#03AEDE")) },
             new PieSeries<int> { Values = new int[] { 4 }, Name = "重伤" ,InnerRadius=15, Fill=  new SolidColorPaint(SKColor.Parse("#C13530")) },
             new PieSeries<int> { Values = new int[] { 3 }, Name = "死亡" ,InnerRadius=15, Fill=  new SolidColorPaint(SKColor.Parse("#696969")) }
      };

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
