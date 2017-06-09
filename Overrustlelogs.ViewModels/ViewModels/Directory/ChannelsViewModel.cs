using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Directory {
    public class ChannelsViewModel : INotifyPropertyChanged {
        private readonly IApiChannels _channels;
        private readonly CurrentState _currentState;

        public ChannelsViewModel(IApiChannels channels, CurrentState currentState) {
            RefreshChannelCommand = new ActionCommand(async () => await GetChannels());
            SwitchToMonthCommand = new ActionCommand(c => _currentState.SwitchViewToMonth((ChannelModel) c));
            _channels = channels;
            _currentState = currentState;
            if (_currentState.Channels == null) {
                ChannelList = new ObservableCollection<ChannelModel>();
                GetChannels().ConfigureAwait(false);
                return;
            }
            ChannelList = new ObservableCollection<ChannelModel>(_currentState.Channels);
        }

        public ObservableCollection<ChannelModel> ChannelList { get; set; }

        public ICommand RefreshChannelCommand { get; }

        public ICommand SwitchToMonthCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetChannels() {
            var channels = await _channels.Get();
            if (channels == null) {
                return;
            }
            ChannelList = new ObservableCollection<ChannelModel>(channels);
            _currentState.Channels = channels;
        }
    }
}