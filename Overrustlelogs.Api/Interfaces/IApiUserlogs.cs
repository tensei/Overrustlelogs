using System.Collections.Generic;
using System.Threading.Tasks;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiUserlogs {
        Task<List<IUserModel>> Get(IMonthModel month);
    }
}