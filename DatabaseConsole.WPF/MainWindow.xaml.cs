using System.Linq;
using System.Windows;
using DatabaseConsole.Data.Services;

namespace DatabaseConsole.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ContactsDAL contactsDAL;

        public MainWindow()
        {
            InitializeComponent();
            contactsDAL = new ContactsDAL();
            ReloadData();
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadData();
        }
        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }


        private void ReloadData()
        {
            DataContainer.ItemsSource = contactsDAL.GetContacts();
        }

        private void ClearFields()
        {
            NameTextbox.Text = string.Empty;
            SurnameTextbox.Text = string.Empty;
            SexTextbox.Text = string.Empty;
            PhoneNumberTextbox.Text = string.Empty;
        }
        

    }
}
