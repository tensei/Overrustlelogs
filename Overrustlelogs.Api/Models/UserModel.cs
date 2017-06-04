using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class UserModel : INotifyPropertyChanged, IUserModel {
        public string Name { get; }
        public string Url { get; }
        public bool Visibility { get; set; } = true;

        public UserModel(string name, string url) {
            Name = name;
            Url = url;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
