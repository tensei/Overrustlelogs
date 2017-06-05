using System;
using System.Windows.Controls;
using System.Windows.Input;
using Overrustlelogs.Api.Models;

namespace Overrustlelogs.Views {
    /// <summary>
    ///     Interaction logic for MonthsView.xaml
    /// </summary>
    public partial class MonthsView : UserControl {
        public MonthsView() {
            InitializeComponent();
            MonthList.SelectionChanged += (sender, args) => { MonthList.UnselectAll(); };
            MonthList.PreviewMouseRightButtonDown += (sender, args) => { args.Handled = true; };
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