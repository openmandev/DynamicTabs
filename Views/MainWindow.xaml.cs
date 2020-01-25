using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Notifications;
using DynamicTabs.ViewModels;

namespace DynamicTabs.Views
{
    public class MainWindow : Window
    {
        private WindowNotificationManager _notificationArea;

        public MainWindow()
        {
            _notificationArea = new WindowNotificationManager(this)
            {
                Position = NotificationPosition.BottomRight,
                MaxItems = 4
            };

            DataContext = new MainWindowViewModel(_notificationArea);

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}