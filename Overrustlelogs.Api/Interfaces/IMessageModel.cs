using System.Windows.Input;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMessageModel {
        string Text { get; set; }
        string[] UnEditedText { get; set; }
        string Month { get; set; }
        bool GetLogButtonVisibility { get; set; }
        string Url { get; }
    }
}