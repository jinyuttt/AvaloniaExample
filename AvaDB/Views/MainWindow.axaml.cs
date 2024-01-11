using Avalonia.Controls;
using Avalonia.Input;
using System;

namespace AvaDB.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander; 
        }

        private void TitleBar_OnPointerMouseHander(object? sender, PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var item = new TabItem() { Header = "�ޱ���-��ѯ" };
            item.Content = new QueryPage();
            tabQuery.Items.Add(item);
            
        }
    }
}