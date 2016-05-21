using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace TemaHotel.Model
{
    public class User : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public String Username { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String password;
        public String UserType { get; set; }
        public bool Active { get; set; }

        public User() { Id = 1; Active = true; }
        public User(string username, string name, string email, string pass, string usertp)
        {
            this.Id = 1;
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = pass;
            this.UserType = usertp;
            this.Active = true;

        }

        public User(string username, string name, string email, string pass)
        {
            this.Id = 1;
            this.Username = username;
            this.Name = name;
            this.Email = email;
            this.Password = pass;
            this.UserType = "Client";
            this.Active = true;

        }

        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
