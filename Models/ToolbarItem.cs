using System.Collections.Generic;
using System.Windows.Input;
using System.ComponentModel;

namespace DynamicTabs.Models
{
  public class ToolbarItem
  {
    public string ImagePath { get; set; }
    public string ToolTip { get; set; }
    public ICommand Command { get; set; }
    public INotifyPropertyChanged CommandParameter { get; set; }
    public IList<ToolbarItem> Items { get; set; }
  }
}
