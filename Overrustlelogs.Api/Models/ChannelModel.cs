using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class ChannelModel : IChannelModel, INotifyPropertyChanged {
        public string Name { get; set; }
        public string Url { get; set; }
        public bool Visibility { get; set; } = true;
        public ObservableCollection<IMonthModel> Months { get; set; }

        public ChannelModel(string name, string url) {
            Name = name;
            Url = url;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
