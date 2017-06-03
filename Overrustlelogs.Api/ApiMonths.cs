using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiMonths : IApiMonths {
        private readonly HttpClient _httpClient;
        public ApiMonths() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1)
                };
            }
        }

        public async Task<List<IMonthModel>> Get(IChannelModel channel) {
            var url = channel.Url;
            var response = await _httpClient.GetStringAsync(url);
            var channelsregex = Regex.Matches(response, "<span>&nbsp; ([a-z]+ [0-9]+)</span>", RegexOptions.IgnoreCase);
            var channels = new List<IMonthModel>();
            foreach (Match match in channelsregex) {
                channels.Add(new MonthModel(match.Groups[1].Value, $"{url}/{match.Groups[1].Value.Replace(" ", "%20")}"));

            }
            return channels;
        }
        public async Task<List<IMonthModel>> Get(string channel) {
            var url = $"https://overrustlelogs.net/{channel}%20chatlog";
            string response;
            try {
                response = await _httpClient.GetStringAsync(url);
            }
            catch (Exception) {
                return null;
            }
            var channelsregex = Regex.Matches(response, "<span>&nbsp; ([a-z]+ [0-9]+)</span>", RegexOptions.IgnoreCase);
            var channels = new List<IMonthModel>();
            foreach (Match match in channelsregex) {
                channels.Add(new MonthModel(match.Groups[1].Value, $"{url}/{match.Groups[1].Value.Replace(" ", "%20")}"));

            }
            return channels;
        }
    }
}
