﻿using System.Collections.ObjectModel;
using Test.Views;

namespace Test.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<ActionTabItem> Tabs { get; set; }

        public ActionTabViewModal()
        {
            Tabs = new ObservableCollection<ActionTabItem>();
        }

        public void Populate()
        {
            // Add A tab to TabControl With a specific header and Content(UserControl)
            Tabs.Add(new ActionTabItem { Header = "UserControl 1", Content = new TabClose() });
            // Add A tab to TabControl With a specific header and Content(UserControl)
            Tabs.Add(new ActionTabItem { Header = "UserControl 2", Content = new TabClose() });
        }
    }
}
