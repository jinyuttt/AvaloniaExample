using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using System.Reflection;

namespace Test;

public partial class TabClose : UserControl
{
    Window _shell;
    int _index;
    public TabClose(Window shell, int index, string headerName)
    {
        InitializeComponent();
        _shell = shell;
        _index = index;
        NameLabel.Content = headerName;
    }
    private void button_close_Click(object sender, System.Windows.RoutedEventArgs e)
    {
       // _shell.CloseTab(_index);
    }
}