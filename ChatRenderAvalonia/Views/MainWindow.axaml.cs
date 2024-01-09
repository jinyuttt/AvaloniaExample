using Avalonia.Controls;
using System.Collections.Generic;
using System;
using ChatRenderDemo.WPF;
using Avalonia.Interactivity;

using System.IO;
using System.Drawing;
using Image = System.Drawing.Image;

namespace ChatRenderAvalonia.Views
{
    public partial class MainWindow : Window
    {
        private bool isMyselfSay = true;
        // private IChatRender chatRender;
        private const string myselfId = "aa01";
        private const string otherId = "aa02";
        private RenderDataProvider renderDataProvider;
        private static Dictionary<string, Bitmap> headDict = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> HeadDict
        {
            get
            {
                return headDict;
            }
        }
        private DateTime lastMessageTime = DateTime.MinValue;
        private object locker = new object();

        private static List<Image> emotionList = new List<Image>();
        public static List<Image> EmotionList
        {
            get
            {
                return emotionList;
            }
        }
        private static Dictionary<string, Bitmap> fileTypeDict = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> FileTypeDict
        {
            get
            {
                return fileTypeDict;
            }
        }
        private List<string> myChatMessageIdList = new List<string>();
        private string messageIdLastContextMenuCalled = null;
        public MainWindow()
        {
            InitializeComponent();

            this.LoadImageSource();
         
            //   this.LoadEmotion();
            //   this.renderDataProvider = new RenderDataProvider();
            //   this.chatRender = ChatRenderFactory.CreateChatRender(this.renderDataProvider, this.chatBoxWpf.ChatControl, myselfId, otherId, false);
            //   this.chatBoxWpf.Initialize(this.chatRender);
            //   this.chatRender.AudioMessageClicked += ChatRender_AudioMessageClicked;
            //   this.chatRender.ContextMenuCalled += ChatRender_ContextMenuCalled;
        }




        //private void ChatRender_ContextMenuCalled(System.Drawing.Point point, ChatMessageType type, string messageId)
        //{

        //    bool isRecall = this.myChatMessageIdList.Contains(messageId);
        //    if (!isRecall) { return; }
        //    this.messageIdLastContextMenuCalled = messageId;
        //    ChatContextMenu.SetHandler(this);
        //    ChatContextMenu.SetRecallVisible(isRecall);
        //    ChatContextMenu.Show();
        //}

        private void ChatRender_AudioMessageClicked(string MessageId, object AudioMessage)
        {
            //bool isPlayVoice = this.chatRender.IsPlayingAudioMessageAnimation(MessageId);
            //if (isPlayVoice)
            //{
            //    this.chatRender.StopPlayAudioMessageAnimation(MessageId);
            //}
            //else
            //{
            //    this.chatRender.StartPlayAudioMessageAnimation(MessageId);
            //}
        }

        private void buttonText_Click(object sender, RoutedEventArgs e)
        {
            string guid = Guid.NewGuid().ToString();
            this.AppendSendTime(DateTime.Now);
            //if (isMyselfSay)
            //{
            //    this.chatRender.AddChatItemText(guid, myselfId, "您好吗？[001]");
            //    this.myChatMessageIdList.Add(guid);
            //}
            //else
            //{
            //    this.chatRender.AddChatItemText(guid, otherId, "我很好，谢谢![001]");
            //}
            this.Window_Changed();
        }

