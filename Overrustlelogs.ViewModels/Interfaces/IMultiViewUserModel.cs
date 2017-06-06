using System.Collections.ObjectModel;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.ViewModels.Interfaces {
    public interface IMultiViewUserModel {
        string Channel { get; set; }
        string User { get; set; }
        ObservableCollection<IMessageModel> Months { get; }
        IMessageModel SelectedMonth { get; set; }
    }
}