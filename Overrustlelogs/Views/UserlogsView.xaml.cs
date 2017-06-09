using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Overrustlelogs.ViewModels.ViewModels;
using Overrustlelogs.ViewModels.ViewModels.Directory;

namespace Overrustlelogs.Views {
    /// <summary>
    ///     Interaction logic for UserlogsView.xaml
    /// </summary>
    public partial class UserlogsView : UserControl {
        public UserlogsView() {
            InitializeComponent();
        }
        
        private void Filter_OnTextChanged(object sender, TextChangedEventArgs e) {
            BindingOperations.GetBindingExpression((TextBox) sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (UserlogsViewModel) DataContext;
            datactx.Filter();
        }
    }
}