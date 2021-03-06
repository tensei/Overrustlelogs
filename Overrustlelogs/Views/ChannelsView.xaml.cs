﻿using System.Windows.Controls;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Views {
    /// <summary>
    ///     Interaction logic for ChannelsView.xaml
    /// </summary>
    public partial class ChannelsView : UserControl {
        public ChannelsView() {
            InitializeComponent();
        }


        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e) {
            var filterTextbox = (TextBox) sender;
            var filter = filterTextbox.Text;
            foreach (var channelListItem in ChannelList.Items) {
                var item = (ChannelModel) channelListItem;
                if (!item.Name.ToLower().Contains(filter.ToLower())) {
                    item.Visibility = false;
                    continue;
                }
                item.Visibility = true;
            }
        }
    }
}