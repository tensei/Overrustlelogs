using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overrustlelogs.Api.Interfaces;

namespace Overrustlelogs.Api.Models {
    public class ChannelModel : IChannelModel {
        public string Name { get; set; }
        public string Url { get; set; }
        public ObservableCollection<IMonthModel> Months { get; set; }

        public ChannelModel(string name, string url) {
            Name = name;
            Url = url;
        }
    }
}
