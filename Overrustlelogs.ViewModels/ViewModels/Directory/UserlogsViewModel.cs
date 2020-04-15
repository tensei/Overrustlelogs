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
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Directory {
    public class UserlogsViewModel : INotifyPropertyChanged {
        private readonly IApiUserlogs _apiUserlogs;
        private readonly CurrentState _currentState;


        public UserlogsViewModel(IApiUserlogs apiUserlogs, CurrentState currentState) {
            _apiUserlogs = apiUserlogs;
            _currentState = currentState;
            RefreshUsersCommand = new ActionCommand(async () => await GetUsers());
            OpenUserlogCommand = new ActionCommand(u => OpenLog((UserModel) u));
            UsersList = new ObservableCollection<IUserModel>();
            if (_currentState.Month.Users != null) {
                _currentState.Month.Users.ForEach(UsersList.Add);
                return;
            }
            GetUsers().ConfigureAwait(false);
        }

        public ICommand RefreshUsersCommand { get; }
        public ObservableCollection<IUserModel> UsersList { get; set; }
        private List<IUserModel> _usersList { get; set; }
        
        public ICommand OpenUserlogCommand { get; }
        public string FilterText { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetUsers() {
            var users = await _apiUserlogs.Get(_currentState.Channel, _currentState.Month);
            if (users == null) {
                return;
            }
            _currentState.Month.Users = new List<IUserModel>(users);
            _usersList = _currentState.Month.Users;
            users.ForEach(UsersList.Add);
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
            var filtered = _usersList.Where(u => u.Name.ToLower().Contains(FilterText.ToLower())).ToList();
            UsersList.Clear();
            filtered.ForEach(UsersList.Add);
        }
    }
}