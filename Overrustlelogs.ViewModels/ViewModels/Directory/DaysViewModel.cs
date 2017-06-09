using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;
using Overrustlelogs.ViewModels.Utils;

namespace Overrustlelogs.ViewModels.ViewModels.Directory {
    public class DaysViewModel : INotifyPropertyChanged {
        private readonly IApiDays _apiDays;
        private readonly CurrentState _currentState;


        public DaysViewModel(Action<string, string> changeTitle, IApiDays apiDays, CurrentState currentState) {
            RefreshDaysCommand = new ActionCommand(async () => await GetDays());
            OpenDayCommand = new ActionCommand(d => OpenLog((DayModel) d));
            _apiDays = apiDays;
            _currentState = currentState;
            changeTitle(_currentState.Channel.Name, _currentState.Month.Name);
            if (_currentState.Month.Days != null) {
                DaysList = _currentState.Month.Days;
                return;
            }
            GetDays().ConfigureAwait(false);
        }

        public ICommand RefreshDaysCommand { get; }
        public ObservableCollection<IDayModel> DaysList { get; set; }

        public ICommand OpenDayCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetDays() {
            var days = await _apiDays.Get(_currentState.Channel, _currentState.Month);
            if (days == null) {
                return;
            }
            _currentState.Month.Days = new ObservableCollection<IDayModel>(days);
            DaysList = _currentState.Month.Days;
        }

        private void OpenLog(IDayModel day) {
            if (day.Name == "userlogs") {
                _currentState.SwitchViewToUserlogs(_currentState.Month);
                return;
            }
            try {
                _currentState.Day = day;
                Process.Start(day.Url);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}