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
    /// Interaction logic for DaysView.xaml
    /// </summary>
    public partial class DaysView : UserControl {
        public DaysView() {
            InitializeComponent();
            DayList.SelectionChanged += (sender, args) => { DayList.UnselectAll(); };
        }

        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e) {
            var filterTextbox = (TextBox)sender;
            var filter = filterTextbox.Text;
            foreach (var dayListItem in DayList.Items) {
                var item = (DayModel)dayListItem;
                if (!item.Name.ToLower().Contains(filter.ToLower())) {
                    item.Visibility = false;
                    continue;
                }
                item.Visibility = true;
            }
        }
    }
}
