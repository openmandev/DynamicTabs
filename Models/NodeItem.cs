using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

// using ecb = ecb__namespace.ecb__class;

namespace DynamicTabs.Models
{
  public class NodeItem : INotifyPropertyChanged
  {
    private string imagePath;
    private string code;
    private string name;
    // private ecb.t_tnk tnk;
    // public ecb.t_tnk tnk; //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    // private PlaneItem planeItem;
    // private TankItem tankItem;

    public NodeItem()
    {
      Nodes = new ObservableCollection<NodeItem>();
    }

    public string ImagePath
    {
      get { return imagePath; }
      set
      {
        imagePath = value;
        OnPropertyChanged("ImagePath");
      }
    }
    public string Code
    {
      get { return code; }
      set
      {
        code = value;
        OnPropertyChanged("Code");
      }
    }
    public string Name
    {
      get { return name; }
      set
      {
        name = value;
        OnPropertyChanged("Name");
      }
    }
    // public PlaneItem PlaneItem
    // {
    //   get { return planeItem; }
    //   set
    //   {
    //     planeItem = value;
    //     OnPropertyChanged("PlaneItem");
    //   }
    // }
    // public TankItem TankItem
    // {
    //   get { return tankItem; }
    //   set
    //   {
    //     tankItem = value;
    //     OnPropertyChanged("TankItem");
    //   }
    // }
    public ObservableCollection<NodeItem> Nodes { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string prop = "")
    {
      if ( PropertyChanged != null )
        PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
  }
}
