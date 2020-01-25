using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DynamicTabs.Views
{
    public class CalibrateView : UserControl
    {
        public CalibrateView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
