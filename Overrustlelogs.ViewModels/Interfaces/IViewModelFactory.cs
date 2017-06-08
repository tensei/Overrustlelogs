using System;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.ViewModels.Interfaces {
    public interface IViewModelFactory {
        ChannelsViewModel CreateChannelsViewModel { get; }
        MainWindowViewModel CreateMainWindowViewModel { get; }

        DaysViewModel CreateDaysViewModel(Action<string, string> changeTitle);
        LogCollectionViewModel CreateLogsViewModel();
        MentionsViewModel CreateMentionsViewModel();
        MonthsViewModel CreateMonthsViewModel(Action<string, string> changeTitle);
        UserlogsViewModel CreateUserlogsViewModel(Action<string, string> changeTitle);
    }
}