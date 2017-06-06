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
            RefreshChannelCommand = new ActionCommand(async () => await GetChannels());
            SwitchToMonthCommand = new ActionCommand(c => CurrentState.SwitchViewToMonth((ChannelModel)c));
            _channels = channels;
            if (CurrentState.Channels == null) {
                ChannelList = new ObservableCollection<ChannelModel>();
                GetChannels().ConfigureAwait(false);
                return;
            }
            ChannelList = new ObservableCollection<ChannelModel>(CurrentState.Channels);
        }
        public ObservableCollection<ChannelModel> ChannelList { get; set; }
        
        public ICommand RefreshChannelCommand { get; }

        public ICommand SwitchToMonthCommand { get; }

        private async Task GetChannels() {
            var channels = await _channels.Get();
            ChannelList = new ObservableCollection<ChannelModel>(channels);
            CurrentState.Channels = channels;
        }
    }
}
