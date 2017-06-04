using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMonthModel {
        string Name { get; }
        string Url { get; }
        bool Visibility { get; set; }
        ObservableCollection<IDayModel> Days { get; set; }
        ObservableCollection<IUserModel> Users { get; set; }
    }
}