        private void buttonImage_Click(object sender, RoutedEventArgs e)
        {
            //string imagePath = ESBasic.Helpers.FileHelper.GetFileToOpen2("请选择要使用的图片", Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures), CommonHelper.ImageExtensions);

            //if (string.IsNullOrEmpty(imagePath))
            //{
            //    return;
            //}
            //System.Drawing.Image img = System.Drawing.Image.FromFile(imagePath);
            //Bitmap bitmap = (Bitmap)img;
            //ImageSource imageSource = bitmap.GetImageSource();
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemImage(guid, isMyselfSay ? myselfId : otherId, imageSource, img.Size);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void buttonGif_Click(object sender, RoutedEventArgs e)
        {
            //string imagePath = ESBasic.Helpers.FileHelper.GetFileToOpen2("请选择要使用的gif动图", Environment.GetFolderPath(Environment.SpecialFolder.CommonPictures), CommonHelper.ImageGifExtensions);

            //if (string.IsNullOrEmpty(imagePath))
            //{
            //    return;
            //}
            //byte[] imageByte = ESBasic.Helpers.FileHelper.ReadFileReturnBytes(imagePath);
            //GifImage gifImage = ChatRenderFactory.GetGifImageFromBuffer(imageByte);
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemGif(guid, isMyselfSay ? myselfId : otherId, gifImage);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void buttonCard_Click(object sender, RoutedEventArgs e)
        {
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemCard(guid, isMyselfSay ? myselfId : otherId, isMyselfSay ? myselfId : otherId);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void buttonFile_Click(object sender, RoutedEventArgs e)
        {
            //string fileOrFolderPath = ESBasic.Helpers.FileHelper.GetFileToOpen("请选择要发送的文件");
            //if (fileOrFolderPath == null)
            //{
            //    return;
            //}
            //string fileName = Path.GetFileName(fileOrFolderPath);
            //FileInfo fileInfo = new FileInfo(fileOrFolderPath);
            //long fileSize = fileInfo.Length;
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemFile(guid, isMyselfSay ? myselfId : otherId, fileName, (ulong)fileSize, FileTransState.NotStart, null, false);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void buttonVoice_Click(object sender, RoutedEventArgs e)
        {
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemAudio(guid, isMyselfSay ? myselfId : otherId, 2, null);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void buttonCall_Click(object sender, RoutedEventArgs e)
        {
            //bool isAudio = (sender as Button).Content.ToString() == "语音通话";
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //this.chatRender.AddChatItemMedia(guid, isMyselfSay ? myselfId : otherId, "14:34", isAudio);
            //this.Window_Changed();
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            this.isMyselfSay = (sender as RadioButton).Content.ToString() == "我说";
        }

        /// <summary>
        /// 加载头像、文件类型图片资源
        /// </summary> 
        private void LoadImageSource()
        {
            for (int i = 1; i < 4; i++)
            {
                string imgPath = $"{AppDomain.CurrentDomain.BaseDirectory}Head\\{i}.png";
                Bitmap headImg = new Bitmap(imgPath);
                headDict.Add(i.ToString(), headImg);
            }
            DirectoryInfo directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "FileIcon\\");
            FileInfo[] files = directoryInfo.GetFiles("*.png");
            for (int i = 0; i < files.Length; i++)
            {
                string headFilePath = AppDomain.CurrentDomain.BaseDirectory + $"FileIcon\\{files[i]}";
                if (File.Exists(headFilePath))
                {
                    Bitmap bitmapImage = new Bitmap(headFilePath);
                    string fileToken = System.IO.Path.GetFileNameWithoutExtension(headFilePath).Replace("file_type_", "");
                    fileTypeDict.Add(fileToken, bitmapImage);
                }
            }
        }

        /// <summary>
        /// 追加系统时间
        /// </summary> 
        private void AppendSendTime(DateTime time)
        {
            lock (this.locker)
            {
                if (time.Subtract(this.lastMessageTime) > TimeSpan.FromMinutes(5))
                {
                    this.lastMessageTime = time;
                    //this.chatRender.AddChatItemTime(time);
                }
            }
        }

        /// <summary>
        /// 加载表情资源
        /// </summary> 
        private void LoadEmotion()
        {
            //List<string> tempList = ESBasic.Helpers.FileHelper.GetOffspringFiles(AppDomain.CurrentDomain.BaseDirectory + "Emotion\\");
            //List<string> emotionFileList = new List<string>();
            //foreach (string file in tempList)
            //{
            //    string name = file.ToLower();
            //    if (name.EndsWith(".bmp") || name.EndsWith(".jpg") || name.EndsWith(".jpeg") || name.EndsWith(".png") || name.EndsWith(".gif"))
            //    {
            //        emotionFileList.Add(name);
            //    }
            //}
            //emotionFileList.Sort(new Comparison<string>(CompareEmotionName));
            //for (int i = 0; i < emotionFileList.Count; i++)
            //{
            //    emotionList.Add((System.Drawing.Image.FromFile(AppDomain.CurrentDomain.BaseDirectory + "Emotion\\" + emotionFileList[i])).GetImageSource(""));
            //}
        }


        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        private static int CompareEmotionName(string a, string b)
        {
            if (a.Length != b.Length)
            {
                return a.Length - b.Length;
            }

            return a.CompareTo(b);
        }
        internal void Window_Changed()
        {
            //this.chatBoxWpf.ChangeRenderSurfaceSize();
            //this.chatBoxWpf.InvalidateVisual();
            //this.chatBoxWpf.ScrollToEnd();
        }

        public void OnRecall()
        {
            //this.chatRender.RecallChatMessage(this.messageIdLastContextMenuCalled);
            //this.myChatMessageIdList.Remove(this.messageIdLastContextMenuCalled);
        }

        private void buttonQuote_Click(object sender, RoutedEventArgs e)
        {
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);
            //ReferencedChatMessage referencedChatMessage = new ReferencedChatMessage(isMyselfSay ? otherId : myselfId, isMyselfSay ? "我很好，谢谢![001]" : "您好？[001]", lastMessageTime);
            //this.chatRender.AddChatItemText(guid, isMyselfSay ? myselfId : otherId, isMyselfSay ? "好的[000]" : "我很好，谢谢[000]", referencedChatMessage);
            //if (isMyselfSay) { this.myChatMessageIdList.Add(guid); }
            //this.Window_Changed();
        }

        private void buttonCustomize_Click(object sender, RoutedEventArgs e)
        {
            //string guid = Guid.NewGuid().ToString();
            //this.AppendSendTime(DateTime.Now);

            //string userID = isMyselfSay ? otherId : myselfId;
            //bool drawName = false;
            //ChatItemCustomize item = new ChatItemCustomize(this.chatRender.SysRender, this.renderDataProvider, this.chatBoxWpf.ChatControl.RenderSize.Width, guid, userID, isMyselfSay, drawName);
            //this.chatRender.AddChatItemCustomized(item);
            //if (isMyselfSay)
            //{
            //    this.myChatMessageIdList.Add(guid);
            //}
            //this.Window_Changed();
        }
    }
}
