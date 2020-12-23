using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Windows;


namespace PeopleViewer
{
    public partial class App : Application
    {
        IUnityContainer Container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Application.Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            Container = new UnityContainer();
            Container.LoadConfiguration();
        }

        private void ComposeObjects()
        {
            Application.Current.MainWindow = Container.Resolve<PeopleViewerWindow>();
            Application.Current.MainWindow.Title = "Late Binding with Unity - People Viewer";
        }
    }
}
