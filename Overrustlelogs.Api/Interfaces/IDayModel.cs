using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IDayModel {
        string Name { get; }
        string Url { get; }
        IMessageModel Messages { get; set; }
    }
}