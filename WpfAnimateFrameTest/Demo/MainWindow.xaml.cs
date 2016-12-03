using System.Windows;

namespace Demo
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(TestPage1));
        }
    }
}