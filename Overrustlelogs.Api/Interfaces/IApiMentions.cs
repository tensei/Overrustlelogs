using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api.Interfaces {
    public interface IApiMentions {
        Task<List<MentionModel>> Get(string channel, string user, int? limit = default(int?), DateTime? date = default(DateTime?));
    }
}