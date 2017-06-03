using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiDays {
        Task<List<IDayModel>> Get(IChannelModel channel, IMonthModel month);
    }
}