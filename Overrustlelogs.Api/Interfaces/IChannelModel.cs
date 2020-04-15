﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Overrustlelogs.Api.Interfaces {
    public interface IChannelModel {
        string Name { get; set; }
        string Url { get; set; }
        string ApiUrl { get; set; }
        bool Visibility { get; set; }
        List<IMonthModel> Months { get; set; }
    }
}