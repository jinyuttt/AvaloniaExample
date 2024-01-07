
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.Input;
using LibMpv.Client;
using LibMpv.MVVM;
using System;

namespace LibMpvAvalonia.ViewModels
{

    public partial class MainWindowViewModel : BaseMpvContextViewModel
    {


        //public MpvContext Mpv { get; set; } = default!;

        public bool IsTextDurationsVisible => FunctionResolverFactory.GetPlatformId() != LibMpvPlatformID.Android;

        [RelayCommand]
        public void PlayPlayer()
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop ||
          desktop.MainWindow?.StorageProvider is not { } provider)
                throw new NullReferenceException("Missing StorageProvider instance.");

            var files =  provider.OpenFilePickerAsync(new FilePickerOpenOptions()
            {
                Title = "Open Text File",
                AllowMultiple = false
            }).Result;
           
            if (files.Count > 0)
            {

                this.LoadFile(files[0].Path.LocalPath);
                //this.LoadFile("http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4");
                this.Play();

            }
           
        }

        [RelayCommand]
        public void PausePlayer()
        {
            this.Pause();
        }

        [RelayCommand]
        public void StopPlayer()
        {
            this.Stop();
        }

      
        public override void InvokeInUIThread(Action action)
        {
           Dispatcher.UIThread.Invoke(action);
        }


        // public VideoRenderer Renderer { get; set; }

        string _mediaUrl = "http://commondatastorage.googleapis.com/gtv-videos-bucket/sample/BigBuckBunny.mp4";
        //public string MediaUrl
        //{
        //    get => _mediaUrl;
        //    set { this.(ref _mediaUrl, value); }
        //}

        

       

       
    }
}
