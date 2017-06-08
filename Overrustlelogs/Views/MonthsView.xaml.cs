using System.Windows.Controls;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Views {
    /// <summary>
    ///     Interaction logic for MonthsView.xaml
    /// </summary>
    public partial class MonthsView : UserControl {
        public MonthsView() {
            InitializeComponent();
        }

        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e) {
            var filterTextbox = (TextBox) sender;
            var filter = filterTextbox.Text;
            foreach (var dayListItem in MonthList.Items) {
                var item = (MonthModel) dayListItem;
                if (!item.Name.ToLower().Contains(filter.ToLower())) {
                    item.Visibility = false;
                    continue;
                }
                item.Visibility = true;
            }
        }
    }
}