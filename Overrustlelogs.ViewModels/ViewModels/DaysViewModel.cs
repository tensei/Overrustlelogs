using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class DaysViewModel : INotifyPropertyChanged {
        private readonly IApiDays _apiDays;


        public DaysViewModel(Action<string, string> changeTitle, IApiDays apiDays) {
            RefreshDaysCommand = new ActionCommand(async () => await GetDays());
            OpenDayCommand = new ActionCommand(d => OpenLog((DayModel) d));
            _apiDays = apiDays;
            changeTitle(CurrentState.Channel.Name, CurrentState.Month.Name);
            if (CurrentState.Month.Days != null) {
                DaysList = CurrentState.Month.Days;
                return;
            }
            GetDays().ConfigureAwait(false);
        }

        public ICommand RefreshDaysCommand { get; }
        public ObservableCollection<IDayModel> DaysList { get; set; }

        public ICommand OpenDayCommand { get; }
        public event PropertyChangedEventHandler PropertyChanged;

        private async Task GetDays() {
            var days = await _apiDays.Get(CurrentState.Channel, CurrentState.Month);
            CurrentState.Month.Days = new ObservableCollection<IDayModel>(days);
            DaysList = CurrentState.Month.Days;
        }

        private void OpenLog(IDayModel day) {
            if (day.Name == "userlogs") {
                CurrentState.SwitchViewToUserlogs(CurrentState.Month);
                return;
            }
            try {
                CurrentState.Day = day;
                Process.Start(day.Url);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}