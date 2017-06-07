using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IDayModel {
        string Name { get; }
        string Url { get; }
        string ApiUrl { get; }
        bool Visibility { get; set; }
        IMessageModel Messages { get; set; }
    }
}