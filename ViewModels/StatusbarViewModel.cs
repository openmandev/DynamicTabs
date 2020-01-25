using System.Reactive;
using ReactiveUI;
using DynamicTabs.Models;

namespace DynamicTabs.ViewModels
{
    public class StatusbarViewModel : ViewModelBase
    {
        public StatusbarViewModel()
        {
            Name = "aaa";
            Body = "QQQQQQ ";
        }

        public StatusbarViewModel(string name)
        {
            Name = name;
            Body = "QQQQQQ "+name;
        }

        public string Name { get; set; }
        public string Body { get; set; }
    }
}
