using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Interfaces;

namespace Overrustlelogs.ViewModels.Models {
    public class MultiViewUserModel : IMultiViewUserModel, INotifyPropertyChanged {
        [JsonIgnore]
        private IMessageModel _selectedMonth;

        public MultiViewUserModel(string user, string channel, List<IMessageModel> months) {
            User = user;
            Channel = channel;
            if (months == null) {
                return;
            }
            Months = new ObservableCollection<IMessageModel>(months);
            SelectedMonth = months.FirstOrDefault();
        }

        public string User { get; set; }
        public string Channel { get; set; }
        [JsonIgnore]
        public ObservableCollection<IMessageModel> Months { get; set; }

        [JsonIgnore]
        public IMessageModel SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                value.GetLogCommand.Execute(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}