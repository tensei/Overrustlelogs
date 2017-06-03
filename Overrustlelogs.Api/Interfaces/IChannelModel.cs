﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IChannelModel {
        string Name { get; set; }
        string Url { get; set; }
        ObservableCollection<IMonthModel> Months { get; set; }
    }
}