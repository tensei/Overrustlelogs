using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiDays : IApiDays {
        private readonly HttpClient _httpClient;

        public ApiDays() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = {ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop")}
                    }
                };
            }
        }

        public async Task<List<IDayModel>> Get(IChannelModel channel, IMonthModel month) {
            var url = month.ApiUrl;
            string response;
            try {
                response = await _httpClient.GetStringAsync(url);
            }
            catch (Exception) {
                return null;
            }
            var dayList = JsonConvert.DeserializeObject<List<string>>(response);
            dayList.Insert(0, "userlogs");
            var days = new List<IDayModel>();
            foreach (var day in dayList) {
                var dayurl =
                    $"https://overrustlelogs.net/{channel.Name}%20chatlog/{month.Name.Replace(" ", "%20")}/{day.Replace(".txt", string.Empty)}";
                var dayurlapi = $"https://overrustlelogs.net/{channel.Name}/{month.Name}/users.json";
                days.Add(new DayModel(day, dayurl, dayurlapi));
            }
            return days;
        }
    }
}