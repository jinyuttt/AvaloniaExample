using Avalonia.Controls;
using Avalonia.Threading;
using System.Threading;
using GaugeAvalonia.ViewModels;
namespace GaugeAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            init();
        }
        private void init()
        {

            Thread thread = new Thread(p =>
            {
                int num = 0;
                while (true)
                {

                    Thread.Sleep(1000);
                    num++;
                    Dispatcher.UIThread.Post(() =>
                    {
                       this.gauge.Value = num;
                        MainWindowViewModel model = this.DataContext as MainWindowViewModel;
                        model.Angle = num;
                       

                    });

                }

            });
            thread.IsBackground = true;
            thread.Start();
        }
    }
}