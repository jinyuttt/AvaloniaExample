using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Data;

namespace AvaDB;

public partial class QueryTable : UserControl
{
    public QueryTable()
    {
        InitializeComponent();
    }

    public void BindDataTable(DataTable dt)
    {
        //
        foreach (DataColumn col in dt.Columns)
        {
            grdData.Columns.Add(new DataGridTextColumn() { Header=col.ColumnName});
        }
       
    }
}