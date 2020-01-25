using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicTabs.Models;
using DynamicTabs.Views;

namespace DynamicTabs.ViewModels
{
  public class CalibrateViewModel : ViewModelBase
  {
    private int tnkCurrent;
    private string errorMess;
    private bool calibLoading = false;
    private bool levelLoading = false;
    public ReactiveCommand<Unit, bool> CalibListCmd { get; }
    public ReactiveCommand<Unit, bool> CalibrateCreCmd { get; }
    public ReactiveCommand<Unit, bool> CalibrateUpdCmd { get; }
    public ObservableCollection<ToolbarItem> ButtonListLeft { get; set; }
    public ObservableCollection<ToolbarItem> ButtonListRight { get; set; }
    private ObservableCollection<ToolbarItem> menuItemsClb;
    private ObservableCollection<ToolbarItem> menuItemsLvl;
    // private ObservableCollection<ecbi.s_clb> calibList;
    private ObservableCollection<LevelItem> levelList;
    // public ecbi.s_clb selectedCalib;
    public UserTabItem UserTabItem { get; set; }
    public ObservableCollection<ToolbarItem> MenuItemsClb
    {
      get => menuItemsClb;
      set => this.RaiseAndSetIfChanged(ref menuItemsClb, value);
    }
    public ObservableCollection<ToolbarItem> MenuItemsLvl
    {
      get => menuItemsLvl;
      set => this.RaiseAndSetIfChanged(ref menuItemsLvl, value);
    }
    public string ErrorMess
    {
      get => errorMess;
      set => this.RaiseAndSetIfChanged(ref errorMess, value);
    }
    public bool CalibLoading
    {
      get => calibLoading;
      set => this.RaiseAndSetIfChanged(ref calibLoading, value);
    }
    public bool LevelLoading
    {
      get => levelLoading;
      set => this.RaiseAndSetIfChanged(ref levelLoading, value);
    }
    // public ecbi.s_clb SelectedCalib
    // {
    //   get => selectedCalib;
    //   // set => this.RaiseAndSetIfChanged(ref selectedCalib, value);
    //   set
    //   {
    //     this.RaiseAndSetIfChanged(ref selectedCalib, value);
  
    //     Console.WriteLine($"SelectedCalib: {selectedCalib}");
    //   }
    // }

    // public ObservableCollection<ecbi.s_clb> CalibList
    // {
    //   get => calibList;
    //   set => this.RaiseAndSetIfChanged(ref calibList, value);
    // }

    public ObservableCollection<LevelItem> LevelList
    {
      get => levelList;
      set => this.RaiseAndSetIfChanged(ref levelList, value);
    }
    public LevelItem LevelItemTitles { get; } //!!!!!!!!!!!

    public CalibrateViewModel()
    {
      // LevelItemTitles = new LevelItem(); //!!!!!!!!!!!
      // LevelItemTitles.slvl = new ecbi.s_lvl();
      // LevelItemTitles.lvl_numb = new ecb.t_lvl_numb();
      // LevelItemTitles.lvl_height = new ecb.t_lvl_height();
      // LevelItemTitles.lvl_name = new ecb.t_lvl_name();
      // LevelItemTitles.lvl_m = new ecb.t_mass();
      // LevelItemTitles.lvl_x = new ecb.t_pos_x();
      // LevelItemTitles.lvl_y = new ecb.t_pos_y();
      // LevelItemTitles.lvl_z = new ecb.t_pos_z();
      // LevelItemTitles.lvl_mtype = new ecb.t_mtype();
      // LevelItemTitles.lvl_ix = new ecb.t_ix();
      // LevelItemTitles.lvl_iy = new ecb.t_iy();
      // LevelItemTitles.lvl_iz = new ecb.t_iz();
      // LevelItemTitles.lvl_ixy = new ecb.t_ixy();
      // LevelItemTitles.lvl_iyz = new ecb.t_iyz();
      // LevelItemTitles.lvl_ixz = new ecb.t_ixz();

      CalibListCmd = ReactiveCommand.CreateFromTask<Unit, bool>(_ => ExcaliburTnkClbLstAsync());

      // CalibrateCreCmd = ReactiveCommand.CreateFromTask<Unit, bool>(_ => CalibrateDialog(false));
      // CalibrateCreCmd.Subscribe(yes =>
      // {
      //   Console.WriteLine("CalibrateViewModel: CalibrateCreCmd Subscribed: begin");
      //   // Refresh(yes);
      //   Console.WriteLine("CalibrateViewModel: CalibrateCreCmd Subscribed: end");
      // });

      // CalibrateUpdCmd = ReactiveCommand.CreateFromTask<Unit, bool>(_ => CalibrateDialog(true));

      ButtonListLeft = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/clb_cre.png", ToolTip="Создать тарировку...", Command=CalibrateCreCmd }
      };

      ButtonListRight = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", ToolTip="Обновить", Command=CalibListCmd }
      };

