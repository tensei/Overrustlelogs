using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiMentions : IApiMentions {
        private readonly Action<string> _snackbarMessageQueue;
        private readonly HttpClient _httpClient;

        public ApiMentions(Action<string> snackbarMessageQueue) {
            _snackbarMessageQueue = snackbarMessageQueue;
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = {ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop")}
                    }
                };
            }
        }

        public async Task<List<MentionModel>> Get(string channel, string user, int? limit = null,
            DateTime? date = null) {
            var mdate = string.Empty;
            if (date != null) {
                mdate = $"?date={date?.ToString("yyyy-MM-dd")}";
            }
            var mlimit = string.Empty;
            if (limit != null) {
                var prefix = string.IsNullOrEmpty(mdate) ? "?" : "&";
                mlimit = $"{prefix}limit={limit}";
            }
            var url =
                $"https://overrustlelogs.net/api/v1/mentions/{channel.ToLower()}/{user.ToLower()}.json{mdate}{mlimit}";
            try {
                var response = await _httpClient.GetStringAsync(url);
                var mentions = JsonConvert.DeserializeObject<List<MentionModel>>(response);
                return mentions;
            }
            catch (Exception e) {
                _snackbarMessageQueue(e.Message);
                return null;
            }
        }
    }
}