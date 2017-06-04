namespace Overrustlelogs.Api.Interfaces {
    public interface IUserModel {
        string Name { get; }
        string Url { get; }
        bool Visibility { get; set; }
    }
}