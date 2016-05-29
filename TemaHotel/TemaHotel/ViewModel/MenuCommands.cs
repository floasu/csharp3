using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TemaHotel.DataAccess;
using TemaHotel.Model;
using TemaHotel.Utilities;

namespace TemaHotel.ViewModel
{
    public class MenuCommands : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public User loggedUser { get; set; }
        public bool showLogin;
        public bool showReservations;
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

            get { return password; }
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

        public bool ShowReservations
        {
            get { return showReservations; }
            set
            {
                showReservations = value;
                OnPropertyChanged("ShowReservations");
            }
        }

        public MenuCommands()
        {
            ShowLogin = false;
            ShowReservations = true;

            RoomServiceLayer rmsv = new RoomServiceLayer();
            //rmsv.GetRooms().ForEach(FreeRooms.Add);
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
                if (cancelLoginCommand == null)
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
                if (manageAccesoriesCommand == null)
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
            ManageRooms rms = new ManageRooms();
            rms.Show();
        }




        private Reservation currentReservation = new Reservation();
        private ObservableCollection<ReservationRoomRow> reservationItems = new ObservableCollection<ReservationRoomRow>();
        private ObservableCollection<Room> freeRooms = new ObservableCollection<Room>();
        private ObservableCollection<Facility> roomFacilities = new ObservableCollection<Facility>();
        private ObservableCollection<Facility> specificFacilities = new ObservableCollection<Facility>();
        private ObservableCollection<String> imagesSource = new ObservableCollection<String>();
        private Room selectedRoom;
        private String currentSourceImage;
        private BitmapImage currentImage;
        private string name;
        private int price;
        private int roomNbr;
        private bool showViewState;
        private bool showEditState;
        private DateTime resStart = DateTime.Now;
        private DateTime resEnd = DateTime.Now;
        private int selectedNumber;
        private ObservableCollection<int> roomNrSource = new ObservableCollection<int>();

        private ICommand searchFreeRoomsCommand;
        private ICommand reserveCommand;

        public int SelectedNumber
        {
            get { return selectedNumber; }
            set
            {
                selectedNumber = value;
                OnPropertyChanged("SelectedNumber");
            }
        }

        public ICommand SearchFreeRoomsCommand
        {
            get
            {
                if (searchFreeRoomsCommand == null)
                {
                    searchFreeRoomsCommand = new RelayCommand(SearchFreeRooms);
                }
                return searchFreeRoomsCommand;
            }
        }

        public ICommand ReserveCommand
        {
            get
            {
                if (reserveCommand == null)
                {
                    reserveCommand = new RelayCommand(Reserve);
                }
                return reserveCommand;
            }
        }

        public ObservableCollection<ReservationRoomRow> ReservationItems
        {
            get { return reservationItems; }
            set
            {
                reservationItems = value;
                OnPropertyChanged("ReservationItems");
            }
        }

        public ObservableCollection<int> RoomNrSource
        {
            get { return roomNrSource; }
            set
            {
                roomNrSource = value;
                OnPropertyChanged("RoomNrSource");
            }

        }

        public Reservation CurrentReservation
        {
            get { return currentReservation; }
            set
            {
                currentReservation = value;
                OnPropertyChanged("CurrentReservation");
            }
        }

