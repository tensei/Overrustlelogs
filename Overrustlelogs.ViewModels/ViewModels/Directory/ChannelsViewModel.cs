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
            _channels = channels;
            _currentState = currentState;
            RefreshChannelCommand = new ActionCommand(async () => await GetChannels());
            SwitchToMonthCommand = new ActionCommand(c => _currentState.SwitchViewToMonth((ChannelModel) c));
            ChannelList = new ObservableCollection<ChannelModel>();
            if (_currentState.Channels == null) {
                GetChannels().ConfigureAwait(false);
                return;
            }
            ChannelList = new ObservableCollection<ChannelModel>(_currentState.Channels);
        }

        public ObservableCollection<ChannelModel> ChannelList { get; set; }

        public ICommand RefreshChannelCommand { get; }

        public ICommand SwitchToMonthCommand { get; }

        private async Task GetChannels() {
            var channels = await _channels.Get();
            if (channels == null || channels.Count <= 0) {
                return;
            }
            ChannelList.Clear();
            channels.ForEach(ChannelList.Add);
            _currentState.Channels = channels;
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}