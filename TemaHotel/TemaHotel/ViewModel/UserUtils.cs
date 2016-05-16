using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TemaHotel.DataAccess;
using TemaHotel.Model;

namespace TemaHotel.ViewModel
{
    public class UserUtils : INotifyPropertyChanged
    {
        User loggedUser;
        public event PropertyChangedEventHandler PropertyChanged;
        private string username;
        private string name;
        private string email;
        private String password;
        private string confirmPassword;

        public UserUtils(MenuCommands com)
        {
            loggedUser = com.LoggedUser;
        }

        public UserUtils() { }

        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged("Username");
            }

        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }

        public string Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }

        }

        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }

        }

        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }

        }

        public void NewUser(object param)
        {
            SignUp currentSign = new SignUp();
            currentSign.Show();
        }

      

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


    }
}
