using System;
using System.Collections.Generic;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.ViewModels {
    public class CurrentState {
        public static IChannelModel Channel { get; set; }
        public static List<ChannelModel> Channels { get; set; }
        public static IMonthModel Month { get; set; }
        public static IDayModel Day { get; set; }

        public static Action<IChannelModel> SwitchViewToMonth { get; set; }
        public static Action<IMonthModel> SwitchViewToDays { get; set; }
        public static Action<IMonthModel> SwitchViewToUserlogs { get; set; }
    }
}