using Avalonia.Controls;
using CefNet.Avalonia;


namespace CefAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            WebView webview = new() { Focusable = true };
            Content = webview;

            webview.BrowserCreated += (s, e) => webview.Navigate("https://www.baidu.com");

            webview.DocumentTitleChanged += (s, e) => Title = e.Title;

            Closing += (s, e) => Program.app?.Shutdown();
        }
    }
}