using System.Windows;

namespace weirditor;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private void App_OnStartup(object sender, StartupEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
    }
    
    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show("An unhandled exception just occurred: " + e.Exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Error);
        e.Handled = true;
    }
}