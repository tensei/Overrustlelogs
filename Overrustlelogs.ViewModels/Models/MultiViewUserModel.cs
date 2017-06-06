using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Interfaces;

namespace Overrustlelogs.ViewModels.Models {
    public class MultiViewUserModel : IMultiViewUserModel, INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private IMessageModel _selectedMonth;
        public string User { get; set; }
        public string Channel { get; set; }
        public ObservableCollection<IMessageModel> Months { get; }

        public IMessageModel SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                value.GetLogCommand.Execute(null);
            }
        }
        public MultiViewUserModel(string user, string channel, List<IMessageModel> months) {
            User = user;
            Channel = channel;
            Months = new ObservableCollection<IMessageModel>(months);
            SelectedMonth = months.FirstOrDefault();
        }
        
    }
}
