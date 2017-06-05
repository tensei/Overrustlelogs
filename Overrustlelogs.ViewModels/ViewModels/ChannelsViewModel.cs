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
        private IChannelModel _selectedChannel;
        public event PropertyChangedEventHandler PropertyChanged;
        public ChannelsViewModel(IApiChannels channels) {
            RefreshChannelCommand = new ActionCommand(async () => await GetChannels());
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

        public IChannelModel SelectedChannel {
            get => _selectedChannel;
            set {
                if (value == null) {
                    return;
                }
                CurrentState.SwitchViewToMonth(value);
                _selectedChannel = value;
            }
        }
        

        private async Task GetChannels() {
            var channels = await _channels.Get();
            ChannelList = new ObservableCollection<ChannelModel>(channels);
            CurrentState.Channels = channels;
            SelectedChannel = null;
        }
    }
}
