using Avalonia.Controls;
using ScottPlot;
using ScottPlot.Avalonia;

namespace ScottPlot_Avalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };

           AvaPlot avaPlot1 = this.Find<AvaPlot>("AvaPlot1");
            
            avaPlot1.Plot.Add.Signal(dataY);
          //  avaPlot1.Plot.SetAxisLimits(0, 30, 0, 30);
          
        }
    }
}