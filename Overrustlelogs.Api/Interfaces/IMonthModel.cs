using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMonthModel {
        string Name { get; }
        string Url { get; }
        ObservableCollection<IDayModel> Days { get; set; }
    }
}