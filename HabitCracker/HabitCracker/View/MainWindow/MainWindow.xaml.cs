using System.ComponentModel;
using System.Windows;

namespace HabitCracker.View.MainWindow
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
        private void MainWindow_OnClosing(object sender, CancelEventArgs e)
        {

            MessageBoxResult dialogResult = MessageBox.Show("Are you sure you want to exist", "Exist", MessageBoxButton.YesNo);
            if (dialogResult == MessageBoxResult.Yes)
            {
                e.Cancel = false;
            }
            else if (dialogResult == MessageBoxResult.No)
            {
                e.Cancel = true;
                //do something else
            }
        }
    }
}