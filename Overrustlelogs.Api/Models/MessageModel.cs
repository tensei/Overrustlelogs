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
        }

        public string Url { get; }
        public string Text { get; set; }
        public string[] UnEditedText { get; set; }
        public string Month { get; set; }

        public bool GetLogButtonVisibility { get; set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
    }
}