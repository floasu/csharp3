using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TemaHotel.DataAccess;
using TemaHotel.Model;
using TemaHotel.Utilities;

namespace TemaHotel.ViewModel
{
    public class ManageUsersVM : INotifyPropertyChanged
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        public event PropertyChangedEventHandler PropertyChanged;
        private string username;
        private string name;
        private string email;
        private string password;
        private string usertype;

        private ICommand createUsCommand;


        public ManageUsersVM()
        {

            UserServiceLayer us = new UserServiceLayer();
            us.GetUsers().ForEach(Users.Add);
        }

        public ICommand CreateUsCommand
        {
            get
            {
                if(createUsCommand == null)
                {
                    createUsCommand = new RelayCommand(CreateUserAccount);
                }
                return createUsCommand;
            }
        }

        public ObservableCollection<User> Users
        {
            get { return users; }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

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

        public string UserType
        {

            get { return usertype; }
            set
            {
                usertype = value;
                OnPropertyChanged("UserType");
            }

        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public void CreateUserAccount(object param)
        {
            if (Password != null && Username != null && Name != null && Email != null && UserType != null)
                {
                    UserServiceLayer userSv = new UserServiceLayer();
                    User newUs = new User(Username, Name, Email, Password, UserType);
                    userSv.AddUser(newUs);
                    Users.Add(newUs);
                    MessageBox.Show("Account created");      
                }
            else
            {
                MessageBox.Show("Invalid Data");

            }
        }


    }
}
 //if (Password.Equals("") == false && Username.Equals("") == false && Name.Equals("") == false && Email.Equals("") == false
 //               && UserType.Equals("") == false )