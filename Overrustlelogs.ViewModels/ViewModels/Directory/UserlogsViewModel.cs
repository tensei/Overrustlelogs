using System;
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


        public UserlogsViewModel(Action<string, string> changeTitle, IApiUserlogs apiUserlogs, CurrentState currentState) {
            _apiUserlogs = apiUserlogs;
            _currentState = currentState;
            RefreshUsersCommand = new ActionCommand(async () => await GetUsers());
            OpenUserlogCommand = new ActionCommand(u => OpenLog((UserModel) u));
            changeTitle(_currentState.Channel.Name, _currentState.Month.Name + "/userlogs");
            if (_currentState.Month.Users != null) {
                UsersList = _currentState.Month.Users;
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
            var users = await _apiUserlogs.Get(_currentState.Channel, _currentState.Month);
            if (users == null) {
                return;
            }
            _currentState.Month.Users = new ObservableCollection<IUserModel>(users);
            _usersList = _currentState.Month.Users;
            UsersList = _currentState.Month.Users;
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