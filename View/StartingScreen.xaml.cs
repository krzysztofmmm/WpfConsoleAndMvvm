using System.Windows;
using System.Windows.Input;

namespace WPFToDolist.View
{
    /// <summary>
    /// Interaction logic for StartingScreen.xaml
    /// </summary>
    public partial class StartingScreen : Window
    {
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to MainWindow
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Close();

        }

        private void Window_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}