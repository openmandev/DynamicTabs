using System;
using Avalonia.Threading;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Reactive;
using Avalonia.Controls;
using ReactiveUI;
using DynamicTabs.Models;
using DynamicTabs.Views;

namespace DynamicTabs.ViewModels
{
  public class MainViewModel : ViewModelBase
  {
    private IManagedNotificationManager _notificationManager;
    public NodeItem selectedNode;
    public ViewModelBase selectedTab;
    public int selectedTabIndex;
    public ObservableCollection<ToolbarItem> MenuItems { get; set; }
    private ObservableCollection<ViewModelBase> tabsList = new ObservableCollection<ViewModelBase>();
    public ObservableCollection<ViewModelBase> TabsList { get; set; }
    public string Username { get; set; }
    public ReactiveCommand<Unit, Unit> PlaneList { get; }
    public ReactiveCommand<Unit, Unit> NodeList { get; }
    public ReactiveCommand<Unit, Unit> Test { get; }
    public ReactiveCommand<object, Unit> CloseTab { get; }
    public ObservableCollection<NodeItem> NodesRoot { get; set; }
    public NodeItem SelectedNode
    {
      get => selectedNode;
      set => this.RaiseAndSetIfChanged(ref selectedNode, value);
    }
    public ViewModelBase SelectedTab
    {
      get => selectedTab;
      set => this.RaiseAndSetIfChanged(ref selectedTab, value);
    }
    public int SelectedTabIndex
    {
      get => selectedTabIndex;
      set => this.RaiseAndSetIfChanged(ref selectedTabIndex, value);
    }

    public MainViewModel(IManagedNotificationManager notificationManager)
    {
      _notificationManager = notificationManager;

      PlaneList = ReactiveCommand.Create<Unit, Unit>(_ => PlaneListOpen());
      Test = ReactiveCommand.Create<Unit, Unit>(_ => CalibrateOpen(1));
      CloseTab = ReactiveCommand.Create<object, Unit>(commandParameter => CloseUsrTab(commandParameter));

      MenuItems = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/pln.png", ToolTip="TabType1", Command=PlaneList },
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/clb.png", ToolTip="Direct TabType2", Command=Test }
      };

      TabsList = tabsList;

      Console.WriteLine("MainViewModel: ready");
    }

    public void MainNotify(string title, string message, NotificationType notificationType)
    {
      Console.WriteLine($"MainNotify: {title} - {message}");
      _notificationManager.Show(new Avalonia.Controls.Notifications.Notification(title, message, notificationType));
    }

    public Unit PlaneListOpen()
    {
      UserTabItem userTab = new UserTabItem() { TabImage = "Assets/pln.png", TabTitle = "TabType1", TabType = UserTabItem.UserTabType.PLANES };
      PlanesViewModel planesViewModel = new PlanesViewModel(userTab);
      // planesViewModel.Notify += MainNotify;
      planesViewModel.CalibrateTabOpen += CalibrateOpen;
      TabsList.Add(planesViewModel);
      SelectedTab = planesViewModel;
      // SelectedTabIndex = TabsList.IndexOf(planesViewModel);
      return Unit.Default;
    }

    public Unit CalibrateOpen(int tnk)
    {
      UserTabItem userTab = new UserTabItem() { TabImage = "Assets/clb.png", TabTitle = "TabType2", TabType = UserTabItem.UserTabType.CALIBRATES };
      CalibrateViewModel calibrateViewModel = new CalibrateViewModel(userTab, tnk);
      // calibrateViewModel.Notify += MainNotify;
      TabsList.Add(calibrateViewModel);
      // Dispatcher.UIThread.Post((Action)(() => SelectedTab = calibrateViewModel));
      SelectedTab = calibrateViewModel;
      // SelectedTabIndex = TabsList.IndexOf(calibrateViewModel);
      return Unit.Default;
    }

    public Unit CloseUsrTab(object commandParameter)
    {
      TabsList.Remove((ViewModelBase)commandParameter);
      return Unit.Default;
    }
  }
}
