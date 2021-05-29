using System;
using System.Collections.Generic;
using System.Windows;

namespace HabitCracker.View.MainWindow
{
    /// <summary>
    /// Логика взаимодействия для AdminMainWindow.xaml
    /// </summary>
    public partial class AdminMainWindow : Window
    {
        private int _theme;
        private readonly List<string> _themes = new List<string>();

        public AdminMainWindow()
        {
            InitializeComponent();
            _theme = 0;

            _themes.Add("LightTheme");
            _themes.Add("DarkTheme");
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            try
            {
                _theme = _theme == 1 ? 0 : 1;
                var uri = new Uri("../../Styles/" + _themes[_theme] + ".xaml", UriKind.Relative);
                var resourceDict = Application.LoadComponent(uri) as ResourceDictionary;
                Application.Current.Resources.Clear();
                Application.Current.Resources.MergedDictionaries.Add(resourceDict);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ToggleTheme_Check(object sender, RoutedEventArgs e)
        {
        }
    }
}