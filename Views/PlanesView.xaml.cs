using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace DynamicTabs.Views
{
    public class PlanesView : UserControl
    {
        public PlanesView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}