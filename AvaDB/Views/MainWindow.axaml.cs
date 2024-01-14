using Avalonia.Controls;
using Avalonia.Input;
using AvaDB.ViewModels;
using System;
using static AvaDB.DialogManager;

namespace AvaDB.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander; 
        }

        private void TitleBar_OnPointerMouseHander(object? sender, PointerPressedEventArgs e)
        {
            this.BeginMoveDrag(e);
        }

        private void Button_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var item = new TabItem() { Header = "无标题-查询" };
            item.Content = new QueryPage();
            tabQuery.Items.Add(item);
            
        }
        // 定义枚举开始的值
        private int i = 0;
        private void OpenDB(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // 弹窗新窗口
             var item=  sender as MenuItem;
            if (item != null)
            {
                DialogManager.DialogClosed += DialogManager_DialogClosed;
            }
            DialogManager.Show((DialogType)1, item==null?"no":item.Header.ToString(),400,600);

            // 超过枚举值重新赋值
            if (i == 6)
            {
                i = 0;
            }
        }

        private void DialogManager_DialogClosed(object? sender, Dialog e)
        {
            if(e.Result==DialogResult.OK)
            {
                MainWindowViewModel model = DataContext as MainWindowViewModel;
                DBViewModel dBView=e.DataContext as DBViewModel;
                if (model != null)
                {
                    model.Nodes.Add(new Node(dBView.Name));
                }
            }
        }

        private void TreeView_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            var node = e.Source;
        }

        private void TreeView_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
           var node= e.Source;
        }
    }
}