using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Overrustlelogs.ViewModels;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow(ViewModelFactory viewModelFactory) {
            InitializeComponent();
            DataContext = viewModelFactory.CreateMainWindowViewModel;
        }

        private void CurrentUrl_OnClick(object sender, RoutedEventArgs e) {
            var button = (Button) sender;
            try {
                Process.Start(button.Content.ToString());
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        private void Changelog_OnClick(object sender, RoutedEventArgs e) {
            try {
                Process.Start("https://github.com/slugalisk/overrustlelogs/commits/master");
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }

        private void Donate_OnClick(object sender, RoutedEventArgs e) {
            try {
                Process.Start("https://twitch.streamlabs.com/overrustlelogs#/");
            }
            catch (Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}