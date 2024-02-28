using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;

namespace AvaDB.ViewModels
{
    public class TabItemModel:ViewModelBase
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ContentControl Content { get; set; } 

        public ReactiveCommand<Unit, Unit> Close { get; }

        ObservableCollection<TabItemModel> Items;
        public TabItemModel(ObservableCollection<TabItemModel> Items) {
            this.Items = Items;
            Close = ReactiveCommand.Create(() => { Items.Remove(this); });
        }

    }
}