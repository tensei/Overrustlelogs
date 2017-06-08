using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class LogCollectionViewModel : INotifyPropertyChanged {
        private readonly IApiChannels _apiChannels;
        private readonly IApiLogs _apiLogs;
        private IMessageModel _selectedMonth;

        public LogCollectionViewModel(IApiLogs apiLogs, IApiChannels apiChannels) {
            _apiLogs = apiLogs;
            _apiChannels = apiChannels;
            SubmitCommand = new ActionCommand(async () => await GetMessages());
            NextMonthCommand = new ActionCommand(NextMonth);
            PrevMonthCommand = new ActionCommand(PrevMonth);
            AddUserCommand = new Api.ActionCommand(async () => await AddUser());
            RemoveUserCommand = new Api.ActionCommand(u => RenoveUser((MultiViewUserModel) u));
            if (CurrentState.Channels == null) {
                Channels = new ObservableCollection<string>();
                GetChannel().ConfigureAwait(false);
                return;
            }
            Channels = new ObservableCollection<string>();
            Users = new ObservableCollection<IMultiViewUserModel>();
            CurrentState.Channels.ForEach(c => Channels.Add(c.Name));
        }

        public string User { get; set; }
        public ObservableCollection<IMessageModel> MonthLogs { get; set; }
        public ObservableCollection<IMultiViewUserModel> Users { get; set; }
        public ObservableCollection<string> Channels { get; set; }

        public ICommand SubmitCommand { get; }
        public ICommand RemoveUserCommand { get; }

        public IMessageModel SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                value.GetLogCommand.Execute(null);
            }
        }

        public string SelectedChannel { get; set; }
        public Visibility ProgressbarVisibility { get; set; } = Visibility.Collapsed;

        public string Status { get; set; }

        public int MonthIndex { get; set; }
        public int ViewIndex { get; set; }

        public ICommand NextMonthCommand { get; }
        public ICommand PrevMonthCommand { get; }
        public ICommand AddUserCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetMessages() {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(SelectedChannel)) {
                return;
            }
            MonthLogs = new ObservableCollection<IMessageModel>();
            if (SelectedChannel != "Destinygg") {
                User = User.ToLower();
            }
            ProgressbarVisibility = Visibility.Visible;
            var monthsList = await _apiLogs.Get(User, SelectedChannel);
            ProgressbarVisibility = Visibility.Collapsed;
            monthsList.ForEach(MonthLogs.Add);
            SelectedMonth = monthsList.FirstOrDefault(m => m.Month == SelectedMonth?.Month) ??
                            monthsList.FirstOrDefault();
        }

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
            Users.Add(new MultiViewUserModel(User, SelectedChannel, monthsList));
        }

        private async Task GetChannel() {
            var ch = await _apiChannels.Get();
            ch.ForEach(c => Channels.Add(c.Name));
        }

        private void ChangeStatus(string text) {
            Status = text;
        }

        private void NextMonth() {
            if (MonthLogs == null) {
                return;
            }
            if (MonthIndex > 0) {
                MonthIndex--;
            }
        }


        private void PrevMonth() {
            if (MonthIndex < MonthLogs?.Count) {
                MonthIndex++;
            }
        }

        private void RenoveUser(IMultiViewUserModel multiViewUser) {
            Users.Remove(multiViewUser);
        }
    }
}