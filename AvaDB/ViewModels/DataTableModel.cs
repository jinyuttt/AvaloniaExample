using System.Collections.ObjectModel;
using System.Data;

namespace AvaDB.ViewModels
{
    public class DataTableModel : ViewModelBase
    {
       // public ObservableCollection<DataRow> Table { get; set; }

        public ObservableCollection<GridRow> Datas { get; set; }
        public DataTableModel() { 
          //  Table = new ObservableCollection<DataRow>();
            Datas = new ObservableCollection<GridRow>();
        }
    }
}
