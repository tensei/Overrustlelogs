using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IChannelModel {
        string Name { get; set; }
        string Url { get; set; }
        string ApiUrl { get; set; }
        bool Visibility { get; set; }
        ObservableCollection<IMonthModel> Months { get; set; }
    }
}