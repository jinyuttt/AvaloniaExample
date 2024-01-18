using AvaDB.ViewModels;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Models.TreeDataGrid;
using Avalonia.Markup.Xaml;
using DynamicData;
using Mysqlx.Expr;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace AvaDB;

public partial class QueryTable : UserControl
{
    public QueryTable()
    {
        InitializeComponent();
    }
    Expression<Func<GridRow, string>> getvalueexpression = customer => customer.CellValue;
    Expression<Func<GridRow, string>> getnameexpression = customer => customer.Name;
    Expression<Func<GridRow, string>> getcelltypeexpression = customer => customer.CellType;
    Expression<Func<GridRow, int>> getidexpression = customer => customer.Id;
    public void BindDataTable(DataTable dt)
    {

       
        var source = new FlatTreeDataGridSource<DataRow>(dt.Rows.AsParallel().Cast<DataRow>());

        foreach(DataColumn col in dt.Columns)
        {
            source.Columns.Add(new TextColumn<DataRow, string>(col.ColumnName, GetGetexpression(col)));
        }
        treedata.Source = source;
    }

    private static Expression<Func<DataRow, string>> GetGetexpression(DataColumn col)
    {
        Expression<Func<DataRow, string>> getexpression = customer => customer[col].ToString();
        return getexpression;
    }

    //private Func<DataRow, T> GetFunc<T>(DataColumn col)
    //{
       

    //}
}