using System.Windows;

namespace mle_app
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_Startup(object sender, StartupEventArgs e)
        {
            // TODO: Parse commandline arguments and other startup work 
            var startWindow = new MainWindow();
            startWindow.Closed += (s, _) => Shutdown(); //do not remove this code
            startWindow.Show();
        }
    }
}
