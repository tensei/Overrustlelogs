using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Models;

namespace Overrustlelogs.ViewModels.Utils {
    public class CurrentState {
        private readonly IApiLogs _apiLogs;
        private readonly Action<string> _snackbarMessageQueue;

        public CurrentState(IApiLogs apiLogs, Action<string> snackbarMessageQueue) {
            _apiLogs = apiLogs;
            _snackbarMessageQueue = snackbarMessageQueue;
        }
        public IChannelModel Channel { get; set; }
        public List<ChannelModel> Channels { get; set; }
        public IMonthModel Month { get; set; }
        public IDayModel Day { get; set; }

        public Action<IChannelModel> SwitchViewToMonth { get; set; }
        public Action<IMonthModel> SwitchViewToDays { get; set; }
        public Action<IMonthModel> SwitchViewToUserlogs { get; set; }

        public void SaveMultiViewUsers(List<IMultiViewUserModel> multiViewUserModels) {
            var folder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "orl");
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var file = Path.Combine(folder, "MultiViewUsers.json");
            var json = JsonConvert.SerializeObject(multiViewUserModels, Formatting.Indented);
            try {
                File.WriteAllText(file, json);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
        public async Task<List<MultiViewUserModel>> LoadMultiViewUsers() {
            var folder = Path.Combine(Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData), "orl");
            if (!Directory.Exists(folder)) {
                Directory.CreateDirectory(folder);
            }
            var file = Path.Combine(folder, "MultiViewUsers.json");
            if (!File.Exists(file)) {
                return null;
            }
            var filestring = File.ReadAllText(file);
            var json = JsonConvert.DeserializeObject<List<MultiViewUserModel>>(filestring);
            foreach (var multiViewUserModel in json) {
                try {
                    var months = await _apiLogs.Get(multiViewUserModel.User, multiViewUserModel.Channel);
                    multiViewUserModel.Months = new ObservableCollection<IMessageModel>(months);
                    //multiViewUserModel.SelectedMonth = months[0];
                    await Task.Delay(50);
                }
                catch (Exception e) {
                    _snackbarMessageQueue(e.Message);
                    Console.WriteLine(e);
                }
            }
            return json;
        }
    }
}