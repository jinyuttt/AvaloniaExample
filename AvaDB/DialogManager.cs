using Avalonia.Controls;
using Avalonia.Threading;
using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvaDB.Views;

namespace AvaDB
{
    internal class DialogManager
    {
        private static readonly Dictionary<DialogType, Dialog> _dialogBases = new();

        public static event EventHandler<Dialog> DialogClosed;

        public static void Show(DialogType type, string content, int height = 100, int width = 200, int timing = 0)
        {
            Dialog dialog;
            // 防止并发可自行修改
            lock (_dialogBases)
            {
                if (_dialogBases.Remove(type, out var dialogBase))
                {
                    try
                    {
                        dialogBase.Close();
                    }
                    catch
                    {
                    }
                }

                dialog = new Dialog
                {
                    Height = height,
                    Width = width,
                    DataContext = new AvaDB.ViewModels.DBViewModel(),
                    DBType = content,
                    WindowStartupLocation = WindowStartupLocation.Manual // 不设置的话无法修改窗口位置
                };
                dialog.Closed += (s, e) =>
                {
                   if(DialogClosed != null)
                    {
                        DialogClosed(null, dialog);
                    }
                };
                if (timing > 0)
                {
                    // 弹窗定时关闭
                    _ = Task.Run(async () =>
                    {
                        await Task.Delay(timing);
                        // 先删除并且拿到删除的value
                        if (_dialogBases.Remove(type, out var dialogBase))
                        {
                            // 操作组件需要使用ui线程
                            _ = Dispatcher.UIThread.InvokeAsync(() =>
                            {
                                try
                                {
                                    // 关闭弹窗组件
                                    dialogBase.Close();
                                }
                                // 可能已经被关闭所以可能会出现异常
                                catch
                                {
                                }
                            });
                        }
                    });
                }

                // 添加到字典中
                _dialogBases.TryAdd(type, dialog);
            }

            // 获取当前屏幕
            var bounds = dialog.Screens.ScreenFromVisual(dialog).Bounds;

            // 偏移
            int skewing = 20;
            // window的任务栏高度
            int taskbar = 50;
            int x, y;
            switch (type)
            {
                case DialogType.topLeft:
                    x = skewing;
                    y = skewing;
                    break;
                case DialogType.topCenter:
                    x = (int)((bounds.Width - dialog.Width) / 2);
                    y = skewing;
                    break;
                case DialogType.topRight:
                    x = (int)((bounds.Width - dialog.Width) - skewing);
                    y = skewing;
                    break;
                case DialogType.leftLower:
                    x = 20;
                    y = (int)(bounds.Height - dialog.Height) - taskbar - skewing;
                    break;
                case DialogType.centerLower:
                    x = (int)((bounds.Width - dialog.Width) / 2);
                    y = (int)(bounds.Height - dialog.Height) - taskbar - skewing;
                    break;
                case DialogType.rightLower:
                    x = (int)(bounds.Width - dialog.Width - skewing);
                    y = (int)(bounds.Height - dialog.Height) - taskbar - skewing;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            // 设置弹窗的位置
            dialog.Position = new PixelPoint(x, y);

            // 获取内容显示的组件并且将内容显示上去
            //var contentBox = dialog.Find<TextBlock>("Content");
            //contentBox.Text = content;
            var titleBox = dialog.Find<TextBlock>("txttitle");
            
            titleBox.Text = "新建连接-" + content;
            dialog.DBType = content;
            dialog.Show();
        }

        public static Object? GetData(DialogType type, string content)
        {

            if (_dialogBases.TryGetValue(type, out var dialogBase))
            {
              return  dialogBase.DataContext;
            }
            return null;
        }
        public enum DialogType
        {
            /// <summary>
            /// 左上
            /// </summary>
            topLeft,

            /// <summary>
            /// 居中靠上
            /// </summary>
            topCenter,

            /// <summary>
            /// 右上
            /// </summary>
            topRight,

            /// <summary>
            /// 左下
            /// </summary>
            leftLower,

            /// <summary>
            /// 居中靠下
            /// </summary>
            centerLower,

            /// <summary>
            /// 右下
            /// </summary>
            rightLower
        }
    }
}
