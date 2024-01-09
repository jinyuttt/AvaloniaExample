using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using System;

namespace WindowStyleAvalonia.Views
{
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            this.btn_close.Click += Btn_close_Click;
        }

        private void Btn_close_Click(object? sender, RoutedEventArgs e)
        {
            var window = ((IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime)
               .MainWindow;
            window.Close();
        }
    }
}
