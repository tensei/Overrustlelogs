using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class UserLogViewModel : INotifyPropertyChanged {
        private readonly IApiLogs _apiLogs;
        private readonly IApiChannels _apiChannels;
        public string User { get; set; }
        public ObservableCollection<IMessageModel> MonthLogs { get; set; }
        public ObservableCollection<string> Channels { get; set; }
        public UserLogViewModel(IApiLogs apiLogs, IApiChannels apiChannels) {
            _apiLogs = apiLogs;
            _apiChannels = apiChannels;
            SubmitCommand = new ActionCommand(async () => await GetMessages());
            NextMonthCommand = new ActionCommand(NextMonth);
            PrevMonthCommand = new ActionCommand(PrevMonth);
            Channels = new ObservableCollection<string>();
            GetChannel().ConfigureAwait(false);
        }

        public ICommand SubmitCommand { get; }
        public IMessageModel SelectedMonth { get; set; }
        public string SelectedChannel { get; set; }
        public Visibility ProgressbarVisibility { get; set; } = Visibility.Collapsed;

        public string Status { get; set; }

        public int MonthIndex { get; set; }

        private async Task GetMessages() {
            if (string.IsNullOrWhiteSpace(User)|| string.IsNullOrWhiteSpace(SelectedChannel)) {
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
            SelectedMonth = monthsList.FirstOrDefault();
        }

        private async Task GetChannel() {
            var ch = await _apiChannels.Get();
            ch.ForEach(c => Channels.Add(c.Name));
        }

        private void ChangeStatus(string text) {
            Status = text;
        }

        public ICommand NextMonthCommand { get; }
        private void NextMonth() {
            if (MonthIndex < MonthLogs?.Count) {
                MonthIndex++;
            }
        }
        public ICommand PrevMonthCommand { get; }
        private void PrevMonth() {
            if (MonthLogs == null) {
                return;
            }
            if (MonthIndex > 0) {
                MonthIndex--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
