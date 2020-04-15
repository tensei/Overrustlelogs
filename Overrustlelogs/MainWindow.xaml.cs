using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs {
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow {
        public MainWindow(IViewModelFactory viewModelFactory, SnackbarMessageQueue snackbarMessageQueue) {
            InitializeComponent();
            DataContext = viewModelFactory.MainWindowViewModel;
            SnackbarMessage.MessageQueue = snackbarMessageQueue;
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

        private void Grid_OnMouseRightButtonUp(object sender, MouseButtonEventArgs e) {
            var datacontext = (MainWindowViewModel) DataContext;
            datacontext.Back();
        }
    }
}