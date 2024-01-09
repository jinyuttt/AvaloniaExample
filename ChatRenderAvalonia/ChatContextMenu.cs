using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Drawing;

namespace ChatRenderDemo.WPF
{
    //聊天消息右键菜单
    class ChatContextMenu
    {
        private static OrayChatContextMenuHandler Handler;
        private static ContextMenu Menu;
        private static MenuItem RecallItem;

        static ChatContextMenu()
        {
            ChatContextMenu.Menu = new ContextMenu()
            {
                FontFamily =new Avalonia.Media.FontFamily("微软雅黑"),
                FontStyle =  Avalonia.Media.FontStyle.Normal,
            };
            ChatContextMenu.RecallItem = new MenuItem();
            ChatContextMenu.RecallItem.Header = "撤回";
            ChatContextMenu.RecallItem.Click += RecallItem_Click;
            ChatContextMenu.Menu.Items.Add(ChatContextMenu.RecallItem);

        }

        private static void RecallItem_Click(object? sender, RoutedEventArgs e)
        {
            ChatContextMenu.Handler.OnRecall();
        }

        //private static void RecallItem_Click(object sender, RoutedEventArgs e)
        //{
        //    ChatContextMenu.Handler.OnRecall();
        //}

        public static void SetRecallVisible(bool isShow)
        {
            ChatContextMenu.RecallItem.IsVisible = isShow;
        }

        public static void SetHandler(OrayChatContextMenuHandler handler)
        {
            ChatContextMenu.Handler = handler;
        }

        public static void Show()
        {
            ChatContextMenu.Menu.Focus();
           // ChatContextMenu.Menu.IsOpen = true;
        }
    }

    interface OrayChatContextMenuHandler
    {
        void OnRecall();
    }
}
