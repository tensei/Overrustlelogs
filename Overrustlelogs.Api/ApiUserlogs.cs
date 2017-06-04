using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiUserlogs : IApiUserlogs {
        private readonly HttpClient _httpClient;
        public ApiUserlogs() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1)
                };
            }
        }

        public async Task<List<IUserModel>> Get(IMonthModel month) {
            var url = month.Url;
            var response = await _httpClient.GetStringAsync(url +"/userlogs");
            var dayList = Regex.Matches(response, "<span>&nbsp; ([a0-z9-]+).txt</span>", RegexOptions.IgnoreCase);
            var users = new List<IUserModel>();
            foreach (Match match in dayList) {
                users.Add(new UserModel(match.Groups[1].Value, $"{url}/userlogs/{match.Groups[1].Value}"));

            }
            return users;
        }
    }
}
