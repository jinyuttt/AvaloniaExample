using Avalonia.Controls;
using TestAvalonia.ViewModels;
namespace TestAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            //   UserControl userControl = null;
            MainWindowViewModel model = this.DataContext as MainWindowViewModel;
            if (model != null)
            {
                model.ContenView = new TextBlock() { Text = "sssss" };
                model.Title = "MMMM";
            }
           // txt.Text="SSSSSSSS";
        }
    }
}