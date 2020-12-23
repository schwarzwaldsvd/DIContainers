using PeopleViewer.Presentation;
using PersonRepository.CachingDecorator;
using PersonRepository.CSV;
using PersonRepository.Interface;
using PersonRepository.Service;
using PersonRepository.SQL;
using System.Windows;
using Ninject;

namespace PeopleViewer
{
    public partial class App : Application
    {
        IKernel Container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            Container = new StandardKernel();
            Container.Bind<IPersonRepository>().To<CSVRepository>().InSingletonScope();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Get<PeopleViewerWindow>();
            Application.Current.MainWindow.Title = "DI with Ninject - People Viewer";
        }
    }
}
