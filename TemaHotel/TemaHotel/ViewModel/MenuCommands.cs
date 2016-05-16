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
        public User loggedUser { get; set; }
        private ObservableCollection<ObservableCollection<int>> list;
        private List<Room> rooms = new List<Room>();
        private ICommand signUpCommand;
 
        private ICommand showLoginCommand;
        private ICommand logoutCommand;
        private bool isAuthenticated;
        public event PropertyChangedEventHandler PropertyChanged;

        public MenuCommands()
        {
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

        public ICommand ShowLoginCommand
        {
            get
            {
                if (showLoginCommand == null)
                {
                    UserUtils usUtils = new UserUtils(this);
                    showLoginCommand = new RelayCommand(showLogin);
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

     
        public bool IsAuthenticated
        {
            get { return isAuthenticated; }
            set
            {
                isAuthenticated = value;
                OnPropertyChanged("IsAuthenticated");
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
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

        public void showLogin(object param)
        {
            if (LoggedUser != null)
            {
                IsAuthenticated = true;

            }
            else
            {
                Login login = new Login();
                login.Show();
            }
        }

        public void Logout(object param)
        {
            IsAuthenticated = false;
            LoggedUser = null;
        }

    }
}
