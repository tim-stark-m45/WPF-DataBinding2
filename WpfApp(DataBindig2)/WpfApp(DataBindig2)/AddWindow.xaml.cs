using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using WpfApp_DataBindig2_.Model;

namespace WpfApp_DataBindig2_
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window, INotifyPropertyChanged
    {
        private Contact newContact = new Contact();
        public Contact NewContact { get => newContact; set => Set(ref newContact, value); }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(NewContact.Name)
                && !String.IsNullOrWhiteSpace(NewContact.Surname)
                && !String.IsNullOrWhiteSpace(NewContact.Phone))
            {
                Close();
            }
            else
            {
                MessageBox.Show("Error!!");
            }
        }

        public AddWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
