using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.Views {
    /// <summary>
    ///     Interaction logic for UserlogsView.xaml
    /// </summary>
    public partial class UserlogsView : UserControl {
        public UserlogsView() {
            InitializeComponent();
            UserList.SelectionChanged += (sender, args) => { UserList.UnselectAll(); };
            UserList.PreviewMouseRightButtonDown += (sender, args) => { args.Handled = true; };
        }
        
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) {
                return;
            }
            BindingOperations.GetBindingExpression((TextBox) sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (UserlogsViewModel) DataContext;
            datactx.FilterCommand.Execute(null);
        }
    }
}