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
    public class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User loggedUser { get; set; }
        private ObservableCollection<ObservableCollection<int>> list;
        private List<Room> rooms = new List<Room>();
        public bool showLogin;
        private bool isAuthenticated;
        private bool showMenu;
        private string username;
        private string password;

        private ICommand signUpCommand;
        private ICommand showLoginCommand;
        private ICommand logoutCommand;
        private ICommand loginCommand;
        private ICommand cancelLoginCommand;
        private ICommand manageRoomCommand;
        private ICommand manageAccesoriesCommand;
        private ICommand manageUsersCommand;

        public bool ShowMenu
        {
            get { return showMenu; }
            set
            {
                showMenu = value;
                OnPropertyChanged("ShowMenu");
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
     
        public string Password
        {

            get { return password;}
            set 
            {
                password = value;
                OnPropertyChanged("Password");
            }

        }
        public bool ShowLogin 
        {
            get { return showLogin; }
            set
            {
                showLogin = value;
                OnPropertyChanged("ShowLogin");
            }
        }
       
    

        public MenuCommands()
        {
            ShowLogin = false;
            list = new ObservableCollection<ObservableCollection<int>>();
            for (int j = 0; j < 5; j++)
            {
                List.Add(new ObservableCollection<int>());
                List[j].Add(j);
            }
        }

        public ObservableCollection<ObservableCollection<int>> List
        {
            get { return list; }
            set
            {
                list = value;
                OnPropertyChanged("List");
            }
        }

        public List<Room> Rooms
        {
            get { return rooms; }
            set
            {
                if (rooms == null)
                {
                    rooms = new List<Room>();
                }
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public User LoggedUser
        {
            get { return loggedUser; }
            set
            {
                loggedUser = value;
                OnPropertyChanged("LoggedUser");
            }
        }

        
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(Login);
                }
                return loginCommand;
            }

        }

        public ICommand CancelLoginCommand
        {
            get
            {
                if(cancelLoginCommand == null)
                {
                    cancelLoginCommand = new RelayCommand(CancelLogin);
                }
                return cancelLoginCommand;
            }
        }
      
        public ICommand ShowLoginCommand
        {
            get
            {
                if (showLoginCommand == null)
                {
                    UserUtils usUtils = new UserUtils(this);
                    showLoginCommand = new RelayCommand(DisplayLoginGrd);
                }
                return showLoginCommand;
            }
        }

        public ICommand LogoutCommand
        {
            get
            {
                if (logoutCommand == null)
                {
                    logoutCommand = new RelayCommand(Logout);
                }
                return logoutCommand;
            }

        } 
     
        public ICommand ManageUsersCommand
        {
            get
            {
                if (manageUsersCommand == null)
                {
                   UserUtils utRoom = new UserUtils();
                    manageUsersCommand = new RelayCommand(utRoom.ManageUsers); 
                }
                return manageUsersCommand;
            }

        }

        public ICommand ManageRoomCommand
        {
            get
            {
                if (manageRoomCommand == null)
                {
                    manageRoomCommand = new RelayCommand(ManageRoom);
                }
                return manageRoomCommand;
            }

        }

        public ICommand SignUpCommand
        {
            get
            {
                if (signUpCommand == null)
                {
                    UserUtils newUs = new UserUtils(this);
                    signUpCommand = new RelayCommand(newUs.NewUser);

                }
                return signUpCommand;
            }
        }

        public ICommand ManageAccesoriesCommand
        {
            get
            {
                if(manageAccesoriesCommand == null)
                {
                    manageAccesoriesCommand = new RelayCommand(ShowAccesories);
                }
                return manageAccesoriesCommand;
            }
        }

        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set
            {
                isAuthenticated = value;
                OnPropertyChanged("IsAuthenticated");
            }
        }

        public void databaseTest(object param)
        {
            //UserServiceLayer userInsert = new UserServiceLayer();
            //User testus = new User("usertest", "nametest", "emailtest", "pass");
            //userInsert.AddUser(testus);
            ////     userInsert.DeleteUser(testus);
            ////     List<User> us = userInsert.GetUsers();
            //Room rm = new Room(5, "dfg", "ert");
            //Room rm2 = new Room(6, "fgh", "tt");
            //Rooms.Add(rm);
            //Rooms.Add(rm2);
            //List.Add(new ObservableCollection<int>());
            //List[List.Count - 1].Add(112);
            if (IsAuthenticated == true)
            {
                IsAuthenticated = false;
            }
            else IsAuthenticated = true;
        }

        public void DisplayLoginGrd(object param)
        {
            ShowLogin = true;      
        }

        public void Login(object param)
        {
            UserUtils loginDetails = param as UserUtils;
            if (Username.Equals("") == false && Password.Equals("") == false)
            {
                UserServiceLayer svUs = new UserServiceLayer();
                User toLog = svUs.GetUserByUsername(Username);
                if (toLog != null && toLog.Password.Equals(Password) == true)
                {
                    LoggedUser = toLog;
                    IsAuthenticated = true;
                    ShowLogin = false;
                    Username = null;
                    Password = null;
                    if (LoggedUser.UserType.Equals("Administrator") == true)
                    {
                        ShowMenu = true;
                    }
                    MessageBox.Show("Succesfully Login");
                }
                else MessageBox.Show("Wrong Details");
            }
            else MessageBox.Show("Please complete all fields");
        }

        public void Logout(object param)
        {
            IsAuthenticated = false;
            LoggedUser = null;
            ShowMenu = false;
        }

        public void CancelLogin(object param)
        {
            ShowLogin = false;
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
 
        private void ShowAccesories(object param)
        {
            ManageFacilities manWnd = new ManageFacilities();
            manWnd.Show();
        }

        public void ManageRoom(object param)
        {
            ManageRooms rooms = new ManageRooms();
            rooms.Show();
        }
    }
}
