using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp_DataBindig2_.Model;

namespace WpfApp_DataBindig2_.Services
{
    public class ContactsStorage
    {
        public string ConnectionString { get; set; }

        public ContactsStorage()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public void AddContacts(IEnumerable<Contact> contacts)
        {

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                
                SqlTransaction transaction = connection.BeginTransaction();
                SqlCommand command;
                //command.Transaction = transaction;

                try
                {
                    foreach (var item in contacts)
                    {
                        var query= $"insert into Contacts values('{item.Name}','{item.Surname}','{item.Phone}')";
                        command = new SqlCommand(query, connection, transaction);

                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public IEnumerable<Contact> GetContacts()
        {

            var contacts = new ObservableCollection<Contact>();
            var query = "select * from Contacts";

            using (var connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var contact = new Contact();
                            contact.Id = reader.GetInt32(0);
                            contact.Name = reader.GetString(1);
                            contact.Surname = reader.GetString(2);
                            contact.Phone = reader.GetString(3);
                            contacts.Add(contact);
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                return contacts;
            }

        }
    }
}
