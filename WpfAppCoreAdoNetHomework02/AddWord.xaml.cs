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
using System.Windows.Shapes;

namespace WpfAppCoreAdoNetHomework02
{
    /// <summary>
    /// Interaction logic for AddWord.xaml
    /// </summary>
    public partial class AddWord : Window
    {
        public AddWord()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}