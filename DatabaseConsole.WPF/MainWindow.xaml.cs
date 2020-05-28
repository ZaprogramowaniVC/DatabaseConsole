using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DatabaseConsole.Data.Model;
using DatabaseConsole.Data.Services;

namespace DatabaseConsole.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ContactsDAL contactsDAL;
        Contact currentContact;

        public MainWindow()
        {
            InitializeComponent();
            contactsDAL = new ContactsDAL();
            currentContact = new Contact();
            ReloadData();
        }

        public void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadData();
        }
        public void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            // walidacja numeru telefonu
            string phone = PhoneNumberTextbox.Text;

            if (phone.Length != 9)
            {
                MessageBox.Show("Zły numer telefonu", "Błąd!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            currentContact.Name = NameTextbox.Text;
            currentContact.Surname = SurnameTextbox.Text;
            currentContact.Sex = (SexDictionary)SexCombobox.SelectedIndex + 1;
            currentContact.PhoneNumber = PhoneNumberTextbox.Text;

            if (currentContact.Id != 0)
            {
                contactsDAL.UpdateContact(currentContact);
            }
            else
            {
                contactsDAL.InsertContact(currentContact);
            }
            
            ClearFields();
            ReloadData();
        }

        private void ReloadData()
        {
            DataContainer.ItemsSource = contactsDAL.GetContacts();
        }

        private void ClearFields()
        {
            NameTextbox.Text = string.Empty;
            SurnameTextbox.Text = string.Empty;
            SexCombobox.SelectedIndex = 0;
            PhoneNumberTextbox.Text = string.Empty;
            currentContact = new Contact();
        }

        private void DataContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentContact = (Contact)((ListView)sender).SelectedItem;

            if (currentContact == null)
            {
                currentContact = new Contact();
                return;
            }

            if (currentContact != null)
            {
                NameTextbox.Text = currentContact.Name;
                SurnameTextbox.Text = currentContact.Surname;
                SexCombobox.SelectedIndex = (int)currentContact.Sex - 1;
                PhoneNumberTextbox.Text = currentContact.PhoneNumber;
            }
        }

        public void NewButton_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentContact.Id != 0)
            {
                MessageBoxResult result = MessageBox.Show($"Czy jesteś pewien/pewna że chcesz usunąć: {currentContact.Name} {currentContact.Surname}",
                                                          "Potwierdzenie",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    contactsDAL.DeleteContact(currentContact.Id);
                    ClearFields();
                    ReloadData();
                }
            }

        }
    }
}
