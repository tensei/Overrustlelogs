using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels.ViewModels {
    public class DaysViewModel: INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IApiDays _apiDays;
        private IDayModel _selectedDay;
        
        public ICommand RefreshDaysCommand { get; }
        public ObservableCollection<IDayModel> DaysList { get; set; }

        public IDayModel SelectedDay {
            get => _selectedDay;
            set {
                OpenLog(value);
                if (value == null) {
                    return;
                }
                _selectedDay = value;
            }
        }


        public DaysViewModel(Action<string, string> changeTitle, IApiDays apiDays) {
            RefreshDaysCommand = new ActionCommand(async ()=> await GetDays());
            _apiDays = apiDays;
            changeTitle(CurrentState.Channel.Name, CurrentState.Month.Name);
            if (CurrentState.Month.Days != null) {
                DaysList = CurrentState.Month.Days;
                return;
            }
            GetDays().ConfigureAwait(false);
        }

        private async Task GetDays() {
            var days = await _apiDays.Get(CurrentState.Channel, CurrentState.Month);
            CurrentState.Month.Days = new ObservableCollection<IDayModel>(days);
            CurrentState.Month.Days.Insert(0, new DayModel("userlogs", CurrentState.Month.Url+"/userlogs"));
            DaysList = CurrentState.Month.Days;
        }
        // 1 is userlogs
        // 0 day log
        private int? _lastclickitem = null;
        private DateTime _lastclick = DateTime.Now;
        private void OpenLog(IDayModel day) {
            var timediff = DateTime.Now - _lastclick;
            if (timediff.Seconds < 1) {
                return;
            }
            if (day == null) {
                if (_lastclickitem == 1) {
                    _lastclick = DateTime.Now;
                    CurrentState.SwitchViewToUserlogs(CurrentState.Month);
                    return;
                }
                if (CurrentState.Day != null) {
                    _lastclick = DateTime.Now;
                    _lastclickitem = 0;
                    Process.Start(CurrentState.Day.Url);
                    return;
                }
            }
            if (day.Name == "userlogs") {
                _lastclick = DateTime.Now;
                _lastclickitem = 1;
                CurrentState.SwitchViewToUserlogs(CurrentState.Month);
                return;
            }
            try {
                _lastclick = DateTime.Now;
                _lastclickitem = 0;
                CurrentState.Day = day;
                Process.Start(day.Url);
            } catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