      menuItemsClb = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/clb_upd.png", ToolTip="Редактировать...", Command=CalibrateUpdCmd },
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/clb_del.png", ToolTip="Удалить", Command=CalibrateUpdCmd }, //!!!!!!!!!!!!!!
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", ToolTip="Обновить", Command=CalibListCmd }
      };

      menuItemsLvl = new ObservableCollection<ToolbarItem>
      {
        new ToolbarItem { ImagePath="avares://DynamicTabs/Assets/refresh.png", ToolTip="Обновить", Command=CalibListCmd }
      };

      MenuItemsClb = menuItemsClb;
      MenuItemsLvl = menuItemsLvl;
    }

    public CalibrateViewModel(UserTabItem userTabItem, int tnk) : this()
    {
      tnkCurrent = tnk;
      this.UserTabItem = userTabItem;

      // var refreshEnabled = taskDesks.WhenAny(taskDesks => taskDesks.IsCompleted == true,taskDesks.IsCompleted);

      // this.WhenAnyValue(x => x.SelectedCalib).Subscribe
      // (
      //   onNext : _ => OnSelectedCalib()
      // );

      Task task = Task.Run(() => ExcaliburTnkClbLstAsync());

      Console.WriteLine("CalibrateViewModel: ready");
    }

    // private void OnSelectedCalib()
    // {
    //   Console.WriteLine($"OnSelectedCalib: {selectedCalib}");

    //   if ( selectedCalib != null )
    //   {
    //     Task task = Task.Run(() => ExcaliburLvlLstAsync(((ecbi.s_clb)selectedCalib).clb));
    //   }
    // }

    private async Task<bool> ExcaliburTnkClbLstAsync()
    {
      Console.WriteLine("ExcaliburTnkClbLstAsync: begin");

      CalibLoading = true;

      await Task.Delay(500);

      // ObservableCollection<CalibItem> calibItemList = new ObservableCollection<CalibItem>();
      // ObservableCollection<ecbi.s_clb> calibItemList = new ObservableCollection<ecbi.s_clb>();

      // qtnk.clb xclb = dtnk.clb_mem(0);
      // while ( xclb != null )
      // {
      //   Console.WriteLine("ExcaliburTnkClbLstAsync: clb_name=[{0}]",xclb.yclb.clb_name.value);

      //   // CalibItem calibItem = new CalibItem
      //   // {
      //   //   Tnk = xclb.yclb.tnk.value,
      //   //   Clb = xclb.yclb.clb.value,
      //   //   ClbName = xclb.yclb.clb_name.value,
      //   //   ClbDens = xclb.yclb.clb_dens.value,
      //   //   ClbStep = xclb.yclb.clb_step.value,
      //   //   ClbAlpha = xclb.yclb.clb_alpha.value,
      //   //   ClbGamma = xclb.yclb.clb_gamma.value,
      //   //   clb = xclb.yclb.clb,
      //   //   sclb = xclb.yclb
      //   // };
      //   // calibItemList.Add(calibItem);
      //   calibItemList.Add(xclb.yclb);

      //   xclb = dtnk.clb_next(xclb);
      // }

      // CalibList = calibItemList;

      Console.WriteLine("ExcaliburTnkClbLstAsync: end");

      CalibLoading = false;

      return true;
    }

    // private async Task<bool> ExcaliburLvlLstAsync(int clb)
    // {
    //   Console.WriteLine("ExcaliburLvlLstAsync: begin");

    //   LevelLoading = true;

    //   qtnk.qtnk dtnk = null;
    //   ecb.t_mess mess = null;// = new ecb.t_mess();
    //   bool yes;

    //   wreqecb.lvl_lst_async lvl_lst_async = new wreqecb.lvl_lst_async();

    //   yes = await lvl_lst_async.RequestAsync(Excalibur.GetInstance().httpClient,clb);

    //   if ( !yes )
    //   {
    //     Console.WriteLine("ExcaliburLvlLstAsync: ErrorMessage=[{0}]",lvl_lst_async.ErrorMessage);
    //     ErrorMess = lvl_lst_async.ErrorMessage;
    //   }

    //   dtnk = lvl_lst_async.dtnk;
    //   mess = lvl_lst_async.mess;

    //   if ( mess.value != "" )
    //   {
    //     Console.WriteLine("ExcaliburLvlLstAsync: mess=[{0}]",mess);
    //     yes = false;
    //   }

    //   ObservableCollection<LevelItem> LevelItemList = new ObservableCollection<LevelItem>();

    //   qtnk.lvl xlvl = dtnk.lvl_mem(0);
    //   while ( xlvl != null )
    //   {
    //     Console.WriteLine("ExcaliburLvlLstAsync: lvl_name=[{0}]",xlvl.ylvl.lvl_name.value);

    //     LevelItem levelItem = new LevelItem
    //     {
    //       clb = xlvl.ylvl.clb,
    //       lvl = xlvl.ylvl.lvl,
    //       lvl_numb = xlvl.ylvl.lvl_numb,
    //       lvl_height = xlvl.ylvl.lvl_height,
    //       lvl_name = xlvl.ylvl.lvl_name,
    //       lvl_m = xlvl.ylvl.lvl_m,
    //       lvl_x = xlvl.ylvl.lvl_x,
    //       lvl_y = xlvl.ylvl.lvl_y,
    //       lvl_z = xlvl.ylvl.lvl_z,
    //       lvl_mtype = xlvl.ylvl.lvl_mtype,
    //       lvl_ix = xlvl.ylvl.lvl_ix,
    //       lvl_iy = xlvl.ylvl.lvl_iy,
    //       lvl_iz = xlvl.ylvl.lvl_iz,
    //       lvl_ixy = xlvl.ylvl.lvl_ixy,
    //       lvl_iyz = xlvl.ylvl.lvl_iyz,
    //       lvl_ixz = xlvl.ylvl.lvl_ixz,
    //       // Desc = xlvl.ylvl.lvl.ToString(),
    //       slvl = xlvl.ylvl
    //     };
    //     LevelItemList.Add(levelItem);

    //     xlvl = dtnk.lvl_next(xlvl);
    //   }

    //   LevelList = LevelItemList;

    //   Console.WriteLine("ExcaliburLvlLstAsync: end");

    //   LevelLoading = false;

    //   return yes;
    // }
  }
}
