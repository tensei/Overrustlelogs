using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class ChannelsViewModel : INotifyPropertyChanged {
        private readonly IApiChannels _channels;
        public event PropertyChangedEventHandler PropertyChanged;
        public ChannelsViewModel(IApiChannels channels) {
            SwitchToMonthCommand = new ActionCommand(m => CurrentState.SwitchViewToMonth((ChannelModel) m));
            _channels = channels;
            ChannelList = new ObservableCollection<ChannelModel>();
            GetChannels().ConfigureAwait(false);
        }
        public ObservableCollection<ChannelModel> ChannelList { get; set; }

        public ICommand SwitchToMonthCommand { get; }
        public async Task GetChannels() {
            var channel = await _channels.Get();
            channel.ForEach(ChannelList.Add);
            CurrentState.Channels = channel;
        }
    }
}
