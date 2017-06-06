using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class MonthsViewModel : INotifyPropertyChanged{
        public event PropertyChangedEventHandler PropertyChanged;
        

        private readonly IApiMonths _apiMonths;
        public ICommand RefreshMonthCommand { get; }
        public ObservableCollection<IMonthModel> MonthsList { get; set; }
        
        
        public MonthsViewModel(IApiMonths apiMonths, Action<string, string> changeTitle) {
            RefreshMonthCommand = new ActionCommand(async ()=> await GetMonths());
            SwitchToDaysCommand = new ActionCommand(m => CurrentState.SwitchViewToDays((MonthModel)m));
            _apiMonths = apiMonths;
            changeTitle(CurrentState.Channel.Name, null);
            if (CurrentState.Channel.Months != null) {
                MonthsList = CurrentState.Channel.Months;
                return;
            }
            GetMonths().ConfigureAwait(false);
        }
        public ICommand SwitchToDaysCommand { get; }
        
        private async Task GetMonths() {
            var months = await _apiMonths.Get(CurrentState.Channel);
            CurrentState.Channel.Months = new ObservableCollection<IMonthModel>(months);
            MonthsList = CurrentState.Channel.Months;
        }
    }
}