        public ObservableCollection<Facility> SpecificFacilities
        {
            get { return specificFacilities; }
            set
            {
                specificFacilities = value;
                OnPropertyChanged("SpecificFacilities");
            }
        }
        
        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                if (selectedRoom != null)
                {
                    setFreeNumberOfRooms();
                    SpecificFacilities = selectedRoom.getRoomFacilities();
                    ImagesSource = selectedRoom.getRoomPictures();
                    if (ImagesSource != null && ImagesSource.Count > 0)
                    {
                        CurrentSourceImage = ImagesSource[0];
                    }
                }
                OnPropertyChanged("SelectedRoom");
            }
        }

        public DateTime ResStart
        {
            get { return resStart; }
            set
            {
                resStart = value;
                OnPropertyChanged("ResStart");
            }
        }

        public DateTime ResEnd
        {
            get { return resEnd; }
            set
            {
                resEnd = value;
                OnPropertyChanged("ResEnd");
            }
        }

        public BitmapImage CurrentImage
        {
            get { return currentImage; }
            set
            {
                currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        public String CurrentSourceImage
        {
            get { return currentSourceImage; }
            set
            {
                if (value != null)
                {
                    currentSourceImage = value;
                    Uri source = new Uri(CurrentSourceImage);
                    CurrentImage = new BitmapImage(source);
                    OnPropertyChanged("CurrentImage");
                }
                else
                {
                    currentSourceImage = null;
                }
            }
        }

        public bool ShowViewState
        {
            get { return showViewState; }
            set
            {
                showViewState = value;
                OnPropertyChanged("ShowViewState");
            }
        }

        public bool ShowEditState
        {
            get { return showEditState; }
            set
            {
                showEditState = value;
                OnPropertyChanged("ShowEditState");
            }
        }

        public ObservableCollection<Room> FreeRooms
        {
            get { return freeRooms; }
            set
            {
                freeRooms = value;
                OnPropertyChanged("FreeRooms");
            }
        }

        public ObservableCollection<Facility> RoomFacilities
        {
            get { return roomFacilities; }
            set
            {
                roomFacilities = value;
                OnPropertyChanged("RoomFacilities");
            }
        }

        public ObservableCollection<String> ImagesSource
        {
            get { return imagesSource; }
            set
            {
                imagesSource = value;
                OnPropertyChanged("ImagesSource");
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

        public int Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged("Price");
            }
        }

        public int RoomNbr
        {
            get { return roomNbr; }
            set
            {
                roomNbr = value;
                OnPropertyChanged("RoomNbr");
            }
        }

        public void Reserve(object param)
        {
            price = SelectedRoom.Price * selectedNumber ;
            ReservationRoomRow resItm = new ReservationRoomRow(price,SelectedNumber, SelectedRoom, ResStart,ResEnd);
            ReservationItems.Add(resItm);
        }

        public void ClearDataControls(object param)
        {
            ClearAddStateData();
        }

        public void ClearAddStateData()
        {
            Name = null;
            Price = 0;
            RoomNbr = 0;
            RoomFacilities.Clear();
            ImagesSource.Clear();
            CurrentImage = null;
        }

        public void setFreeNumberOfRooms()
        {
            int max = 0;
            Collection<int> sourceNumber = new Collection<int>();
            ReservationRoomRowServiceLayer sv = new ReservationRoomRowServiceLayer();
            List<ReservationRoomRow> list = sv.ReservedRoomByRoomDate(SelectedRoom.Id, ResStart, ResEnd);
            foreach (ReservationRoomRow rs in list)
            {
                max += rs.ReservedRoomNumber;
            }
            for (int i = 1; i <= SelectedRoom.NbrRooms - max; i++)
            {
                sourceNumber.Add(i);
            }
            RoomNrSource = new ObservableCollection<int>(sourceNumber);
        }

        public void SearchFreeRooms(object param)
        {
            ReservationRoomRowServiceLayer sv = new ReservationRoomRowServiceLayer();
            RoomServiceLayer svRoom = new RoomServiceLayer();
            List<Room> rm = svRoom.GetRooms();
            foreach (Room r in rm)
            {
                int max = 0;
                List<ReservationRoomRow> list = sv.ReservedRoomByRoomDate(r.Id, ResStart, ResEnd);
                foreach (ReservationRoomRow rs in list)
                {
                    max += rs.ReservedRoomNumber;
                }
                if (max < r.NbrRooms)
                {
                    FreeRooms.Add(r);
                }
            }

        }

    }
}
