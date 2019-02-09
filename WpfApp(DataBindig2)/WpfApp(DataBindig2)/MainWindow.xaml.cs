using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WpfApp_DataBindig2_.Model;
using WpfApp_DataBindig2_.Services;

namespace WpfApp_DataBindig2_
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        //private string contactname;
        //public string ContactName { get => contactname; set => Set(ref contactname, value); }

        //private string contactsurname;
        //public string ContactSurname { get => contactsurname; set => Set(ref contactsurname, value); }

        //private string phone;
        //public string Phone { get => phone; set => Set(ref phone, value); }

        private ObservableCollection<Contact> contacts = new ObservableCollection<Contact>();
        public ObservableCollection<Contact> Contacts { get => contacts; set => Set(ref contacts, value); }

        private Contact selectedContact;
        public Contact SelectedContact { get => selectedContact; set => Set(ref selectedContact, value); }

        public ContactsStorage Storage { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Storage = new ContactsStorage();
            Contacts = new ObservableCollection<Contact>(Storage.GetContacts());

            //Contacts.Add(new Contact
            //{
            //    MyName = "Tim",
            //    Surname="Lenck",
            //    Phone="2345678"
            //});
            //Contacts.Add(new Contact
            //{
            //    MyName = "Tim2",
            //    Surname = "Lenswck",
            //    Phone = "2342345675678"
            //});
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            var window = new AddWindow();
            window.ShowDialog();
            window.NewContact.IsNew = true;
            Contacts.Add(window.NewContact);
        }

        private void OnclearClick(object sender, RoutedEventArgs e)
        {
            SelectedContact.Name = "Empty";
            SelectedContact.Surname = "Empty";
            SelectedContact.Phone = "Empty";
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            Contacts.Remove(SelectedContact);
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            Storage.AddContacts(Contacts.Where(x=>x.IsNew));
        }
    }
}
