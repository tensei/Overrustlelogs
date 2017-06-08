using System;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.ViewModels.Factories {
    public class ViewModelFactory : IViewModelFactory {
        private readonly IApiFactory _apiFactory;
        private readonly Action<string> _snackbarMessageQueue;

        public ViewModelFactory(IApiFactory apiFactory, Action<string> snackbarMessageQueue) {
            _apiFactory = apiFactory;
            _snackbarMessageQueue = snackbarMessageQueue;
        }

        public MainWindowViewModel CreateMainWindowViewModel => new MainWindowViewModel(this, _snackbarMessageQueue);
        public ChannelsViewModel CreateChannelsViewModel => new ChannelsViewModel(_apiFactory.GetApiChannels());

        public MonthsViewModel CreateMonthsViewModel(Action<string, string> changeTitle) {
            return new MonthsViewModel(_apiFactory.GetApiMonths(), changeTitle);
        }

        public DaysViewModel CreateDaysViewModel(Action<string, string> changeTitle) {
            return new DaysViewModel(changeTitle, _apiFactory.GetApiDayss());
        }

        public LogCollectionViewModel CreateLogsViewModel() {
            return new LogCollectionViewModel(_apiFactory.GetApiLogss(), _apiFactory.GetApiChannels());
        }

        public UserlogsViewModel CreateUserlogsViewModel(Action<string, string> changeTitle) {
            return new UserlogsViewModel(changeTitle, _apiFactory.GetApiUserlogs());
        }

        public MentionsViewModel CreateMentionsViewModel() {
            return new MentionsViewModel(_apiFactory.GetApiChannels(), _apiFactory.GetApiMentions());
        }
    }
}