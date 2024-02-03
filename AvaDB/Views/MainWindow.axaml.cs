using Avalonia.Controls;
using Avalonia.Input;
using AvaDB.ViewModels;
using System;
using static AvaDB.DialogManager;
using Hikari;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Data;
using System.Collections.ObjectModel;
using System.IO;
using AvaDB.Tools;
using DynamicData;

namespace AvaDB.Views
{
    public partial class MainWindow : Window
    {
        const string filejon = "dbnode.json";
        const string mydir = "avadb";
        public MainWindow()
        {
            InitializeComponent();
            this.
            titleBar.OnPointerMouseHander += TitleBar_OnPointerMouseHander; 
          
            this.Loaded += MainWindow_Loaded;
            
        }

        void load(ObservableCollection<Node> nodes)
        {
           string path= Path.Combine(Path.GetTempPath(),mydir, filejon); 
           var vfg= Util.JsonFileDeserialize<ObservableCollection<Node>>(path);
            if (vfg.Result!= null)
            {
                nodes.AddRange(vfg.Result);
            }
        }

        void save(ObservableCollection<Node> nodes)
        {
            string path = Path.Combine(Path.GetTempPath(),mydir, filejon);
             Util.JsonFileSerializeAsync<ObservableCollection<Node>>(nodes,path);
           
        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ObservableCollection<Node> nodes=  treeView.ItemsSource as ObservableCollection<Node>;
            nodes.Clear();
            load(nodes);
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
                    save(model.Nodes);

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
                    var subTables = new Node("表", new ObservableCollection<Node>());
                    subTables.Properties = node.Properties;
                    subTables.CfgPath = node.CfgPath;
                    subTables.ConnectString = node.ConnectString;
                    subTables.Connection = node.Connection;
                    subTables.DataAdapter = node.DataAdapter;
                    subTables.NodeType = NodeEnum.Tables;
                    node.SubNodes.Add(subTables);
                    //
                   var subOther = new Node("函数", new ObservableCollection<Node>());
                    subOther.Properties = node.Properties;
                    subOther.CfgPath = node.CfgPath;
                    subOther.ConnectString = node.ConnectString;
                    subOther.Connection = node.Connection;
                    subOther.DataAdapter = node.DataAdapter;
                    subOther.NodeType = NodeEnum.Functions;
                    node.SubNodes.Add(subOther);
                    //
                    subOther = new Node("视图", new ObservableCollection<Node>());
                    subOther.Properties = node.Properties;
                    subOther.CfgPath = node.CfgPath;
                    subOther.ConnectString = node.ConnectString;
                    subOther.Connection = node.Connection;
                    subOther.DataAdapter = node.DataAdapter;
                    subOther.NodeType = NodeEnum.Views;
                    node.SubNodes.Add(subOther);
                    //
                    subOther = new Node("查询", new ObservableCollection<Node>());
                    subOther.Properties = node.Properties;
                    subOther.CfgPath = node.CfgPath;
                    subOther.ConnectString = node.ConnectString;
                    subOther.Connection = node.Connection;
                    subOther.DataAdapter = node.DataAdapter;
                    subOther.NodeType = NodeEnum.QurySqls;
                    node.SubNodes.Add(subOther);

                    string query = "";
                    if (node.Properties.TryGetValue("alltables", out query))
                    {
                        node.Connection.ChangeDatabase(node.Title);
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
                            subTables.SubNodes.Add(sub);
                        }
                        
                     
                    }
                }
                else if(node.NodeType==NodeEnum.Table) 
                {
                    node.DataAdapter.SelectCommand = node.Connection.CreateCommand();
                    node.DataAdapter.SelectCommand.CommandText = "select * from "+node.Title;
                    var ds = new DataSet();
                    node.DataAdapter.Fill(ds);
                    QueryTable itemtable = new QueryTable();
                    var table = new DataTableModel();
                    itemtable.DataContext= table;
                    var tabItem = new TabItem() { Header =node.Title };
                    tabItem.Content = itemtable;
                    tabQuery.Items.Add(tabItem);
                    itemtable.BindDataTable(ds.Tables[0]);
                }
            }
        }

        private void TreeView_SelectionChanged(object? sender, Avalonia.Controls.SelectionChangedEventArgs e)
        {
          
        }

        private void MenuItem_Click_Table(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var menu = e.Source as MenuItem;
            var node = menu.DataContext as Node;
             
            if (node.Properties.TryGetValue("tableinfo", out var query))
            {
                node.DataAdapter.SelectCommand = node.Connection.CreateCommand();
                node.DataAdapter.SelectCommand.CommandText = query.Replace("{Table}",node.Title);
                var ds = new DataSet();
                node.DataAdapter.Fill(ds);

                TableTailModel tailModel=new TableTailModel();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    TableField field = new();
                    field.Name = row["Field"].ToString();
                    field.FieldType = row["Type"].ToString();
                    field.IsNull = row["Null"].ToString()=="NO"?true:false;
                    field.IsKey = row["Key"].ToString() == "PRI" ? true : false;
                    if (field.FieldType.Contains("("))
                    {
                        int index = field.FieldType.IndexOf("(");
                        string last = field.FieldType.Substring(index).Trim('(', ')');
                        field.DataLen = last;
                        field.FieldType = field.FieldType.Substring(0, index);
                    }
                    tailModel.TableNote.Add(field);
                }
                DesignTable designTable = new DesignTable();
                designTable.DataContext = tailModel;
                tabQuery.Items.Add(new TabItem() {  Content = designTable, Header="设计"+node.Title });
            
            }
        }

        private void MenuItem_Click_Open(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
        }

        private void MenuItem_Click_Create(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var menu = e.Source as MenuItem;
            var node = menu.DataContext as Node;
            DesignTable designTable = new DesignTable() { Connection = node.Connection };
            TableTailModel tailModel = new TableTailModel();
            designTable.DataContext = tailModel;
            tabQuery.Items.Add(new TabItem() { Content = designTable, Header = "设计" + node.Title });
            designTable.CreateTableHandler += DesignTable_CreateTableHandler;
        }

        private void DesignTable_CreateTableHandler(string obj)
        {
           
        }
    }
}