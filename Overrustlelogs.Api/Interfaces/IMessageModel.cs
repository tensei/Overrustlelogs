using System.Windows.Input;

namespace Overrustlelogs.Api.Interfaces {
    public interface IMessageModel {
        string Text { get; set; }
        string[] UnEditedText { get; set; }
        string Month { get; set; }
        ICommand GetLogCommand { get; }
        bool GetLogButtonVisibility { get; set; }
        void ParseLog(string search);
    }
}