using AvaDB.Views;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaDB;

public partial class SaveDB : Window
{
    public DialogResult Result { get; set; }
    public SaveDB()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Result = DialogResult.OK;
        this.Close();
    }

    private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        Result = DialogResult.Cancel;
        this.Close();
    }
}