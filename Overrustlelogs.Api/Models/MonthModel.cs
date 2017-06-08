using System.Collections.ObjectModel;
using System.ComponentModel;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class MonthModel : IMonthModel, INotifyPropertyChanged {
        public MonthModel(string name, string url, string apiUrl) {
            Name = name;
            Url = url;
            ApiUrl = apiUrl;
        }

        public string Name { get; set; }
        public string Url { get; set; }
        public string ApiUrl { get; set; }
        public bool Visibility { get; set; } = true;

        public ObservableCollection<IDayModel> Days { get; set; }
        public ObservableCollection<IUserModel> Users { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}