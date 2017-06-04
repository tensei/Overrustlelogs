using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Views {
    /// <summary>
    /// Interaction logic for ChannelsView.xaml
    /// </summary>
    public partial class ChannelsView : UserControl {
        public ChannelsView() {
            InitializeComponent();
            ChannelList.SelectionChanged += (sender, args) => { ChannelList.UnselectAll(); };
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
