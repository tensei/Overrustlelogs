using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class MessageModel : IMessageModel, INotifyPropertyChanged {
        private readonly IApiLogs _apiLogs;

        public MessageModel(string text, string month, string url, IApiLogs apiLogs) {
            _apiLogs = apiLogs;
            Text = text;
            Month = month;
            Url = url;
            GetLogCommand = new ActionCommand(async () => await GetLog());
        }

        public string Url { get; }
        public string Text { get; set; }
        public string[] UnEditedText { get; set; }
        public string Month { get; set; }

        public bool GetLogButtonVisibility { get; set; }
        public ICommand GetLogCommand { get; }

        public void ParseLog(string search) {
            // [2017-05-20 19:04:51 UTC] xxxx: xxxx
            var text = string.Empty;
            foreach (var s in UnEditedText) {
                if (s.ToLower().Contains(search.ToLower())) text = text + $"{s}\n";
            }
            Text = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetLog() {
            Text = string.Empty;
            GetLogButtonVisibility = true;
            var text = await _apiLogs.GetLogString(Url);
            if (text == null) {
                Text = "Error try again";
                return;
            }
            Text = text;
            UnEditedText = text.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries);
            GetLogButtonVisibility = false;
        }
    }
}