using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiLogs {
        Task<List<IMessageModel>> Get(string user, string channel);
        Task<string> GetLogString(string url);
    }
}