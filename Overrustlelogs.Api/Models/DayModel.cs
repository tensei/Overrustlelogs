using System.ComponentModel;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class DayModel : IDayModel, INotifyPropertyChanged {
        public DayModel(string name, string url, string apiUrl) {
            Name = name;
            Url = url;
            ApiUrl = apiUrl;
        }

        public string Name { get; }
        public string Url { get; }
        public string ApiUrl { get; }
        public bool Visibility { get; set; } = true;
        public IMessageModel Messages { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}