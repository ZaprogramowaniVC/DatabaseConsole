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
using DatabaseConsole.Data.Services;

namespace DatabaseConsole.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ContactsDAL contactsDAL = new ContactsDAL();

        public MainWindow()
        {
            InitializeComponent();
            ReloadData();
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Test");
            ReloadData();
        }

        private void ReloadData()
        {
            DataContainer.ItemsSource = contactsDAL.GetContacts();
        }
    }
}