using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMonthModel {
        string Name { get; set; }
        string Url { get; set; }
        bool Visibility { get; set; }
        ObservableCollection<IDayModel> Days { get; set; }
        ObservableCollection<IUserModel> Users { get; set; }
    }
}