using Avalonia.Controls;
using Avalonia.Extensions.Media;
using Avalonia.Platform.Storage;
using LibVLCSharp.Shared;
using System.IO;

namespace PlayerAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var topLevel = TopLevel.GetTopLevel(this);

            // 启动异步操作以打开对话框。
            var files =  topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            }).Result;
            //var files = await storage.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            //{
            //    Title = title,
            //    //您可以添加自定义文件类型，也可以从内置文件类型添加。请参阅“定义自定义文件类型”，了解如何创建自定义文件类型。
            //    FileTypeFilter = new[] { ImageAll, FilePickerFileTypes.TextPlain }
            //});
            if (files.Count > 0)
            {
                PlayerView VideoView = this.FindControl<PlayerView>("playerView");
              
                VideoView.Play(files[0].Path);
               
            }
        }
    }
}