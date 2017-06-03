using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiMonths {
        Task<List<IMonthModel>> Get(IChannelModel channel);
        Task<List<IMonthModel>> Get(string channel);
    }
}