using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using ChatRenderAvalonia.Views;
namespace ChatRenderAvalonia.Views
{
    class RenderDataProvider 
    {
        public RenderDataProvider()
        {

        }
        public string GetSpeakerDisplayName(string speakerID)
        {
            return speakerID;
        }

        public string GetUserName(string userID)
        {
            return userID == "aa01" ? "张三" : "李四";
        }
        public object GetUserHeadImage(string speakerID)
        {
            return CommonHelper.GetUserHeadByID(speakerID);
        }
        public static Image Bitmap2ImageSource(System.Drawing.Bitmap bitmap)
        {
            IntPtr hBitmap = bitmap.GetHbitmap();
            Image wpfBitmap=null;
            //Image wpfBitmap =Imaging.CreateBitmapSourceFromHBitmap(
            //    hBitmap,
            //    IntPtr.Zero,
            //    Int32Rect.Empty,
            //    System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            //if (!DeleteObject(hBitmap))//记得要进行内存释放。否则会有内存不足的报错。
            //{
            //    throw new System.ComponentModel.Win32Exception();
            //}
            return wpfBitmap;
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);


        public object GetImageOfFileType(string fileExtendName)
        {

            return CommonHelper.GetFileIconBitmap(fileExtendName);
        }
        public string GetFilePathToSave(string fileName)
        {
            return CommonHelper.GetFilePathToSave(fileName);
        }
        public Dictionary<int, object> GetEmotions()
        {
            Dictionary<int, object> dictionary = new Dictionary<int, object>();
            List<Image> emotionList = MainWindow.EmotionList;
            for (int i = 0; i < emotionList.Count; i++)
            {
                dictionary.Add(i, emotionList[i]);
            }
            return dictionary;
        }
        public object GetImageOfSendFailed()
        {
            return null;
        }

        public object GetImageOfVideoCall()
        {
            Bitmap bitmapImage = new Bitmap("./Resources/videoCall.png");
           
            return bitmapImage;
        }

        public object GetImageOfAudioCall()
        {
            Bitmap bitmapImage = new Bitmap("./Resources/audioCall.png");
            return bitmapImage;
        }
    }
}
