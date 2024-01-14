using Avalonia.Controls;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;

namespace AvaDB.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Node> Nodes { get; }
        public ObservableCollection<Node> SelectedNodes { get; }
        private Window hostWindow;
         public ReactiveCommand<Unit, Unit> QuitProgramCommand { get; }

        public ReactiveCommand<Unit, Unit> OpenCommand { get; }
        //public void QuitProgramCommand()
        //{
        //    hostWindow.Close();
        //}
        public ObservableCollection<MenuItem> DBSource { get; set; }

        public MainWindowViewModel(Window _hostWindow )
        {
            hostWindow = _hostWindow;

            QuitProgramCommand = ReactiveCommand.Create(() => { hostWindow.Close(); });

            SelectedNodes = new ObservableCollection<Node>();
            Nodes = new ObservableCollection<Node>
            {
                new Node("Animals", new ObservableCollection<Node>
                {
                    new Node("Mammals", new ObservableCollection<Node>
                    {
                        new Node("Lion"), new Node("Cat"), new Node("Zebra")
                    })
                })
            };

            var moth = Nodes.Last().SubNodes?.Last();
            if (moth != null) SelectedNodes.Add(moth);

            // DBSource = new MenuItem[] { new() { Header="1" },new MenuItem {  Header="2"} };
            DBSource = new ObservableCollection<MenuItem> { new() { Header = "1" } };
        }
    }
}
