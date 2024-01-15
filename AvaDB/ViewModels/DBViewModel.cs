using AvaDB.Views;
using Hikari;
using Hikari.Manager;
using MsBox.Avalonia;
using MsBox.Avalonia.Enums;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reactive;
using System.Text;

namespace AvaDB.ViewModels
{
    public class DBViewModel : ViewModelBase
    {

        public string Name {  get; set; }

        public string Description { get; set; }

        public string Host { get; set; } = "localhost";

        public int Port {  get; set; }

        public string User {  get; set; }

        public string Password { get; set; }

        public bool IsCheck {  get; set; }=true;

        public string ConnectString {  get; set; }
        public DialogResult Result { get; set; }

        public IDbConnection DbConnection { get; set; }

        public Dictionary<string,string> Properties { get; set; }

        public string CfgPath { get; set; }

        public ReactiveCommand<Unit, Unit> DBConnect { get; set; }

        public DBViewModel() {
            DBConnect = ReactiveCommand.Create(DBConnectAction);
        }


        public void DBConnectAction()
        {
            //  string con = $"{Host}:{Port};{User}:{Password}";
            Hikari.HikariConfig config = new Hikari.HikariConfig();
            config.LoadConfig("DBPoolCfg/MySql_Hikari.cfg");
            StringBuilder builder = new StringBuilder();
            builder.Append(config.ConnectString);
            builder.Replace("{Host}", Host);
            builder.Replace("{Port}", Port.ToString());
            builder.Replace("{User}", User);
            builder.Replace("{Password}", Password);
            config.ConnectString = builder.ToString();
            HikariDataSource dataSource =new HikariDataSource(config);
          
           // var db = ManagerPool.Singleton.GetDbConnection("MySql");
            try
            {

               var db= dataSource.GetConnection();
               

               db.Open();
                MessageBoxManager.GetMessageBoxStandard("提示", "连接成功");
            }
            catch (Exception ex)
            {
                var result = MessageBoxManager.GetMessageBoxStandard("确认退出", "你确定要退出应用程序吗？",
                   ButtonEnum.YesNo,Icon.Info).ShowWindowDialogAsync(null);
                MessageBoxManager.GetMessageBoxStandard("提示", "连接失败");
            }

            if (IsCheck)
            {

            }

        }
    
    }
}
