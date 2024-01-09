using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace WindowStyleAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PointerPressed += MainWindow_PointerPressed;
        }

        private void MainWindow_PointerPressed(object? sender, PointerPressedEventArgs e)
        {
            if (e.Pointer.Type == PointerType.Mouse)
            {

                this.BeginMoveDrag(e);
            }
        }
    }
}