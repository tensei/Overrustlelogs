using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Api {
    public class ApiLogs : IApiLogs {
        private readonly IApiMonths _apiMonths;

        private readonly HttpClient _httpClient;

        public ApiLogs(IApiMonths apiMonths) {
            _apiMonths = apiMonths;
            if (_httpClient == null) {
                _httpClient = new HttpClient {
                    Timeout = TimeSpan.FromMinutes(1),
                    DefaultRequestHeaders = {
                        UserAgent = {ProductInfoHeaderValue.Parse("Overrustlelogs-Desktop")}
                    }
                };
            }
        }

        public async Task<List<IMessageModel>> Get(string user, string channel) {
            var months = await _apiMonths.Get(channel);
            var monthList = new List<IMessageModel>();
            foreach (var monthModel in months) {
                var url = $"{monthModel.Url}/userlogs/{user}.txt";
                monthList.Add(new MessageModel(null, monthModel.Name, url, this));
            }
            if (monthList.Count > 0) {
                monthList[0].GetLogCommand.Execute(null);
            }
            return monthList;
        }

        public async Task<string> GetLogString(string url) {
            try {
                return await _httpClient.GetStringAsync(url);
            }
            catch (HttpRequestException e) {
                return e.Message;
            }
            catch (Exception) {
                return null;
            }
        }
    }
}