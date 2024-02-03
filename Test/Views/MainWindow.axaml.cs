using Avalonia.Controls;
using Test.ViewModels;
using Tmds.DBus.Protocol;

namespace Test.Views
{
    public partial class MainWindow : Window
    {
        private ActionTabViewModal vmd;

        public MainWindow()
        {
            InitializeComponent();
            vmd = new ActionTabViewModal();
            // Bind the xaml TabControl to view model tabs
            actionTabs.ItemsSource = vmd.Tabs;
            // Populate the view model tabs
            vmd.Populate();
        }
        public void addTab(Connection connection)
        {
            TabItem tab = new TabItem();
            tab.Header = connection.name;
            tabConnections.Items.Add(tab);
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // This event will be thrown when on a close image clicked
            vmd.Tabs.RemoveAt(actionTabs.SelectedIndex);
        }
    }
    // This class will be the Tab int the TabControl
    public class ActionTabItem
    {
        // This will be the text in the tab control
        public string Header { get; set; }
        // This will be the content of the tab control It is a UserControl whits you need to create manualy
        public UserControl Content { get; set; }
    }
}