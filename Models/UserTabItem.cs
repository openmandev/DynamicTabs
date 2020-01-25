using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicTabs.Models
{
  public class UserTabItem
  {
    public enum UserTabType
    {
      PLANES,
      CALIBRATES,
      NODES,
      DESKS,
      VIEW2D,
      CATALOG,
      TEST
    }
    public string TabImage { get; set; }
    public string TabTitle { get; set; }
    public UserTabType TabType { get; set; }
  }
}
