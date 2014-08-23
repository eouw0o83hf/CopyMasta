using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Castle.Windsor;

namespace CopyMasta
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IWindsorContainer _container;
        private MainWindow _mainWindow;

        public App()
        {
            _container = new WindsorContainer();
            var installer = new CmWindsorInstaller();
            _container.Install(installer);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _mainWindow = _container.Resolve<MainWindow>();
            _mainWindow.ShowDialog();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _container.Release(_mainWindow);
            base.OnExit(e);
        }
    }
}
