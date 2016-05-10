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
        private SignUp currentSign;
        User newUser = new User();
        public event PropertyChangedEventHandler PropertyChanged;
        public string username;
        public string name;
        private string email;
        private String password;
        private string confirmPassword;

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

        public UserUtils(MenuCommands comm)
        {
           
        }

        public void NewUser(object param)
        {
            currentSign = new SignUp();
            currentSign.Show();
        }

        public void CreateUserAccount(object param)
        {
            if (Password.Equals(ConfirmPassword))
            {
                if (Password != null && Username != null && Name != null && Email != null)
                {
                    User us = new User();
                    us.Username = Username;
                    us.Name = Name;
                    us.Email = Email;
                    us.Password = Password;
                    UserServiceLayer userDb = new UserServiceLayer();
                    userDb.AddUser(us);
                    MessageBox.Show("Account created please login");
                    currentSign.Close();
                    Password = null;
                    Username = null;
                    Email = null;

                }
                else
                {
                    MessageBox.Show("You must complete all fields");

                }
            }
            else
            {
                MessageBox.Show("");

            }
        }

        public void Login(object param)
        {
           
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
