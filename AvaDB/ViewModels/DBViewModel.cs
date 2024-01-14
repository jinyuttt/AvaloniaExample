using AvaDB.Views;
using Hikari.Manager;
using MsBox.Avalonia;
using ReactiveUI;
using System;
using System.Reactive;

namespace AvaDB.ViewModels
{
    public class DBViewModel : ViewModelBase
    {

        public string Name {  get; set; }

        public string Description { get; set; }

        public string Host {  get; set; }

        public int Port {  get; set; }

        public string User {  get; set; }

        public string Password { get; set; }

        public bool IsCheck {  get; set; }=true;

        public DialogResult Result { get; set; }

        public ReactiveCommand<Unit, Unit> DBConnect { get; }

        public DBViewModel() {
            DBConnect = ReactiveCommand.Create(DBConnectAction);
        }


        public void DBConnectAction()
        {
            string con = $"{Host}:{Port};{User}:{Password}";
            var db = ManagerPool.Singleton.GetDbConnection("MySql");
            try
            {
                db.ConnectionString = con;
                var p = ManagerPool.Singleton.GetParameters();


                db.Open();
                MessageBoxManager.GetMessageBoxStandard("提示", "连接成功");
            }
            catch (Exception ex)
            {
                MessageBoxManager.GetMessageBoxStandard("提示", "连接失败");
            }

            if (IsCheck)
            {

            }

        }
    }
}
