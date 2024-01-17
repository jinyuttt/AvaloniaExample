using AvaDB.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Hikari;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;
using System.Text;
using System;

namespace AvaDB.Views
{
    public partial class Dialog : Window
    {

        public string DBType {  get; set; }

        public DialogResult Result { get; set; } = DialogResult.Unkonw;


        public Dialog()
        {

            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
            this.Loaded += Dialog_Loaded;
        }

        private void Dialog_Loaded(object? sender, RoutedEventArgs e)
        {
            Init();
        }

        private  void Init()
        {
            if (DBType == "MySQL")
            {
                txtPort.Text = "3306";
                txtHost.Text = "192.168.237.128";
                txtName.Text = "mysql";
                txtUser.Text = "jinyu";
                txtPsw.Text = "";

            }
        }
        private HikariConfig GetConnect(string type)
        {
            DBViewModel viewModel = this.DataContext as DBViewModel;
            Hikari.HikariConfig config = new Hikari.HikariConfig();

            config.LoadConfig("DBPoolCfg/MySql_Hikari.cfg");
            StringBuilder builder = new StringBuilder();
            builder.Append(config.ConnectString);
            builder.Replace("{Host}", viewModel.Host);
            builder.Replace("{Port}", viewModel.Port.ToString());
            builder.Replace("{User}", viewModel.User);
            builder.Replace("{Password}", viewModel.Password);
            config.ConnectString = builder.ToString();
            return config;
        }
        public void DBConnectAction()
        {
           
           var config=GetConnect(DBType);
            HikariDataSource dataSource = new HikariDataSource(config);
            try
            {

                var db = dataSource.GetConnection();
                if(db.State!=System.Data.ConnectionState.Open)
                {
                    db.Open();
                }
                var result = MessageBoxManager.GetMessageBoxStandard("提示", "连接成功？",
                 ButtonEnum.Ok, MsBox.Avalonia.Enums.Icon.Info).ShowWindowDialogAsync(this);
            
            }
            catch (Exception ex)
            {
                var result = MessageBoxManager.GetMessageBoxStandard("提示", "连接失败？",
                   ButtonEnum.Ok,MsBox.Avalonia.Enums.Icon.Info).ShowWindowDialogAsync(this);
             
            }

            

        }

        private void Close_OnClick(object? sender, RoutedEventArgs e)
        {
            Result = DialogResult.Unkonw;
            (this.DataContext as DBViewModel).Result = Result;
            Close();
        }

        private void Button_Click_OK(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var config = GetConnect(DBType);

            Result = DialogResult.OK;
            var mode = this.DataContext as DBViewModel;
            mode.Result = Result;
            mode.ConnectString = config.ConnectString;
            mode.Properties = config.Parameters;
            mode.CfgPath = $"DBPoolCfg/{DBType}_Hikari.cfg";
            Close();
        }

        private void Button_Click_Cancle(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Result = DialogResult.Cancel;
            (this.DataContext as DBViewModel).Result = Result;
            Close(true);
        }

        private void Button_Click_Test(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            DBConnectAction();
        }
    }
}
