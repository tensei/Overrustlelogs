using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Overrustlelogs.Api;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels.Factories;
using Overrustlelogs.ViewModels.Interfaces;
using Overrustlelogs.ViewModels.Utils;
using Unity;

namespace Overrustlelogs {
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private SnackbarMessageQueue _snackbarMessageQueue;

        private void AppStartup(object sender, StartupEventArgs args) {
            var container = new UnityContainer();
            _snackbarMessageQueue = new SnackbarMessageQueue();
            container.RegisterInstance<Action<string>>(SnackbarQueueMessage);
            container.RegisterType<CurrentState>();
            container.RegisterInstance(_snackbarMessageQueue);
            container.RegisterType<IApiChannels, ApiChannels>();
            container.RegisterType<IApiMonths, ApiMonths>();
            container.RegisterType<IApiDays, ApiDays>();
            container.RegisterType<IApiLogs, ApiLogs>();
            container.RegisterType<IApiUserlogs, ApiUserlogs>();
            container.RegisterType<IApiMentions, ApiMentions>();
            container.RegisterType<IApiFactory, ApiFactory>();
            container.RegisterType<IViewModelFactory, ViewModelFactory>();
            container.RegisterType<MainWindow>();
            container.Resolve<MainWindow>().Show();
        }

        private void SnackbarQueueMessage(string text) {
            if (_snackbarMessageQueue == null) {
                _snackbarMessageQueue = new SnackbarMessageQueue();
            }
            _snackbarMessageQueue.Enqueue(text, true);
        }
    }
}