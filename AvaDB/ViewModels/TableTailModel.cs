using System.Collections.ObjectModel;

namespace AvaDB.ViewModels
{
    public class TableTailModel:ViewModelBase
    {
        public ObservableCollection<TableField>? TableNote { get; set; }

        public OptTable Opt {  get; set; }

      public string TableName { get; set; }

        public TableTailModel()
        {
            TableNote= new ObservableCollection<TableField>();
          
        }
    }
}
