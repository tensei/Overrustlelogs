using System;
using Overrustlelogs.ViewModels.ViewModels;
using Overrustlelogs.ViewModels.ViewModels.Directory;
using Overrustlelogs.ViewModels.ViewModels.Stalk;

namespace Overrustlelogs.ViewModels.Interfaces {
    public interface IViewModelFactory
    {
        ChannelsViewModel ChannelsViewModel { get; }
        MainWindowViewModel MainWindowViewModel { get; }

        DaysViewModel CreateDaysViewModel();
        StalkViewModel CreateStalkViewModel();
        StalkMultiViewModel CreateStalkMultiViewModel();
        StalkSingleViewModel CreateStalkSingleViewModel();
        MentionsViewModel CreateMentionsViewModel();
        MonthsViewModel CreateMonthsViewModel();
        UserlogsViewModel CreateUserlogsViewModel();
    }
}