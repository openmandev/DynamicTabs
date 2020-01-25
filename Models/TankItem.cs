using System.Collections.Generic;
using System.Collections.ObjectModel;

// using ecb = ecb__namespace.ecb__class;
// using ecbi = ecbi__namespace.ecbi__class;
// using qpln = qpln__namespace.qpln__class;

namespace DynamicTabs.Models
{
  public class TankItem : NodeItem
  {
    public long Pln { get; set; }
    public long Tnk { get; set; }
    public string TnkName { get; set; }
    public Dictionary<string,string> TnkType { get; set; }
    // public ObservableCollection<string> TnkType { get; set; }
    public KeyValuePair<string,string> TnkTypeSelected { get; set; }
    // public string TnkTypeSelected { get; set; }
    public int TnkTypeSelectedIndex { get; set; }
    public double TnkPosX { get; set; }
    public double TnkPosY { get; set; }
    public double TnkPosZ { get; set; }
    // public ecb.t_tnk tnk { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // public ecbi.s_tnk stnk { get; set; } //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // public qpln.tnk xtnk { get; set; }
  }
}
