using System;
using System.Collections.Generic;
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
    class UserActionCommand : INotifyPropertyChanged
    {
        private ICommand createUserCommand;
        public event PropertyChangedEventHandler PropertyChanged;

       
        public ICommand CreateUserCommand
        {
            get
            {
                if (createUserCommand == null)
                {
                    createUserCommand = new RelayCommand(CreateUserAccount);
                }
                return createUserCommand;
            }

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
