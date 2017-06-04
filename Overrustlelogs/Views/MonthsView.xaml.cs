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
    /// Interaction logic for MonthsView.xaml
    /// </summary>
    public partial class MonthsView : UserControl {
        public MonthsView() {
            InitializeComponent();
            MonthList.SelectionChanged += (sender, args) => { MonthList.UnselectAll(); };
        }

        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e) {
            var filterTextbox = (TextBox)sender;
            var filter = filterTextbox.Text;
            foreach (var dayListItem in MonthList.Items) {
                var item = (MonthModel)dayListItem;
                if (!item.Name.ToLower().Contains(filter.ToLower())) {
                    item.Visibility = false;
                    continue;
                }
                item.Visibility = true;
            }
        }
    }
}
