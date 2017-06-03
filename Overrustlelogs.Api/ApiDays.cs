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
    public class ApiDays : IApiDays {
        private readonly HttpClient _httpClient;
        public ApiDays() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1)
                };
            }
        }

        public async Task<List<IDayModel>> Get(IChannelModel channel, IMonthModel month) {
            var url = month.Url;
            var response = await _httpClient.GetStringAsync(url);
            var dayList = Regex.Matches(response, "<span>&nbsp; ([a0-z9-]+).txt</span>", RegexOptions.IgnoreCase);
            var days = new List<IDayModel>();
            foreach (Match match in dayList) {
                days.Add(new DayModel(match.Groups[1].Value, $"{url}/{match.Groups[1].Value}"));

            }
            return days;
        }
    }
}
