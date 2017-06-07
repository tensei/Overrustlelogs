using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiChannels : IApiChannels {
        private readonly HttpClient _httpClient;
        public ApiChannels() {
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = { ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop") }
                    }
                };
            }
        }

        public async Task<List<ChannelModel>> Get() {
            const string url = "https://overrustlelogs.net/api/v1/channels.json";
            string response;
            try {
                response = await _httpClient.GetStringAsync(url);
            } catch (Exception) {
                return null;
            }
            var json = JsonConvert.DeserializeObject<List<string>>(response);
            var channels = new List<ChannelModel>();
            json.ForEach(x => channels.Add(new ChannelModel(x, $"https://overrustlelogs.net/{x}%20chatlog",
                $"https://overrustlelogs.net/api/v1/{x}/months.json")));
            return channels;
        }
    }
}
