using System.Collections.ObjectModel;

// using ecb = ecb__namespace.ecb__class;
// using ecbi = ecbi__namespace.ecbi__class;

namespace DynamicTabs.Models
{
  public class PlaneItem : NodeItem
  {
    public long Pln { get; set; }
    public string PlnName { get; set; }
    // public ecb.t_pln pln { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // public ecbi.s_pln spln { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
  }
}
