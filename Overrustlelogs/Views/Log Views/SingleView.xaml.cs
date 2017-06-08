using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs.Views.Log_Views {
    /// <summary>
    ///     Interaction logic for SingleView.xaml
    /// </summary>
    public partial class SingleView : UserControl {
        public SingleView() {
            InitializeComponent();
        }

        private void DownArrow_OnClick(object sender, RoutedEventArgs e) {
            TextLog.ScrollToEnd();
        }

        private void UpArrow_OnClick(object sender, RoutedEventArgs e) {
            TextLog.ScrollToHome();
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) {
                return;
            }
            BindingOperations.GetBindingExpression((TextBox) sender, TextBox.TextProperty)?.UpdateSource();
            var datactx = (LogCollectionViewModel) DataContext;
            datactx.SubmitCommand.Execute(null);
        }

        private void ComboBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (e.Key != Key.Enter) {
                return;
            }
            BindingOperations.GetBindingExpression((ComboBox) sender, ComboBox.TextProperty)?.UpdateSource();
            var datactx = (LogCollectionViewModel) DataContext;
            datactx.SubmitCommand.Execute(null);
        }

        private void Search_OnTextChanged(object sender, TextChangedEventArgs e) {
            var textbox = (TextBox) sender;
            //BindingOperations.GetBindingExpression(textbox, TextBox.TextProperty)?.UpdateSource();
            var datactx = (LogCollectionViewModel) DataContext;
            datactx?.SelectedMonth?.ParseLog(textbox.Text);
        }
    }
}