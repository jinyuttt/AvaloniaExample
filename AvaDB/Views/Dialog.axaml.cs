using AvaDB.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaDB.Views
{
    public partial class Dialog : Window
    {

        public string DBType {  get; set; }

        public DialogResult Result { get; set; } = DialogResult.Unkonw;
        public Dialog()
        {

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
           
        }
        private void Close_OnClick(object? sender, RoutedEventArgs e)
        {
            Result = DialogResult.Unkonw;
            (this.DataContext as DBViewModel).Result = Result;
            Close();
        }

        private void Button_Click_OK(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
           Result = DialogResult.OK;
            (this.DataContext as DBViewModel).Result = Result;
            Close();
        }

        private void Button_Click_Cancle(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            (this.DataContext as DBViewModel).Result = Result;
            Close(true);
        }
    }
}
