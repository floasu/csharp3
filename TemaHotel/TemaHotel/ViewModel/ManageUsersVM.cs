using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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
        private bool showUpdateDetails;
        public ArrayList userTypeCM = new ArrayList { "Administrator", "Client", "Employee" };

        private ICommand createUsCommand;
        private ICommand deleteUserCommand;
        private ICommand updateUserCommand;
        private ICommand clearDataControlsCommand;
        private ICommand saveUpdateModificationCommand;
        private ICommand cancelUpdateStateCommand;

        public ManageUsersVM()
        {

            UserServiceLayer us = new UserServiceLayer();
            us.GetUsers().ForEach(Users.Add);
        }

        public ArrayList UserTypeCM
        {
            get {return userTypeCM; }
            set
            {
                userTypeCM = value;
                OnPropertyChanged("UserTypeCM");
            }

        }

        public bool ShowUpdateDetails
        {
            get { return showUpdateDetails; }
            set
            {
                showUpdateDetails = value;
                OnPropertyChanged("ShowUpdateDetails");
            }
        }

        public ICommand CreateUsCommand
        {
            get
            {
                if (createUsCommand == null)
                {
                    createUsCommand = new RelayCommand(CreateUserAccount);
                }
                return createUsCommand;
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                if (deleteUserCommand == null)
                {
                    deleteUserCommand = new RelayCommand(DeleteUserAccount);
                }
                return deleteUserCommand;
            }
        }

        public ICommand UpdateUserCommand
        {
            get
            {
                if (updateUserCommand == null)
                {
                    updateUserCommand = new RelayCommand(UpdateUserAccount);
                }
                return updateUserCommand;
            }
        }

        public ICommand ClearDataControlsCommand
        {
            get
            {
                if (clearDataControlsCommand == null)
                {
                    clearDataControlsCommand = new RelayCommand(ClearDataControls);
                }
                return clearDataControlsCommand;
            }
        }

        public ICommand SaveUpdateModificationCommand
        {
            get
            {
                if (saveUpdateModificationCommand == null)
                {
                    saveUpdateModificationCommand = new RelayCommand(SaveUpdateModification);
                }
                return saveUpdateModificationCommand;
            }
        }

        public ICommand CancelUpdateStateCommand
        {
            get
            {
                if (cancelUpdateStateCommand == null)
                {
                    cancelUpdateStateCommand = new RelayCommand(CancelUpdateState);
                }
                return cancelUpdateStateCommand;
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
                if (userSv.GetUserByUsername(newUs.Username) != null)
                {
                    MessageBox.Show("There is already an account with this username");
                    return;
                }
                userSv.AddUser(newUs);
                Users.Add(newUs);
                setUserDetailsNull();
                MessageBox.Show("Account created");
            }
            else
            {
                MessageBox.Show("Invalid Data");

            }
        }

        public void DeleteUserAccount(object param)
        {
            User us = param as User;
            UserServiceLayer sv = new UserServiceLayer();
            sv.DeleteUser(us);
            Users.Remove(us);
        }

        public void UpdateUserAccount(object param)
        {
            User userToUp = param as User;
            if (userToUp != null)
            {
                Username = userToUp.Username;
                Name = userToUp.Name;
                Email = userToUp.Email;
                Password = userToUp.Password;
                UserType = userToUp.UserType;
                ShowUpdateDetails = true;
            }

        }

        public void ClearDataControls(object param)
        {
            setUserDetailsNull();
        }

        public void SaveUpdateModification(object param)
        {
            ShowUpdateDetails = false;
            UserServiceLayer userSv = new UserServiceLayer();
            User us = param as User;
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].Id == us.Id)
                {
                    Users[i].Username = Username;
                    Users[i].Name = Name;
                    Users[i].Email = Email;
                    Users[i].Password = Password;
                    Users[i].UserType = UserType;
                    userSv.ModifyUser(Users[i]);
                    Users.Clear();
                    userSv.GetUsers().ForEach(Users.Add);
                    setUserDetailsNull();
                    break;
                }
            }
        }

        public void CancelUpdateState(object param)
        {
            ShowUpdateDetails = false;
            setUserDetailsNull();
        }

        public void setUserDetailsNull()
        {
            Username = null;
            Name = null;
            Email = null;
            Password = null;
            UserType = null;
        }
    }
}
