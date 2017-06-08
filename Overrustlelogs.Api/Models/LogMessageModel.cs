using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class LogMessageModel : ILogMessageModel {
        public string Text { set; get; }
        public string Nick { set; get; }
        public string Date { set; get; }
    }
}