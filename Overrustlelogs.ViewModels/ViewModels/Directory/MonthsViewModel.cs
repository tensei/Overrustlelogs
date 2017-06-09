using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Directory {
    public class MonthsViewModel : INotifyPropertyChanged {
        private readonly IApiMonths _apiMonths;
        private readonly CurrentState _currentState;


        public MonthsViewModel(IApiMonths apiMonths, Action<string, string> changeTitle, CurrentState currentState) {
            RefreshMonthCommand = new ActionCommand(async () => await GetMonths());
            SwitchToDaysCommand = new ActionCommand(m => _currentState.SwitchViewToDays((MonthModel) m));
            _apiMonths = apiMonths;
            _currentState = currentState;
            changeTitle(_currentState.Channel.Name, null);
            if (_currentState.Channel.Months != null) {
                MonthsList = _currentState.Channel.Months;
                return;
            }
            GetMonths().ConfigureAwait(false);
        }

        public ICommand RefreshMonthCommand { get; }
        public ObservableCollection<IMonthModel> MonthsList { get; set; }
        public ICommand SwitchToDaysCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetMonths() {
            var months = await _apiMonths.Get(_currentState.Channel);
            if (months == null) {
                return;
            }
            _currentState.Channel.Months = new ObservableCollection<IMonthModel>(months);
            MonthsList = _currentState.Channel.Months;
        }
    }
}