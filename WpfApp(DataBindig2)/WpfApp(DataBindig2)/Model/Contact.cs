using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp_DataBindig2_.Model
{
    public class Contact : INotifyPropertyChanged
    {
        private int id;
        public int Id { get => id; set => Set(ref id, value); }

        private string name;
        public string Name { get => name; set => Set(ref name, value); }

        private string surname;
        public string Surname { get => surname; set => Set(ref surname, value); }

        private string phone;
        public string Phone { get => phone; set => Set(ref phone, value); }

        public bool IsNew { get; set; } = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public void Set<T>(ref T field, T value, [CallerMemberName]string prop = "")
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
