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
            ForwardCommand = new ActionCommand(Forward);
            BackCommand = new ActionCommand(Back);
            _viewModelFactory = viewModelFactory;
            ChannelsDataContext = _viewModelFactory.CreateChannelsViewModel;
            LogsDataContext = _viewModelFactory.CreateLogsViewModel();
            MentionsDataContext = _viewModelFactory.CreateMentionsViewModel();
            CurrentState.SwitchViewToMonth = ShowMonths;
            CurrentState.SwitchViewToDays = ShowDays;
            CurrentState.SwitchViewToUserlogs = ShowUserlogs;
        }
        public int ViewIndex { get; set; }
        public string Title { get; set; } = "Overrustle Logs";
        public string CurrentUrl { get; set; } = "https://overrustlelogs.net";

        public ChannelsViewModel ChannelsDataContext { get; set; }
        public MonthsViewModel MonthsDataContext { get; set; }
        public DaysViewModel DaysDataContext { get; set; }
        public LogCollectionViewModel LogsDataContext { get; set; }
        public UserlogsViewModel UserlogsDataContext { get; set; }
        public MentionsViewModel MentionsDataContext { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ICommand BackCommand { get; }
        public ICommand ForwardCommand { get; }



        private void Back() {
            var currentIndex = ViewIndex;
            switch (currentIndex) {
                case 1:
                    ViewIndex--;
                    ChangeTitle();
                    break;
                case 2:
                    ViewIndex--;
                    ChangeTitle(CurrentState.Channel.Name);
                    break;
                case 3:
                    ViewIndex--;
                    ChangeTitle(CurrentState.Channel.Name, CurrentState.Month.Name);
                    break;
            }
        }
        private void Forward() {
            var currentIndex = ViewIndex;
            switch (currentIndex) {
                case 0:
                    if (CurrentState.Channel == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(CurrentState.Channel.Name);
                    break;
                case 1:
                    if (CurrentState.Month == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(CurrentState.Channel.Name, CurrentState.Month.Name);
                    break;
                case 2:
                    if (CurrentState.Month == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(CurrentState.Channel.Name, CurrentState.Month.Name + "/userlogs");
                    break;
            }
        }

        private void ShowMonths(IChannelModel channel) {
            CurrentState.Channel = channel;
            CurrentState.Month = null;
            MonthsDataContext = _viewModelFactory.CreateMonthsViewModel(ChangeTitle);
            ViewIndex = 1;
        }
        private void ShowDays(IMonthModel month) {
            CurrentState.Month = month;
            DaysDataContext = _viewModelFactory.CreateDaysViewModel(ChangeTitle);
            ViewIndex = 2;
        }
        private void ShowUserlogs(IMonthModel month) {
            CurrentState.Month = month;
            UserlogsDataContext = _viewModelFactory.CreateUserlogsViewModel(ChangeTitle);
            ViewIndex = 3;
        }

        private void SwitchViewTo(string index) {
            var ind = int.Parse(index);
            ViewIndex = ind;
            Title = "Overrustle Logs";
            if (ind == 0) {
                ChangeTitle();
            }
        }

        private void ChangeTitle(string channel = null, string month = null) {
            const string urlBase = "https://overrustlelogs.net";
            if (month != null) {
                CurrentUrl = $"{urlBase}/{channel} chatlog/{month}/";
                return;
            }
            if (channel != null) {
                CurrentUrl = $"{urlBase}/{channel} chatlog/";
                return;
            }
            CurrentUrl = urlBase;
        }
    }
}
