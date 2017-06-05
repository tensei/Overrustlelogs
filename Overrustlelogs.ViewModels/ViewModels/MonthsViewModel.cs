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
        private readonly Action<string, string> _changeTitle;
        private IMonthModel _selectedMonth;

        public ICommand RefreshMonthCommand { get; }
        public ObservableCollection<IMonthModel> MonthsList { get; set; }

        public IMonthModel SelectedMonth {
            get => _selectedMonth;
            set {
                if (value == null) {
                    return;
                }
                CurrentState.SwitchViewToDays(value);
                _selectedMonth = value;
            }
        }
        
        public MonthsViewModel(IApiMonths apiMonths, Action<string, string> changeTitle) {
            RefreshMonthCommand = new ActionCommand(async ()=> await GetMonths());
            _apiMonths = apiMonths;
            _changeTitle = changeTitle;
            _changeTitle(CurrentState.Channel.Name, null);
            if (CurrentState.Channel.Months != null) {
                MonthsList = CurrentState.Channel.Months;
                return;
            }
            GetMonths().ConfigureAwait(false);
        }
        
        private async Task GetMonths() {
            var months = await _apiMonths.Get(CurrentState.Channel);
            CurrentState.Channel.Months = new ObservableCollection<IMonthModel>(months);
            MonthsList = CurrentState.Channel.Months;
        }
    }
}
