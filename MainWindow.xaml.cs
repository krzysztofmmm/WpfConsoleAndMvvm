using System.Windows;
using System.Windows.Controls;

namespace WpfWithoutmvvm
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

        private void AddButton_Click(object sender , RoutedEventArgs e)
        {
            string text = InputTextBox.Text;
            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            ListBox.Items.Add(textBlock);
        }

        private void DeleteButton_Click(object sender , RoutedEventArgs e)
        {
            if(ListBox.SelectedItem != null)
            {
                ListBox.Items.Remove(ListBox.SelectedItem);
            }
        }
    }
}
