using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvaDB.ViewModels
{
    public class TableField
    {
        public string? Name { get; set; }

        public string? FieldType { get; set; }

        public string DataLen { get; set; }

        public bool IsNull { get; set; }

        public bool IsKey { get; set; }

        public string? Note { get; set; }

        public  ObservableCollection<string> FieldTypes { get; set; }

        public TableField()
        {
            FieldTypes = new ObservableCollection<string>() { "char", "double", "int", "json", "bigint" };
        }
    }
}
