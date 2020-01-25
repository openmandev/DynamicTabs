using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.ApplicationLifetimes;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using DynamicTabs.Models;
using DynamicTabs.Views;
using ReactiveUI;

namespace DynamicTabs.ViewModels
{
  public class MainWindowViewModel : ViewModelBase
  {
    // public static string BaseUrlEcb = "http://localhost:17444/excalib/content";
    public static string BaseUrl = "http://localhost/aaa/aaacont";
    ViewModelBase content;
    private IManagedNotificationManager _notificationManager;
    // private CookieContainer _cookieContainer = new CookieContainer();
    // private HttpClient httpClient = CreateHttpClient();

    public MainWindowViewModel(IManagedNotificationManager notificationManager)
    {
      Console.WriteLine("Avalonia version: [" + typeof(Control).Assembly.GetName().Version + "]");
      long totalMemory = GC.GetTotalMemory(false);
      Console.WriteLine("totalMemory: [" + totalMemory + "]");

      // Excalibur.GetInstance().SetBaseAddress(BaseUrlEcb);
      // Console.WriteLine("Excalibur: [" + Excalibur.GetInstance().httpClient.BaseAddress + "]");

      _notificationManager = notificationManager;

      // httpClient.BaseAddress = new Uri(BaseUrl);

      Login();

      Console.WriteLine("MainWindowViewModel: ready");
    }

    public ViewModelBase Content
    {
      get => content;
      private set => this.RaiseAndSetIfChanged(ref content, value);
    }
    public IManagedNotificationManager NotificationManager
    {
      get { return _notificationManager; }
      set { this.RaiseAndSetIfChanged(ref _notificationManager, value); }
    }

    public MainViewModel mainViewModel { get; set; }

    public void Login()
    {
      mainViewModel = new MainViewModel(NotificationManager);
      Content = mainViewModel;
    }

    public void Quit()
    {
      (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }
  }
}
