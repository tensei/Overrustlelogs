namespace Overrustlelogs.Api.Interfaces {
    public interface ILogMessageModel {
        string Date { get; set; }
        string Nick { get; set; }
        string Text { get; set; }
    }
}