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

            webview.BrowserCreated += (s, e) => webview.Navigate("https://www.bydauto.com.cn/pc/home?type=dynasty&networkType=dynasty");

            webview.DocumentTitleChanged += (s, e) => Title = e.Title;

            Closing += (s, e) => Program.app?.Shutdown();
        }
    }
}