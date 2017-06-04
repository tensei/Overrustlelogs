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
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.Views {
    /// <summary>
    /// Interaction logic for UserlogsView.xaml
    /// </summary>
    public partial class UserlogsView : UserControl {
        public UserlogsView() {
            InitializeComponent();
            UserList.SelectionChanged += (sender, args) => { UserList.UnselectAll(); };
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            BindingOperations.GetBindingExpression((TextBox)sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (UserlogsViewModel)DataContext;
            datactx.FilterCommand.Execute(null);
        }
    }
}
