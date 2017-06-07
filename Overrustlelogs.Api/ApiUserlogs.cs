using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiUserlogs : IApiUserlogs {
        private readonly HttpClient _httpClient;
        public ApiUserlogs() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = { ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop") }
                    }
                };
            }
        }

        public async Task<List<IUserModel>> Get(IChannelModel channel, IMonthModel month) {
            var url = month.ApiUrl.Replace("days.json", "users.json");
            string response;
            try {
                response = await _httpClient.GetStringAsync(url);
            } catch (Exception) {
                return null;
            }
            var userList = JsonConvert.DeserializeObject<List<string>>(response);
            var users = new List<IUserModel>();
            foreach (var user in userList) {
                users.Add(new UserModel(user, $"https://overrustlelogs.net/{channel.Name}%20chatlog/{month.Name}/userlogs/{user.Replace(".txt", string.Empty)}"));

            }
            return users;
        }
    }
}
