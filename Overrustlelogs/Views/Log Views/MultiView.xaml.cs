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
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.Views.Log_Views {
    /// <summary>
    /// Interaction logic for MultiView.xaml
    /// </summary>
    public partial class MultiView : UserControl {
        public MultiView() {
            InitializeComponent();
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            BindingOperations.GetBindingExpression((TextBox)sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (LogCollectionViewModel)DataContext;
            datactx.SubmitCommand.Execute(null);
        }
        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) return;
            BindingOperations.GetBindingExpression((ComboBox)sender, ComboBox.TextProperty)?.UpdateSource();
            var datactx = (LogCollectionViewModel)DataContext;
            datactx.SubmitCommand.Execute(null);
        }
    }
}
