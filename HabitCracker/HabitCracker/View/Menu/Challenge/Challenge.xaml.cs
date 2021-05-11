using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HabitCracker.View.Menu.Challenge
{
    /// <summary>
    /// Логика взаимодействия для Challenges.xaml
    /// </summary>
    public partial class Challenges : Page
    {
        public Challenges()
        {
            InitializeComponent();
        }

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainChallenge.Visibility = Visibility.Visible;
            EventGrid.Visibility = Visibility.Visible;
        }
    }
}