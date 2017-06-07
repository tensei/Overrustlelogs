using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class DayModel : IDayModel, INotifyPropertyChanged {
        public string Name { get; }
        public string Url { get; }
        public string ApiUrl { get; }
        public bool Visibility { get; set; } = true;
        public IMessageModel Messages { get; set; }

        public DayModel(string name, string url, string apiUrl) {
            Name = name;
            Url = url;
            ApiUrl = apiUrl;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
