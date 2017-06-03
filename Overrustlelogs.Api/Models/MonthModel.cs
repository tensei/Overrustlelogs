using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class MonthModel : IMonthModel {
        public string Name { get; }
        public string Url { get; }

        public ObservableCollection<IDayModel> Days { get; set; }
        public MonthModel(string name, string url) {
            Name = name;
            Url = url;
        }
    }
}
