using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class MainWindowViewModel : INotifyPropertyChanged {
        private readonly ViewModelFactory _viewModelFactory;
        public event PropertyChangedEventHandler PropertyChanged;
        public MainWindowViewModel(ViewModelFactory viewModelFactory) {
            SwitchViewCommand = new ActionCommand(i => SwitchViewTo((string) i));
            _viewModelFactory = viewModelFactory;
            ChannelsDataContext = _viewModelFactory.CreateChannelsViewModel;
            LogsDataContext = _viewModelFactory.CreateLogsViewModel();
            CurrentState.SwitchViewToMonth = ShowMonths;
            CurrentState.SwitchViewToDays = ShowDays;
        }
        public int ViewIndex { get; set; }
        public string Title { get; set; } = "Overrustle Logs";

        public ChannelsViewModel ChannelsDataContext { get; set; }
        public MonthsViewModel MonthsDataContext { get; set; }
        public DaysViewModel DaysDataContext { get; set; }
        public UserLogViewModel LogsDataContext { get; set; }

        public ICommand SwitchViewCommand { get; }
        private void ShowMonths(IChannelModel channel) {
            CurrentState.Channel = channel;
            MonthsDataContext = _viewModelFactory.CreateMonthsViewModel(ChangeTitle);
            ViewIndex = 1;
        }
        private void ShowDays(IMonthModel month) {
            CurrentState.Month = month;
            DaysDataContext = _viewModelFactory.CreateDaysViewModel(ChangeTitle);
            ViewIndex = 2;
        }

        private void SwitchViewTo(string index) {
            var ind = int.Parse(index);
            ViewIndex = ind;
            Title = "Overrustle Logs";
        }

        private void ChangeTitle(string channel, string month = null, string day = null, string user = null) {
            const string title = "Overrustle Logs";
            if (user != null) {
                Title = $"{title} - /{channel} chatlog/{month}/userlogs/{user}/";
                return;
            }
            if (day != null) {
                Title = $"{title} - /{channel} chatlog/{month}/{day}/";
                return;
            }
            if (month != null) {
                Title = $"{title} - /{channel} chatlog/{month}/";
                return;
            }
            if (channel != null) {
                Title = $"{title} - /{channel} chatlog/";
            }
        }
    }
}
