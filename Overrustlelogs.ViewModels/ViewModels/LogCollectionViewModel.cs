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
    public class LogCollectionViewModel : INotifyPropertyChanged {
        private readonly IApiLogs _apiLogs;
        private readonly IApiChannels _apiChannels;
        private IMessageModel _selectedMonth;
        public string User { get; set; }
        public ObservableCollection<IMessageModel> MonthLogs { get; set; }
        public ObservableCollection<string> Channels { get; set; }
        public LogCollectionViewModel(IApiLogs apiLogs, IApiChannels apiChannels) {
            _apiLogs = apiLogs;
            _apiChannels = apiChannels;
            SubmitCommand = new ActionCommand(async () => await GetMessages());
            NextMonthCommand = new ActionCommand(NextMonth);
            PrevMonthCommand = new ActionCommand(PrevMonth);
            Channels = new ObservableCollection<string>();
            GetChannel().ConfigureAwait(false);
        }

        public ICommand SubmitCommand { get; }

        public IMessageModel SelectedMonth {
            get { return _selectedMonth; }
            set {
                _selectedMonth = value;
                value.GetLogCommand.Execute(null);
            }
        }

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
            SelectedMonth = monthsList.FirstOrDefault(m => m.Month == SelectedMonth?.Month) ?? monthsList.FirstOrDefault();
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
            if (MonthLogs == null) {
                return;
            }
            if (MonthIndex > 0) {
                MonthIndex--;
                //SelectedMonth.GetLogCommand.Execute(null);
            }
        }
        public ICommand PrevMonthCommand { get; }
        private void PrevMonth() {
            if (MonthIndex < MonthLogs?.Count) {
                MonthIndex++;
                //SelectedMonth.GetLogCommand.Execute(null);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
