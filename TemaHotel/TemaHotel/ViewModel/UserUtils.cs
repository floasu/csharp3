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

        public void CreateUserAccount(object param)
        {
            UserUtils newUser = param as UserUtils;
            if (newUser.Password.Equals(newUser.ConfirmPassword) && newUser.Password.Equals("") == false)
            {
                if (newUser.Username.Equals("") == false && newUser.Name.Equals("") == false && newUser.Email.Equals("") == false)
                {
                    UserServiceLayer userSv = new UserServiceLayer();
                    userSv.AddUser(new User(newUser.Username, newUser.Name, newUser.Email, newUser.Password));
                    MessageBox.Show("Account created please login");
                    foreach (Window wnd in Application.Current.Windows)
                    {
                        if (wnd.Name == "wndSignUp")
                        {
                            wnd.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("You must complete all fields");
                }
            }
            else
            {
                MessageBox.Show("Invalid Password");

            }
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
