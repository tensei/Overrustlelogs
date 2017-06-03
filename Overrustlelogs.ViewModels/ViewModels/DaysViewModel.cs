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
        
        private readonly Action<string, string> _changeTitle;
        private readonly IApiDays _apiDays;

        public ICommand SwitchToLogCommand { get; }
        public ObservableCollection<IDayModel> DaysList { get; set; }
        public DaysViewModel(Action<string, string> changeTitle, IApiDays apiDays) {
            SwitchToLogCommand = new ActionCommand(l => OpenLog((DayModel)l));
            _changeTitle = changeTitle;
            _apiDays = apiDays;
            _changeTitle(CurrentState.Channel.Name, CurrentState.Month.Name);
            if (CurrentState.Month.Days != null) {
                DaysList = CurrentState.Month.Days;
                return;
            }
            GetDays().ConfigureAwait(false);
        }

        private async Task GetDays() {
            var days = await _apiDays.Get(CurrentState.Channel, CurrentState.Month);
            CurrentState.Month.Days = new ObservableCollection<IDayModel>(days);
            DaysList = CurrentState.Month.Days;
        }

        private void OpenLog(IDayModel day) {
            try {
                Process.Start(day.Url);
            }
            catch (Exception e) {
                Console.WriteLine(e);
            }
        }
    }
}
