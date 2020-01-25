using System;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicTabs.ViewModels;
using DynamicTabs.Views;

namespace DynamicTabs
{
  public class App : Application
  {
    public override void Initialize()
    {
      AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
      if ( ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop )
      {
        desktop.MainWindow = new MainWindow();
        // desktop.MainWindow.DataContext = new MainWindowViewModel();
      }
      else if ( ApplicationLifetime is ISingleViewApplicationLifetime singleView )
      {
        singleView.MainView = new MainView();
        Console.WriteLine("ISingleViewApplicationLifetime");
      }

      base.OnFrameworkInitializationCompleted();
    }
  }
}