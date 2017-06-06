using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class MonthModel : IMonthModel, INotifyPropertyChanged {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Visibility { get; set; } = true;

        public ObservableCollection<IDayModel> Days { get; set; }
        public ObservableCollection<IUserModel> Users { get; set; }

        public MonthModel(string name, string url) {
            Name = name;
            Url = url;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
