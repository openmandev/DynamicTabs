using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DynamicTabs.Views
{
    public class MainView : UserControl
    {
        public MainView()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            // try
            // {
                AvaloniaXamlLoader.Load(this);
            // }
            // catch ( System.Exception e )
            // {
            //     Console.WriteLine(e.Message);
            // }
        }
    }
}
