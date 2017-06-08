using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class MonthsViewModel : INotifyPropertyChanged {
        private readonly IApiMonths _apiMonths;


        public MonthsViewModel(IApiMonths apiMonths, Action<string, string> changeTitle) {
            RefreshMonthCommand = new ActionCommand(async () => await GetMonths());
            SwitchToDaysCommand = new ActionCommand(m => CurrentState.SwitchViewToDays((MonthModel) m));
            _apiMonths = apiMonths;
            changeTitle(CurrentState.Channel.Name, null);
            if (CurrentState.Channel.Months != null) {
                MonthsList = CurrentState.Channel.Months;
                return;
            }
            GetMonths().ConfigureAwait(false);
        }

        public ICommand RefreshMonthCommand { get; }
        public ObservableCollection<IMonthModel> MonthsList { get; set; }
        public ICommand SwitchToDaysCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetMonths() {
            var months = await _apiMonths.Get(CurrentState.Channel);
            if (months == null) {
                return;
            }
            CurrentState.Channel.Months = new ObservableCollection<IMonthModel>(months);
            MonthsList = CurrentState.Channel.Months;
        }
    }
}