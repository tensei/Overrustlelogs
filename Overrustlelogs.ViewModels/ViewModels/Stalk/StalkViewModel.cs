using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Stalk {
    public class StalkViewModel : INotifyPropertyChanged {
        private readonly IViewModelFactory _viewModelFactory;

        public StalkViewModel(IViewModelFactory viewModelFactory) {
            _viewModelFactory = viewModelFactory;
            SingleDataContext = _viewModelFactory.CreateStalkSingleViewModel();
            MultiDataContext = _viewModelFactory.CreateStalkMultiViewModel();
        }
        public ObservableCollection<IMultiViewUserModel> Users { get; set; }
        
        
        public int ViewIndex { get; set; }
        

        public StalkSingleViewModel SingleDataContext { get; }
        public StalkMultiViewModel MultiDataContext { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        
        
    }
}