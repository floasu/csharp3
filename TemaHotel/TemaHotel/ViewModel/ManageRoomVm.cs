using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using TemaHotel.DataAccess;
using TemaHotel.Model;
using TemaHotel.Utilities;

namespace TemaHotel.ViewModel
{
    public class ManageRoomVm : INotifyPropertyChanged
    {
        private ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        private ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();
        private ObservableCollection<Facility> roomFacilities = new ObservableCollection<Facility>();
        private ObservableCollection<BitmapImage> images = new ObservableCollection<BitmapImage>();
        private BitmapImage currentImage;
        private int currentImageIndex=-1;
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private int price;
        private int roomNbr;
        private ICommand createRoomCommand;
        private ICommand editRoomCommand;
        private ICommand deleteRoomCommand;
        private ICommand addFacilitytoRoomCommand;
        private ICommand removeFacilityFromRoomCommand;
        private ICommand nextImageCommand;

        public BitmapImage CurrentImage
        {
            get { return currentImage;}
            set
            {
                currentImage = value;
                OnPropertyChanged("CurrentImage");
            }
        }

        public ManageRoomVm()
        {
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.GetFacilities().ForEach(Facilities.Add);

            RoomServiceLayer roomSv = new RoomServiceLayer();
            roomSv.GetRooms().ForEach(Rooms.Add);

            String path = System.IO.Directory.GetCurrentDirectory() + "../../../Resources/bluesky.jpg";
            Uri source = new Uri(path);
            BitmapImage image = new BitmapImage(source);
            images.Add(image);
        }

        public ICommand CreateRoomCommand
        {
            get
            {
                if(createRoomCommand == null)
                {
                    createRoomCommand = new RelayCommand(CreateRoom);
                }
                return createRoomCommand;
            }
        }
   
        public ICommand EditRoomCommand
        {
            get
            {
                if (editRoomCommand == null)
                {
                    editRoomCommand = new RelayCommand(EditRoom);
                }
                return editRoomCommand;
            }
        }
 
        public ICommand DeleteRoomCommand
        {
            get
            {
                if( deleteRoomCommand == null)
                {
                    deleteRoomCommand = new RelayCommand(DeleteRoom);
                }
                return deleteRoomCommand;
            }
        }

        public ICommand AddFacilitytoRoomCommand
        {
            get
            {
                if (addFacilitytoRoomCommand == null)
                {
                    addFacilitytoRoomCommand = new RelayCommand(AddFacilitytoRoom);
                }
                return addFacilitytoRoomCommand;
            }
        }

        public ICommand RemoveFacilityFromRoomCommand
        {
            get
            {
                if (removeFacilityFromRoomCommand == null)
                {
                    removeFacilityFromRoomCommand = new RelayCommand(RemoveFacilityFromRoom);
                }
                return removeFacilityFromRoomCommand;
            }
        }

        public ICommand NextImageCommand
        {
            get
            {
                if (nextImageCommand == null)
                {
                    nextImageCommand = new RelayCommand(NextImage);
                }
                return nextImageCommand;
            }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
                OnPropertyChanged("Rooms");
            }
        }

        public ObservableCollection<Facility> Facilities
        {
            get { return facilities; }
            set
            {
                facilities = value;
                OnPropertyChanged("Facilities");
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
        
        public void CreateRoom(object param)
        {
            Room room = new Room(Name, Price, RoomNbr);
            room.RoomFacilieties = RoomFacilities;
            RoomServiceLayer sv = new RoomServiceLayer();
            sv.AddRoom(room);
            Rooms.Add(room);
            Name = null;
            Price = 0;
            RoomNbr = 0;
            RoomFacilities.Clear();
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.GetFacilities().ForEach(Facilities.Add);
        }

        public void EditRoom(object param)
        {

        }

        public void DeleteRoom(object param)
        {

        }

        public void AddFacilitytoRoom(object param)
        {
            Facility fac = param as Facility;
            if (fac != null)
            {
                Facilities.Remove(fac);
                RoomFacilities.Add(fac);
            }
        }

        public void RemoveFacilityFromRoom(object param)
        {
            Facility fac = param as Facility;
            if (fac != null)
            {
                Facilities.Add(fac);
                RoomFacilities.Remove(fac);
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
 
        public void NextImage(object param)
        {
            if(currentImageIndex < images.Count - 1)
            {
                currentImageIndex +=1;
                CurrentImage = images[currentImageIndex];
            }
        }

    
    }
}
