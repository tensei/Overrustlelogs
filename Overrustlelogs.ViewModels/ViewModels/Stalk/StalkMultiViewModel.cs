using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Stalk {
    public class StalkMultiViewModel {
        private readonly IApiLogs _apiLogs;
        private readonly IApiChannels _apiChannels;
        private readonly CurrentState _currentState;

        public StalkMultiViewModel(IApiLogs apiLogs, IApiChannels apiChannels, CurrentState currentState) {
            _apiLogs = apiLogs;
            _apiChannels = apiChannels;
            _currentState = currentState;
            AddUserCommand = new ActionCommand(async () => await AddUser());
            RemoveUserCommand = new ActionCommand(u => RenoveUser((MultiViewUserModel)u));
            GetLogCommand = new ActionCommand(async l => {
                var user = (MultiViewUserModel) l;
                await GetLog(user.SelectedMonth, user.SearchText);
            });
            Users = new ObservableCollection<IMultiViewUserModel>();
            LoadUsers();
            if (_currentState.Channels == null) {
                Channels = new ObservableCollection<string>();
                GetChannel().ConfigureAwait(false);
                return;
            }
            Channels = new ObservableCollection<string>();
            _currentState.Channels.ForEach(c => Channels.Add(c.Name));

        }
        public string SelectedChannel { get; set; }
        public ObservableCollection<IMultiViewUserModel> Users { get; set; }
        public ObservableCollection<string> Channels { get; set; }
        public string User { get; set; }
        public ICommand RemoveUserCommand { get; }
        public ICommand AddUserCommand { get; }
        public ICommand GetLogCommand { get; }

        private async Task AddUser() {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(SelectedChannel)) {
                return;
            }
            if (SelectedChannel != "Destinygg") {
                User = User.ToLower();
            }
            var monthsList = await _apiLogs.Get(User, SelectedChannel);
            if (Users == null) {
                Users = new ObservableCollection<IMultiViewUserModel>();
            }
            Users.Add(new MultiViewUserModel(User, SelectedChannel, monthsList, GetLog));
            _currentState.SaveMultiViewUsers(Users.ToList());
        }

        private void RenoveUser(IMultiViewUserModel multiViewUser) {
            Users.Remove(multiViewUser);
            _currentState.SaveMultiViewUsers(Users.ToList());
        }

        private async void LoadUsers() {
            var users = await _currentState.LoadMultiViewUsers(GetLog);
            users?.ForEach(Users.Add);
        }
        private async Task GetChannel() {
            var ch = await _apiChannels.Get();
            ch.ForEach(c => Channels.Add(c.Name));
        }

        private async Task GetLog(IMessageModel messageModel, string searchText) {
            if (messageModel == null) {
                return;
            }
            messageModel.Text = string.Empty;
            messageModel.GetLogButtonVisibility = true;
            var text = await _apiLogs.GetLogString(messageModel.Url);
            if (text == null) {
                messageModel.Text = "Error try again";
                return;
            }
            if (string.IsNullOrWhiteSpace(searchText)) {
                messageModel.Text = text;
            }
            messageModel.UnEditedText = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            ParseLog(searchText, messageModel);
            messageModel.GetLogButtonVisibility = false;
        }

        public void ParseLog(string search, IMessageModel messageModel) {
            if (search == null || messageModel == null) {
                return;
            }
            // [2017-05-20 19:04:51 UTC] xxxx: xxxx
            var text = string.Empty;
            foreach (var s in messageModel.UnEditedText) {
                if (s.ToLower().Contains(search.ToLower())) text = text + $"{s}\n";
            }
            messageModel.Text = text;
        }
    }
}
