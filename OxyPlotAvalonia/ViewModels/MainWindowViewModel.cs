using OxyPlot;
using OxyPlot.Series;
using System.Collections.Generic;

namespace OxyPlotAvalonia.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
#pragma warning disable CA1822 // Mark members as static
        public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
        public PlotModel Model { get; private set; }
        public MainWindowViewModel()
        {

         //   OxyPlot.Avalonia.PlotView plotView=new OxyPlot.Avalonia.PlotView();
         //   plotView.Model = Model;
            // Create the plot model
            var tmp = new PlotModel { Title = "Simple example", Subtitle = "using OxyPlot" };

           // OxyPlot.Series.Series series=new  
            // Create two line series (markers are hidden by default)
          
            var series1 = new LineSeries { Title = "Series 1", MarkerType = MarkerType.Circle };
            List<DataPoint> points = new List<OxyPlot.DataPoint>();
            points.Add(new DataPoint(0, 0));
            points.Add(new DataPoint(10, 18));
            points.Add(new DataPoint(20, 12));
            points.Add(new DataPoint(30, 8));
            points.Add(new DataPoint(40, 15));
            series1.ItemsSource = points;

            var series2 = new LineSeries { Title = "Series 2", MarkerType = MarkerType.Square };
            List<DataPoint> points1 = new List<OxyPlot.DataPoint>();
            points.Add(new DataPoint(0, 0));
            points.Add(new DataPoint(10, 18));
            points.Add(new DataPoint(20, 12));
            points.Add(new DataPoint(30, 8));
            points.Add(new DataPoint(40, 15));
            points1.Add(new DataPoint(0, 4));
            points1.Add(new DataPoint(10, 12));
            points1.Add(new DataPoint(20, 16));
            points1.Add(new DataPoint(30, 25));
            points1.Add(new DataPoint(40, 5));
         
            // Add the series to the plot model
            // tmp.Series.Add(series1);
            // tmp.Series.Add(series2);
            tmp.Series.Add(series1);
            this.Model = tmp;
        }
    }
}
