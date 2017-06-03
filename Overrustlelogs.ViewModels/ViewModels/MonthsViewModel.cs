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
        private readonly Action<string, string, string, string> _changeTitle;

        public ICommand SwitchToDayCommand { get; }
        public ObservableCollection<IMonthModel> MonthsList { get; set; }
        public MonthsViewModel(IApiMonths apiMonths, Action<string, string, string, string> changeTitle) {
            SwitchToDayCommand = new ActionCommand(d => CurrentState.SwitchViewToDays((MonthModel) d));
            _apiMonths = apiMonths;
            _changeTitle = changeTitle;
            _changeTitle(CurrentState.Channel.Name, null, null, null);
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
