﻿using System;
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

namespace HabbitCracker.View.Menu
{
    /// <summary>
    /// Логика взаимодействия для Habbits.xaml
    /// </summary>
    public partial class Habbits : Page
    {
        public Habbits()
        {
            InitializeComponent();
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            DisplayGrid.Visibility = Visibility.Visible;
        }
    }
}