using Avalonia.Controls;

namespace WindowAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander; ;
        }

        private void TitleBar_OnPointerMouseHander(object? sender, Avalonia.Input.PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }
    }
}