using System.Collections.Generic;
using System.Threading.Tasks;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiChannels {
        Task<List<ChannelModel>> Get();
    }
}