using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.ViewModels.Interfaces {
    public interface IApiFactory {
        IApiChannels GetApiChannels();
        IApiMonths GetApiMonths();
        IApiDays GetApiDayss();
        IApiLogs GetApiLogss();
        IApiUserlogs GetApiUserlogs();
        IApiMentions GetApiMentions();
    }
}