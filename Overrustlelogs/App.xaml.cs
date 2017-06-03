using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Practices.Unity;
using Overrustlelogs.Api;
using Overrustlelogs.Api.Interfaces;
using Overrustlelogs.ViewModels;
using Overrustlelogs.ViewModels.ViewModels;

namespace Overrustlelogs {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void AppStartup(object sender, StartupEventArgs args) {
            var container = new UnityContainer();
            container.RegisterType<IApiChannels, ApiChannels>();
            container.RegisterType<IApiMonths, ApiMonths>();
            container.RegisterType<IApiDays, ApiDays>();
            container.RegisterType<IApiLogs, ApiLogs>();
            container.RegisterType<ViewModelFactory>();
            container.RegisterType<MainWindow>();
            container.Resolve<MainWindow>().Show();
        }
    }
}
