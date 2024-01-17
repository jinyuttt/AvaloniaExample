using System.Collections.ObjectModel;

namespace AvaDB.ViewModels
{
    public class TableTailModel:ViewModelBase
    {
        public ObservableCollection<TableField>? TableNote { get; set; }

      

        public TableTailModel()
        {
            TableNote= new ObservableCollection<TableField>();
            //FieldTypes = new ObservableCollection<string>() { "char", "double", "int", "json", "bigint" };
        }
    }
}
