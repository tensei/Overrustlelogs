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
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Stalk {
    public class StalkSingleViewModel : INotifyPropertyChanged {
        private readonly IApiChannels _apiChannels;
        private readonly CurrentState _currentState;
        private readonly IApiLogs _apiLogs;
        private IMessageModel _selectedMonth;
        public StalkSingleViewModel(IApiLogs apiLogs, IApiChannels apiChannels, CurrentState currentState) {
            _apiLogs = apiLogs;
            _apiChannels = apiChannels;
            _currentState = currentState;
            SubmitCommand = new ActionCommand(async () => await GetMessages());
            NextMonthCommand = new ActionCommand(NextMonth);
            PrevMonthCommand = new ActionCommand(PrevMonth);
            GetLogCommand = new ActionCommand(async l => await GetLog((MessageModel)l));
            if (_currentState.Channels == null) {
                Channels = new ObservableCollection<string>();
                GetChannel().ConfigureAwait(false);
                return;
            }
            Channels = new ObservableCollection<string>();
            _currentState.Channels.ForEach(c => Channels.Add(c.Name));

        }

        public string User { get; set; }
        public ObservableCollection<IMessageModel> MonthLogs { get; set; }
        public ObservableCollection<string> Channels { get; set; }
        public ICommand SubmitCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand PrevMonthCommand { get; }
        public ICommand GetLogCommand { get; }
        public int MonthIndex { get; set; }
        public string SelectedChannel { get; set; }
        public Visibility ProgressbarVisibility { get; set; } = Visibility.Collapsed;


        public IMessageModel SelectedMonth {
            get => _selectedMonth;
            set {
                _selectedMonth = value;
                GetLog(value).ConfigureAwait(false);
            }
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
        private async Task GetChannel() {
            var ch = await _apiChannels.Get();
            ch.ForEach(c => Channels.Add(c.Name));
        }

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

        private async Task GetLog(IMessageModel messageModel) {
            await Task.Run(async () => {
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
                messageModel.Text = text;
                messageModel.UnEditedText = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
                messageModel.GetLogButtonVisibility = false;
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
