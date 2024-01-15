using Avalonia.Controls;
using Avalonia.Input;
using AvaDB.ViewModels;
using System;
using static AvaDB.DialogManager;
using Hikari;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Data;

namespace AvaDB.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander; 
            treeView.Items.Clear();
            
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
       // private int i = 0;
        private void OpenDB(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            // 弹窗新窗口
             var item=  sender as MenuItem;
            if (item != null)
            {
                DialogManager.DialogClosed += DialogManager_DialogClosed;
            }
            DialogManager.Show((DialogType)1, item==null?"no":item.Header.ToString(),400,600);

           
        }

        private void DialogManager_DialogClosed(object? sender, Dialog e)
        {
            if(e.Result==DialogResult.OK)
            {
                MainWindowViewModel model = DataContext as MainWindowViewModel;
                DBViewModel dBView=e.DataContext as DBViewModel;
                if (model != null)
                {
                    var node = new Node(dBView.Name,new System.Collections.ObjectModel.ObservableCollection<Node>());
                    node.NodeType = NodeEnum.Name;
                    node.ConnectString=dBView.ConnectString;
                    node.Connection = dBView.DbConnection;
                    node.Properties = dBView.Properties;
                    node.CfgPath = dBView.CfgPath;
                    model.Nodes.Add(node);
                }
            }
        }

        private void TreeView_DoubleTapped(object? sender, Avalonia.Input.TappedEventArgs e)
        {
            var item = e.Source as Avalonia.Controls.Control;
            var node = item.DataContext as Node;
            if (node != null) {
                if(node.NodeType == NodeEnum.Name)
                {
                   if(node.Connection == null)
                    {
                        Hikari.HikariConfig config=new Hikari.HikariConfig();
                        config.ConnectString=node.ConnectString;
                        config.LoadConfig(node.CfgPath);
                        config.ConnectString= node.ConnectString;
                        HikariDataSource dataSource=new HikariDataSource(config);
                      
                        try
                        {

                            var db = dataSource.GetConnection();
                            if (db.State != System.Data.ConnectionState.Open)
                            {
                                db.Open();
                            }
                            node.Connection=db;
                            node.DataAdapter = dataSource.DataAdapter;

                        }
                        catch (Exception ex)
                        {
                            var result = MessageBoxManager.GetMessageBoxStandard("提示", "连接失败？",
                               ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowWindowDialogAsync(this);
                            return;

                        }
                        
                    }
                    string query = "";
                    if (node.Properties.TryGetValue("allbase", out query))
                    {
                        node.DataAdapter.SelectCommand=node.Connection.CreateCommand();
                        node.DataAdapter.SelectCommand.CommandText=query;
                        var ds = new DataSet();
                        node.DataAdapter.Fill(ds);
                        
                        foreach(DataRow row in ds.Tables[0].Rows)
                        {
                            var sub=new Node(row[0].ToString(),new System.Collections.ObjectModel.ObservableCollection<Node>());
                            sub.Properties=node.Properties;
                            sub.CfgPath=node.CfgPath;
                            sub.ConnectString=node.ConnectString;
                            sub.Connection=node.Connection;
                            sub.DataAdapter = node.DataAdapter;
                            sub.NodeType = NodeEnum.DB;
                            node.SubNodes.Add(sub);
                        }
                    }
                }
                else if(node.NodeType == NodeEnum.DB)
                {
                    string query = "";
                    if (node.Properties.TryGetValue("alltables", out query))
                    {
                        node.DataAdapter.SelectCommand = node.Connection.CreateCommand();
                        node.DataAdapter.SelectCommand.CommandText = query;
                        var ds = new DataSet();
                        node.DataAdapter.Fill(ds);

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            var sub = new Node(row[0].ToString(), new System.Collections.ObjectModel.ObservableCollection<Node>());
                            sub.Properties = node.Properties;
                            sub.CfgPath = node.CfgPath;
                            sub.ConnectString = node.ConnectString;
                            sub.Connection = node.Connection;
                            sub.DataAdapter = node.DataAdapter;
                            sub.NodeType=NodeEnum.Table;
                            node.SubNodes.Add(sub);
                        }
                    }
                }
            }
        }

        private void TreeView_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
           var node= e.Source;
        }
    }
}