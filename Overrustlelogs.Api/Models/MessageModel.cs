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
            _url = url;
            GetLogCommand = new ActionCommand(async () => await GetLog());
        }

        private string _url { get; }
        public string Text { get; set; }
        public string[] UnEditedText { get; set; }
        public string Month { get; set; }

        public bool GetLogButtonVisibility { get; set; }
        public ICommand GetLogCommand { get; }

        public void ParseLog(string search) {
            // [2017-05-20 19:04:51 UTC] xxxx: xxxx
            var text = UnEditedText.Where(s => s.ToLower().Contains(search.ToLower()))
                .Aggregate(string.Empty, (current, s) => current + $"{s}\n");
            Text = text;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetLog() {
            Text = string.Empty;
            GetLogButtonVisibility = true;
            var text = await _apiLogs.GetLogString(_url);
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