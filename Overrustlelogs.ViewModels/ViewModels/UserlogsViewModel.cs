using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class UserlogsViewModel : INotifyPropertyChanged {
        private readonly IApiUserlogs _apiUserlogs;


        public UserlogsViewModel(Action<string, string> changeTitle, IApiUserlogs apiUserlogs) {
            _apiUserlogs = apiUserlogs;
            RefreshUsersCommand = new ActionCommand(async () => await GetUsers());
            OpenUserlogCommand = new ActionCommand(u => OpenLog((UserModel) u));
            changeTitle(CurrentState.Channel.Name, CurrentState.Month.Name + "/userlogs");
            if (CurrentState.Month.Users != null) {
                UsersList = CurrentState.Month.Users;
                return;
            }
            GetUsers().ConfigureAwait(false);
        }

        public ICommand RefreshUsersCommand { get; }
        public ObservableCollection<IUserModel> UsersList { get; set; }
        private ObservableCollection<IUserModel> _usersList { get; set; }
        
        public ICommand OpenUserlogCommand { get; }
        public string FilterText { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetUsers() {
            var users = await _apiUserlogs.Get(CurrentState.Channel, CurrentState.Month);
            if (users == null) {
                return;
            }
            CurrentState.Month.Users = new ObservableCollection<IUserModel>(users);
            _usersList = CurrentState.Month.Users;
            UsersList = CurrentState.Month.Users;
        }

        private void OpenLog(IUserModel user) {
            try {
                Process.Start(user.Url);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        public void Filter() {
            var filtered = _usersList.Where(u => u.Name.ToLower().Contains(FilterText.ToLower()));
            UsersList = new ObservableCollection<IUserModel>(filtered);
        }
    }
}