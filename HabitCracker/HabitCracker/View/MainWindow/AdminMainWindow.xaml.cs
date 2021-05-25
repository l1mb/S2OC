using System;
using System.ComponentModel;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

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


        private void ToggleTheme_Check(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton btn)
            {
                
            }
        }
    }
}