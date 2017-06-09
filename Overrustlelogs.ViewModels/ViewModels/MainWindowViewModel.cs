using System;
using System.ComponentModel;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Factories;
using Overrustlelogs.ViewModels.Utils;
using Overrustlelogs.ViewModels.ViewModels.Directory;
using Overrustlelogs.ViewModels.ViewModels.Stalk;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class MainWindowViewModel : INotifyPropertyChanged {
        private readonly ViewModelFactory _viewModelFactory;
        private readonly Action<string> _snackbarMessage;
        private readonly CurrentState _currentState;

        public MainWindowViewModel(ViewModelFactory viewModelFactory, Action<string> snackbarMessage, CurrentState currentState) {
            SwitchViewCommand = new ActionCommand(i => SwitchViewTo((string) i));
            ForwardCommand = new ActionCommand(Forward);
            BackCommand = new ActionCommand(Back);
            _viewModelFactory = viewModelFactory;
            _snackbarMessage = snackbarMessage;
            _currentState = currentState;
            ChannelsDataContext = _viewModelFactory.CreateChannelsViewModel;
            LogsDataContext = _viewModelFactory.CreateStalkViewModel();
            MentionsDataContext = _viewModelFactory.CreateMentionsViewModel();
            _currentState.SwitchViewToMonth = ShowMonths;
            _currentState.SwitchViewToDays = ShowDays;
            _currentState.SwitchViewToUserlogs = ShowUserlogs;
        }

        public int ViewIndex { get; set; }
        public string Title { get; set; } = "Overrustle Logs";
        public string CurrentUrl { get; set; } = "https://overrustlelogs.net";

        public ChannelsViewModel ChannelsDataContext { get; set; }
        public MonthsViewModel MonthsDataContext { get; set; }
        public DaysViewModel DaysDataContext { get; set; }
        public StalkViewModel LogsDataContext { get; set; }
        public UserlogsViewModel UserlogsDataContext { get; set; }
        public MentionsViewModel MentionsDataContext { get; set; }

        public ICommand SwitchViewCommand { get; }

        public ICommand BackCommand { get; }
        public ICommand ForwardCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;


        public void Back() {
            var currentIndex = ViewIndex;
            switch (currentIndex) {
                case 1:
                    ViewIndex--;
                    ChangeTitle();
                    break;
                case 2:
                    ViewIndex--;
                    ChangeTitle(_currentState.Channel.Name);
                    break;
                case 3:
                    ViewIndex--;
                    ChangeTitle(_currentState.Channel.Name, _currentState.Month.Name);
                    break;
            }
        }

        private void Forward() {
            var currentIndex = ViewIndex;
            switch (currentIndex) {
                case 0:
                    if (_currentState.Channel == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(_currentState.Channel.Name);
                    break;
                case 1:
                    if (_currentState.Month == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(_currentState.Channel.Name, _currentState.Month.Name);
                    break;
                case 2:
                    if (_currentState.Month == null) {
                        break;
                    }
                    ViewIndex++;
                    ChangeTitle(_currentState.Channel.Name, _currentState.Month.Name + "/userlogs");
                    break;
            }
        }

        private void ShowMonths(IChannelModel channel = null) {
            if (channel != null) {
                _currentState.Channel = channel;
                _currentState.Month = null;
            }
            MonthsDataContext = _viewModelFactory.CreateMonthsViewModel(ChangeTitle);
            ViewIndex = 1;
        }

        private void ShowDays(IMonthModel month = null) {
            if (month != null) {
                _currentState.Month = month;
            }
            DaysDataContext = _viewModelFactory.CreateDaysViewModel(ChangeTitle);
            ViewIndex = 2;
        }

        private void ShowUserlogs(IMonthModel month = null) {
            if (month != null) {
                _currentState.Month = month;
            }
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