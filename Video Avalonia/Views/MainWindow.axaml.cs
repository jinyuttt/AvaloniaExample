using Avalonia.Controls;
using System.IO;
using System;
using System.Drawing;
using AForge.Video.DirectShow;

namespace Video_Avalonia.Views
{
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoDevice;
        private VideoCapabilities[] videoCapabilities;
        private VideoCaptureDevice videoSource;

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (videoDevices.Count == 0)
                throw new ApplicationException();
            VideolistBox.Items.Clear();
            foreach (FilterInfo device in videoDevices)
            {
                VideolistBox.Items.Add(device.Name);
            }
            VideolistBox.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice(videoDevices[VideolistBox.SelectedIndex].MonikerString);
            for (int i = 0; i < videoSource.VideoCapabilities.Length; i++)
            {
                cbo_rate.Items.Add(videoSource.VideoCapabilities[i].FrameSize.Width + "*" + videoSource.VideoCapabilities[i].FrameSize.Height);
            }
            cbo_rate.SelectedIndex = 0;

            VideoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;


        }

        private void VideoSourcePlayer_NewFrame(object sender, ref Bitmap image)
        {
            
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            videoSource.VideoResolution = videoSource.VideoCapabilities[cbo_rate.SelectedIndex];
            VideoSourcePlayer.VideoSource = videoSource;
            VideoSourcePlayer.Start();


        }

        private void Button_Click_1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {

            VideoSourcePlayer.Stop();

        }
    }
}