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

        /// <summary>
        /// 名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; } = "";

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectString { get; set; } = "";

        public string Host { get; set; } = "localhost";

        public int Port { get; set; }

        public string User { get; set; }

        public string Password { get; set; }

        /// <summary>
        /// 连接
        /// </summary>
        public IDbConnection Connection { get; set; } = null;

        /// <summary>
        /// 适配器
        /// </summary>
        public IDbDataAdapter DataAdapter { get; set; } = null;

        /// <summary>
        /// 菜单是否显示
        /// </summary>
        public bool ContextMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Table; }
        }

       /// <summary>
       /// 连接菜单
       /// </summary>
        public bool ConMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Name; }
        }

        /// <summary>
        /// 数据库菜单
        /// </summary>
        public bool DBMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.DB; }
        }

        /// <summary>
        /// 其它菜单
        /// </summary>
        public bool OtherMenuIsVisiblity
        {
            get { return NodeType == NodeEnum.Other; }
        }

        /// <summary>
        /// 节点类型
        /// </summary>
        public NodeEnum NodeType { get; set; } = NodeEnum.Name;

        /// <summary>
        /// 配置路径
        /// </summary>
        public string CfgPath { get; set; }

        /// <summary>
        /// 数据库属性
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }
        public bool IsCheck { get; internal set; }

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