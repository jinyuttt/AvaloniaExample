using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Reactive;

namespace AvaDB.ViewModels
{
    public class Node
    {
        public ObservableCollection<Node>? SubNodes { get; }
        public string Title { get; }

        public string Description { get; set; } = "";

        public string ConnectString { get; set; } = "";

        public IDbConnection Connection { get; set; } = null;

        public IDbDataAdapter DataAdapter { get; set; } = null;

        public bool ContextMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Table; }
        }
        public bool ConMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Name; }
        }
        public bool DBMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.DB; }
        }
        public bool OtherMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Other; }
        }

        public NodeEnum NodeType { get; set; } = NodeEnum.Name;

        public string CfgPath { get; set; }

        public Dictionary<string, string> Properties { get; set; }

        public Node()
        {
            
        }
        public Node(string title)
        {
            Title = title;
        }

        public Node(string title, ObservableCollection<Node> subNodes)
        {
            Title = title;
            SubNodes = subNodes;
        }
    }
}