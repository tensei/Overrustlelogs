using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class MentionsViewModel : INotifyPropertyChanged {
        private readonly IApiChannels _apiChannels;
        private readonly IApiMentions _apiMentions;
        private readonly CurrentState _currentState;

        public MentionsViewModel(IApiChannels apiChannels, IApiMentions apiMentions, CurrentState currentState) {
            _apiChannels = apiChannels;
            _apiMentions = apiMentions;
            _currentState = currentState;
            SubmitCommand = new ActionCommand(async () => await GetMessages());
            SelectedDate = DateTime.Now;
            if (_currentState.Channels == null) {
                Channels = new ObservableCollection<string>();
                GetChannel().ConfigureAwait(false);
                return;
            }
            Channels = new ObservableCollection<string>();
            _currentState.Channels.ForEach(c => Channels.Add(c.Name));
        }

        public string User { get; set; }
        public string Text { get; set; }
        public string[] UnEditedText { get; set; }
        public ObservableCollection<string> Channels { get; set; }

        public ICommand SubmitCommand { get; }


        public string SelectedChannel { get; set; }
        public Visibility ProgressbarVisibility { get; set; } = Visibility.Collapsed;

        public DateTime SelectedDate { get; set; }

        public List<int> Limits => new List<int> {0, 1, 10, 100, 1000, 10000};
        public int SelectedLimit { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetMessages() {
            if (string.IsNullOrWhiteSpace(User) || string.IsNullOrWhiteSpace(SelectedChannel)) {
                return;
            }
            if (SelectedChannel != "Destinygg") {
                User = User.ToLower();
            }
            ProgressbarVisibility = Visibility.Visible;
            DateTime? date = SelectedDate;
            if (date?.Date == DateTime.Today.Date) {
                date = null;
            }
            int? limit = SelectedLimit;
            if (limit == 0) {
                limit = null;
            }
            var mentions = await _apiMentions.Get(SelectedChannel, User, limit, date);
            if (mentions == null) {
                Text = $"No Mentions found for {SelectedDate.Date}";
                ProgressbarVisibility = Visibility.Collapsed;
                return;
            }
            var sb = new StringBuilder();
            foreach (var mentionModel in mentions) {
                sb.AppendLine($"[{mentionModel.Date}] {mentionModel.nick}: {mentionModel.text}");
            }
            Text = sb.ToString();
            UnEditedText = sb.ToString().Split('\n');
            ProgressbarVisibility = Visibility.Collapsed;
        }

        private async Task GetChannel() {
            var ch = await _apiChannels.Get();
            ch.ForEach(c => Channels.Add(c.Name));
        }

        public void ParseLog(string search) {
            // [2017-05-20 19:04:51 UTC] xxxx: xxxx
            var text = UnEditedText.Where(s => s.ToLower().Contains(search.ToLower()))
                .Aggregate(string.Empty, (current, s) => current + $"{s}\n");
            Text = text;
        }
    }
}