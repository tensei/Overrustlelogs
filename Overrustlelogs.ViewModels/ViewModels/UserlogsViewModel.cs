using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class UserlogsViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IApiUserlogs _apiUserlogs;
        private IUserModel _selectedUser;

        public ICommand RefreshUsersCommand { get; }
        public ObservableCollection<IUserModel> UsersList { get; set; }
        public ObservableCollection<IUserModel> _usersList { get; set; }

        public IUserModel SelectedUser {
            set {
                if (value == null) {
                    return;
                }
                OpenLog(value);
                SelectedUserIndex = -1;
            }
        }

        public ICommand FilterCommand { get; }
        public string FilterText{ get; set; }

        public int SelectedUserIndex { get; set; } = -1;

        public UserlogsViewModel(Action<string, string> changeTitle, IApiUserlogs apiUserlogs) {
            _apiUserlogs = apiUserlogs;
            RefreshUsersCommand = new ActionCommand(async () => await GetUsers());
            FilterCommand = new ActionCommand(Filter);
            changeTitle(CurrentState.Channel.Name, CurrentState.Month.Name+"/userlogs");
            if (CurrentState.Month.Users != null) {
                UsersList = CurrentState.Month.Users;
                return;
            }
            GetUsers().ConfigureAwait(false);
        }

        private async Task GetUsers() {
            var days = await _apiUserlogs.Get(CurrentState.Month);
            CurrentState.Month.Users = new ObservableCollection<IUserModel>(days);
            _usersList = CurrentState.Month.Users;
            UsersList = CurrentState.Month.Users;
        }

        private void OpenLog(IUserModel user) {
            try {
                Process.Start(user.Url);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }

        private void Filter() {
            var filtered = _usersList.Where(u => u.Name.ToLower().Contains(FilterText.ToLower()));
            UsersList = new ObservableCollection<IUserModel>(filtered);
        }
    }
}
