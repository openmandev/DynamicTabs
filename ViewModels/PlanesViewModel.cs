using System;
using System.Reactive;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DynamicTabs.Models;
using DynamicTabs.Views;

using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DynamicTabs.ViewModels
{
  public class PlanesViewModel : ViewModelBase
  {
    public delegate Unit CalibrateTabOpenHandler(int tnk);
    public event CalibrateTabOpenHandler CalibrateTabOpen;
    private bool dataProgress = false;
    private ObservableCollection<NodeItem> nodesRoot;
    private NodeItem selectedNode;
    private ObservableCollection<IMenubarItemBase> menuItemsPln;
    private ObservableCollection<IMenubarItemBase> menuItemsTnk;
    public UserTabItem UserTabItem { get; set; }
    public string ErrorMess { get; set; }
    public bool DataProgress
    {
      get => dataProgress;
      set => this.RaiseAndSetIfChanged(ref dataProgress, value);
    }
    public ObservableCollection<NodeItem> NodesRoot
    {
      get => nodesRoot;
      set => this.RaiseAndSetIfChanged(ref nodesRoot, value);
    }
    public ObservableCollection<IMenubarItemBase> MenuItemsPln
    {
      get => menuItemsPln;
      set => this.RaiseAndSetIfChanged(ref menuItemsPln, value);
    }
    public ObservableCollection<IMenubarItemBase> MenuItemsTnk
    {
      get => menuItemsTnk;
      set => this.RaiseAndSetIfChanged(ref menuItemsTnk, value);
    }
    public NodeItem SelectedNode
    {
      get => selectedNode;
      set => this.RaiseAndSetIfChanged(ref selectedNode, value);
    }
    public bool CMOpen { get; set; }
    public ReactiveCommand<Unit, bool> PlaneList { get; }
    public ReactiveCommand<Unit, Unit> Calibrate { get; }
    public ObservableCollection<ToolbarItem> ButtonListLeft { get; set; }
    public ObservableCollection<ToolbarItem> ButtonListRight { get; set; }

    public PlanesViewModel()
    {
      PlaneList = ReactiveCommand.CreateFromTask<Unit, bool>(_ => ExcaliburPlnLstAsync());

      // Calibrate = ReactiveCommand.CreateFromTask<Unit, Unit>(_ => CalibrateTabOpen());
      Calibrate = ReactiveCommand.Create
      ( () =>
        {
          // CalibrateTabOpen((SelectedNode as TankItem).stnk.tnk);
          CalibrateTabOpen(1);
          // Dispatcher.UIThread.Post((Action)(() => CalibrateTabOpen((SelectedNode as TankItem).stnk.tnk)));
        }
      );

      ButtonListLeft = new ObservableCollection<ToolbarItem>
      {
        // new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/pln_cre.png", ToolTip="Создать ЛА...", Command=PlaneCre },
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/clb.png", ToolTip="Тарировка", Command=Calibrate }
      };

      ButtonListRight = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", ToolTip="Обновить", Command=PlaneList }
      };

      menuItemsPln = new ObservableCollection<IMenubarItemBase>
      {
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/pln_upd.png", Header="Редактировать...", Command=PlaneUpd },
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/pln_del.png", Header="Удалить", Command=PlaneList },
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/upload.png", Header="Загрузить геометрию...", Command=PlnPntUpload },
        // new MenubarItem { Header="-" },
        new MenubarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", Header="Refresh", Command=PlaneList },
        // new MenubarItem { Header="-" },
        // new MenubarSeparator {},
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/tnk_cre.png", Header="Создать бак...", Command=TankCre },
      };

      menuItemsTnk = new ObservableCollection<IMenubarItemBase>
      {
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/tnk_upd.png", Header="Редактировать...", Command=TankUpd },
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/tnk_del.png", Header="Удалить", Command=PlaneList },
        // new MenubarItem { ImagePath="avares://DynamicTabs/Assets/upload.png", Header="Загрузить геометрию...", Command=TnkTrgUpload },
        // new MenubarItem { Header="-" },
        new MenubarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", Header="Refresh", Command=PlaneList },
        // new MenubarItem { Header="-" },
        // new MenubarSeparator {},
        new MenubarItem { ImagePath="avares://DynamicTabs/Assets/clb.png", Header="Context TabType2", Command=Calibrate }
      };
    }

    public PlanesViewModel(UserTabItem userTabItem) : this()
    {
      this.UserTabItem = userTabItem;

      this.WhenAnyValue(x => x.SelectedNode).Subscribe
      (
        onNext : _ => OnSelectedNode()
      );

      Task task = Task.Run(() => ExcaliburPlnLstAsync());

      Console.WriteLine("PlanesViewModel: ready");
    }

    // private void OnCalibrate()
    // {
    //   Console.WriteLine($"OnCalibrate: {SelectedNode}");

    //   CalibrateTabOpen(SelectedNode.tnk);
    // }

    private void OnSelectedNode()
    {
      Console.WriteLine($"OnSelectedNode: {selectedNode}");
    }

    private async Task<bool> ExcaliburPlnLstAsync()
    {
      Console.WriteLine("ExcaliburPlnLstAsync: begin");

      DataProgress = true;

      await Task.Delay(500);

      ObservableCollection<NodeItem> nodeItemList = new ObservableCollection<NodeItem>();

      PlaneItem nodeItemPln = new PlaneItem
      {
        ImagePath = "avares://DynamicTabs/Assets/pln.png",
        Code = "Code",
        Name = "Name",
        Pln = 1L,
        PlnName = "PlnName"
        // spln = xpln.ypln
      };
      nodeItemList.Add(nodeItemPln);

      TankItem nodeItemTnk = new TankItem
      {
        ImagePath = "avares://DynamicTabs/Assets/tnk.png",
        Code = "TnkCode",
        Name = "TnkName",
        // Pln = xtnk.ytnk.pln.value,
        // Tnk = xtnk.ytnk.tnk.value,
        // TnkName = xtnk.ytnk.tnk_name.value,
        // TnkType = xtnk.ytnk.tnk_type.ValuesTitles(),
        // // TnkTypeSelected = new KeyValuePair<string, string>(xtnk.ytnk.tnk_type.title,xtnk.ytnk.tnk_type.value),
        // // TnkTypeSelected = new KeyValuePair<string, string>("Локально",xtnk.ytnk.tnk_type.value),
        // TnkTypeSelectedIndex = xtnk.ytnk.tnk_type.Values().IndexOf(xtnk.ytnk.tnk_type.value),
        // TnkPosX = xtnk.ytnk.tnk_pos_x.value,
        // TnkPosY = xtnk.ytnk.tnk_pos_y.value,
        // TnkPosZ = xtnk.ytnk.tnk_pos_z.value,
        // stnk = xtnk.ytnk,
        // xtnk = xtnk
      };
      nodeItemPln.Nodes.Add(nodeItemTnk);

      NodesRoot = nodeItemList;

      DataProgress = false;

      Console.WriteLine("ExcaliburPlnLstAsync: end");

      return true;
    }
  }
}
