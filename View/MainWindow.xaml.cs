using System.Windows;
using System.Windows.Input;

namespace WPFToDolist
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnStart_Click(object sender , RoutedEventArgs e)
        {
            // Navigate to MainWindow

            var MainWindow = new MainWindow();
            MainWindow.Show();
            Close();
        }

        private void Window_MouseDown(object sender , MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void BtnMinimize_Click(object sender , RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void BtnClose_Click(object sender , RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



    }
}
