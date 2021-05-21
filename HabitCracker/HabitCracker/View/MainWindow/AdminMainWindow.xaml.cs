using System.ComponentModel;
using System.Windows;

namespace HabitCracker.View.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        public AdminMainWindow()
        {
            InitializeComponent();
        }

        private void AdminMainWindow_OnClosing(object sender, CancelEventArgs e)
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