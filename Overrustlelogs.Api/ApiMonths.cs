using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class ApiMonths : IApiMonths {
        private readonly HttpClient _httpClient;
        public ApiMonths() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = { ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop") }
                    }
                };
            }
        }

        public async Task<List<IMonthModel>> Get(IChannelModel channel) {
            return await Get(channel.Name);
        }
        public async Task<List<IMonthModel>> Get(string channel) {
            var url = $"https://overrustlelogs.net/api/v1/{channel}/months.json";
            string response;
            try {
                response = await _httpClient.GetStringAsync(url);
            }
            catch (Exception) {
                return null;
            }
            var months = JsonConvert.DeserializeObject<List<string>>(response);
            var monthList = new List<IMonthModel>();
            foreach (var month in months) {
                var monthurlapi = $"https://overrustlelogs.net/api/v1/{channel}/{month.Replace(" ", "%20")}/days.json";
                var monthurl = $"https://overrustlelogs.net/{channel} chatlog/{month}";
                monthList.Add(new MonthModel(month, monthurl, monthurlapi));
            }
            return monthList;
        }
    }
}
