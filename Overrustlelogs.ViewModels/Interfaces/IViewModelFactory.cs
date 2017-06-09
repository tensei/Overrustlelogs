using System;
using Overrustlelogs.ViewModels.ViewModels;
using Overrustlelogs.ViewModels.ViewModels.Directory;
using Overrustlelogs.ViewModels.ViewModels.Stalk;

namespace Overrustlelogs.ViewModels.Interfaces {
    public interface IViewModelFactory {
        ChannelsViewModel CreateChannelsViewModel { get; }
        MainWindowViewModel CreateMainWindowViewModel { get; }

        DaysViewModel CreateDaysViewModel(Action<string, string> changeTitle);
        StalkViewModel CreateStalkViewModel();
        StalkMultiViewModel CreateStalkMultiViewModel();
        StalkSingleViewModel CreateStalkSingleViewModel();
        MentionsViewModel CreateMentionsViewModel();
        MonthsViewModel CreateMonthsViewModel(Action<string, string> changeTitle);
        UserlogsViewModel CreateUserlogsViewModel(Action<string, string> changeTitle);
    }
}