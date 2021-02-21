using Prism.Mvvm;
using Prism.Regions;
using PriTrigger.Views;

namespace PriTrigger.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Sample";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel(IRegionManager regMan)
        {
            regMan.RegisterViewWithRegion("ContentRegion", typeof(ViewContent));
            regMan.RegisterViewWithRegion("CommandRegion", typeof(ViewCommand));

        }
    }
}
