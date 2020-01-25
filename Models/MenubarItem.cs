using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;

namespace DynamicTabs.Models
{
  public class MenubarItem : IMenubarItemBase
  {
    public string ImagePath { get; set; }
    public string Header { get; set; }
    public ICommand Command { get; set; }
    public INotifyPropertyChanged CommandParameter { get; set; }
    public IList<MenubarItem> Items { get; set; }
  }
}
