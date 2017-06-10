using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Overrustlelogs.ViewModels.Models;
using Overrustlelogs.ViewModels.ViewModels;
using Overrustlelogs.ViewModels.ViewModels.Stalk;

namespace Overrustlelogs.Views.Log_Views {
    /// <summary>
    ///     Interaction logic for MultiView.xaml
    /// </summary>
    public partial class MultiView : UserControl {
        public MultiView() {
            InitializeComponent();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) {
                return;
            }
            BindingOperations.GetBindingExpression((TextBox) sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (StalkMultiViewModel) DataContext;
            datactx.AddUserCommand.Execute(null);
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) {
                return;
            }
            BindingOperations.GetBindingExpression((ComboBox) sender, ComboBox.TextProperty)?.UpdateSource();
            var datactx = (StalkMultiViewModel) DataContext;
            datactx.AddUserCommand.Execute(null);
        }

        private void Search_OnTextChanged(object sender, TextChangedEventArgs e) {
            var textbox = (TextBox) sender;
            BindingOperations.GetBindingExpression(textbox, TextBox.TextProperty)?.UpdateSource();
            var datactxtextbox = (MultiViewUserModel) textbox.DataContext;
            var datactx = (StalkMultiViewModel) DataContext;
            datactx.ParseLog(textbox.Text, datactxtextbox.SelectedMonth);
        }

        private void LogTextBox_OnTextChanged(object sender, TextChangedEventArgs e) {
            var textbox = sender as TextBox;
            textbox?.ScrollToEnd();
        }
    }
}