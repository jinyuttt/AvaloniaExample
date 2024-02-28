using Avalonia.Controls;
using QRCoder;
using System.IO;
using System;
using Avalonia.Media.Imaging;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using System.Net.NetworkInformation;
using ZXing;
using SkiaSharp;

namespace ImageAvalonia.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Image()
        {

            //读取粘贴板内容
            string str = "The text which should be encoded.";
            //可以判断text为空不 
            QRCodeGenerator qrGenerator = new();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(str, QRCodeGenerator.ECCLevel.M);
            var qrcode = new BitmapByteQRCode(qrCodeData);
            var bitmap = qrcode.GetGraphic(10);
            //string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\a.png";
            ////生成图片文件
            //FileStream fs = new FileStream(path, FileMode.Create);
            //fs.Write(bitmap);
            //fs.Close();
            //Bitmap bitmap1 = new Bitmap(path);
            MemoryStream stream = new MemoryStream(bitmap);
            Bitmap bitmap1 =new  Bitmap(stream);

            qr.Source = bitmap1;
        }

        public void  ToQrCode()
        {
            string str = "The text which should be encoded.";
            var writer = new ZXing.ImageSharp.BarcodeWriter<Rgba32>
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new ZXing.QrCode.QrCodeEncodingOptions
                {
                    DisableECI = true,
                    CharacterSet = "UTF-8",
                    Width = 500,
                    Height = 500,
                    Margin = 1
                }
            };
            var image = writer.WriteAsImageSharp<Rgba32>(str);
            var ms = new MemoryStream();
            image.Save(ms, new PngEncoder());
            ms.Position = 0;
          //  MemoryStream stream = new MemoryStream(bitmap);
            Bitmap bitmap1 = new Bitmap(ms);
            img.Source = bitmap1;
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Image();
            ToQrCode();
        }
    }
}