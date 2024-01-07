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

            // �����첽�����Դ򿪶Ի���
            var files =  topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Open Text File",
                AllowMultiple = false
            }).Result;
            //var files = await storage.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions()
            //{
            //    Title = title,
            //    //����������Զ����ļ����ͣ�Ҳ���Դ������ļ�������ӡ�����ġ������Զ����ļ����͡����˽���δ����Զ����ļ����͡�
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