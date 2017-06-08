using System.ComponentModel;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class UserModel : INotifyPropertyChanged, IUserModel {
        public UserModel(string name, string url) {
            Name = name;
            Url = url;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Name { get; }
        public string Url { get; }
        public bool Visibility { get; set; } = true;
    }
}