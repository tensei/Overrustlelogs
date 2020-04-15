using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMonthModel {
        string Name { get; set; }
        string Url { get; set; }
        string ApiUrl { get; set; }
        bool Visibility { get; set; }
        List<IDayModel> Days { get; set; }
        List<IUserModel> Users { get; set; }
    }
}