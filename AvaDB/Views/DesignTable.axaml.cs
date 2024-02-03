using AvaDB.ViewModels;
using Avalonia.Controls;
using Avalonia.VisualTree;
using System;
using System.Data;
using System.Text;

namespace AvaDB;

public partial class DesignTable : UserControl
{
    public IDbConnection Connection { get; set; }

    public event Action<string> CreateTableHandler;
    public DesignTable()
    {
        InitializeComponent();
    }

    private void CreateTable(string table)
    {
        TableTailModel tailModel= grdFields.DataContext as TableTailModel;
        StringBuilder builder = new StringBuilder();
        builder.Append("create table if not exists " + table+"(");
        foreach(var item in tailModel.TableNote)
        {
            builder.Append(item.Name);
            builder.Append(" ");
            builder.Append(item.FieldType);
            if(!string.IsNullOrEmpty(item.DataLen) )
            {
                builder.Append("("+item.DataLen+")");
            }
            if(item.IsNull)
            {
                builder.Append("not null");
            }
            builder.Append(",");
        }
        builder.Remove(builder.Length-1,1);
        builder.Append(')');

        var cmd = Connection.CreateCommand();
        cmd.CommandText = builder.ToString();
       int num= cmd.ExecuteNonQuery();

        if (CreateTableHandler != null&&num>0) {
            CreateTableHandler(table);
        }

       


    }
    private void Button_Click_Add(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var source=  grdFields.DataContext as TableTailModel;
        source.TableNote.Add(new TableField());
    }

    private void Button_Click_Insert(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var source = grdFields.DataContext as TableTailModel;
        source.TableNote.Insert( grdFields.SelectedIndex, new TableField() );
        
    }

    private void Button_Click_Delete(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var source = grdFields.DataContext as TableTailModel;
        source.TableNote.RemoveAt(grdFields.SelectedIndex);
    }

    private async void Button_Click_Save(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var source = grdFields.DataContext as TableTailModel;
        if (string.IsNullOrEmpty(source.TableName))
        {
            SaveDB dB = new SaveDB() { Height = 100, Width=450, WindowStartupLocation= WindowStartupLocation.CenterScreen,DataContext=new SaveDBModel() };
          await  dB.ShowDialog((Window)this.GetVisualRoot());
            if(dB.Result == 0)
            {
                var item = dB.DataContext as SaveDBModel;
                CreateTable(item.Name);
            }
        }
    }

    private void DataGrid_CellEditEnded(object? sender, Avalonia.Controls.DataGridCellEditEndedEventArgs e)
    {

    }

    private void DataGrid_RowEditEnded(object? sender, Avalonia.Controls.DataGridRowEditEndedEventArgs e)
    {
    }
}