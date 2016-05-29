using Microsoft.Win32;
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
    public class ManageRoomVm : INotifyPropertyChanged
    {
        private ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        private ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();
        private ObservableCollection<Facility> specificFacilities = new ObservableCollection<Facility>();
        private ObservableCollection<Facility> roomFacilities = new ObservableCollection<Facility>();
        private ObservableCollection<String> imagesSource = new ObservableCollection<String>();
        private Room selectedRoom;
        private BitmapImage currentImage;
        private String currentSourceImage;
        public event PropertyChangedEventHandler PropertyChanged;
        private string name;
        private int price;
        private int roomNbr;
        private bool showViewState;
        private bool showEditState;
        private ICommand viewStateCommand;
        private ICommand createRoomCommand;
        private ICommand editRoomCommand;
        private ICommand deleteRoomCommand;
        private ICommand addFacilitytoRoomCommand;
        private ICommand removeFacilityFromRoomCommand;
        private ICommand showAddStateCommand;
        private ICommand uploadPicturesCommand;
        private ICommand deleteImageCommand;
        private ICommand clearDataControlsCommand;
        private ICommand saveChangesCommand;
        private ICommand cancelEditStateCommand;

        public Room SelectedRoom
        {
            get { return selectedRoom; }
            set
            {
                selectedRoom = value;
                if (selectedRoom != null)
                {
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

        public ManageRoomVm()
        {
            showViewState = true;
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.GetFacilities().ForEach(Facilities.Add);

            RoomServiceLayer roomSv = new RoomServiceLayer();
            roomSv.GetRooms().ForEach(Rooms.Add);
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

        public ICommand ViewStateCommand
        {
            get
            {
                if (viewStateCommand == null)
                {
                    viewStateCommand = new RelayCommand(ViewState);
                }
                return viewStateCommand;
            }
        }

        public ICommand CreateRoomCommand
        {
            get
            {
                if (createRoomCommand == null)
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
                if (deleteRoomCommand == null)
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

        public ICommand ShowAddStateCommand
        {
            get
            {
                if (showAddStateCommand == null)
                {
                    showAddStateCommand = new RelayCommand(ShowAddState);
                }
                return showAddStateCommand;
            }
        }

        public ICommand UploadPicturesCommand
        {
            get
            {
                if (uploadPicturesCommand == null)
                {
                    uploadPicturesCommand = new RelayCommand(UploadPictures);
                }
                return uploadPicturesCommand;
            }
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                if (deleteImageCommand == null)
                {
                    deleteImageCommand = new RelayCommand(DeleteImage);
                }
                return deleteImageCommand;
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

        public ICommand SaveChangesCommand
        {
            get
            {
                if (saveChangesCommand == null)
                {
                    saveChangesCommand = new RelayCommand(SaveChanges);
                }
                return saveChangesCommand;
            }

        }

        public ICommand CancelEditStateCommand
        {
            get
            {
                if (cancelEditStateCommand == null)
                {
                    cancelEditStateCommand = new RelayCommand(CancelEditState);
                }
                return cancelEditStateCommand;
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

        public ObservableCollection<Facility> SpecificFacilities
        {
            get { return specificFacilities; }
            set
            {
                specificFacilities = value;
                OnPropertyChanged("SpecificFacilities");
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

        public void ViewState(object param)
        {
            if (ShowViewState == false)
            {
                ClearAddStateData();
                ShowViewState = true;
                if (Rooms != null && Rooms.Count > 0)
                {
                    SelectedRoom = Rooms[0];
                }
            }
        }

        public void ShowAddState(object param)
        {
                ClearAddStateData();
                ShowViewState = false;
                ShowEditState = false;
        }

        public void CreateRoom(object param)
        {
            if (Name != null && Price > 0 && RoomNbr > 0)
            {
                Room room = new Room(Name, Price, RoomNbr);
                room.RoomFacilieties = RoomFacilities;
                room.setPictures(ImagesSource);
                RoomServiceLayer sv = new RoomServiceLayer();
                sv.AddRoom(room);
                Rooms.Add(room);
                ClearAddStateData();
                Facilities.Clear();
                FacilityServiceLayer facil = new FacilityServiceLayer();
                facil.GetFacilities().ForEach(Facilities.Add);
                MessageBox.Show("Room Created");
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }
        }

        public void EditRoom(object param)
        {
            Room rm = param as Room;
            if (rm != null)
            {
                ClearAddStateData();
                ShowViewState = false;
                ShowEditState = true;
                SelectedRoom = rm;
                RoomFacilities = rm.getRoomFacilities();
                foreach (Facility f in RoomFacilities)
                {
                    foreach (Facility fac in Facilities)
                    {
                        if (fac.Id == f.Id)
                        {
                            Facilities.Remove(fac);
                            break;
                        }
                    }
                }
                Name = rm.Name;
                Price = rm.Price;
                RoomNbr = rm.NbrRooms;
            }
        }

        public void DeleteRoom(object param)
        {
            Room rm = param as Room;
            if (rm != null)
            {
                RoomServiceLayer sv = new RoomServiceLayer();
                sv.DeleteRoom(rm);
                Rooms.Remove(rm);
                ClearAddStateData();
            }
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

        public void UploadPictures(object param)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Upload Pictures";
            openFileDialog.Filter = "Text files  (*.bmp, *.jpg, *.bmp;*.jpg|*.*";
            openFileDialog.Multiselect = true;
            Nullable<bool> result = openFileDialog.ShowDialog();
            String[] filename = null;
            if (result == true)
            {
                filename = openFileDialog.FileNames;
                foreach (String st in filename)
                {
                    if (ImagesSource.Contains(st) == false)
                    {
                        ImagesSource.Add(st);
                        Uri source = new Uri(st);
                        BitmapImage image = new BitmapImage(source);
                    }
                }
            }

        }

        public void DeleteImage(object param)
        {
            String pict = param as String;
            CurrentImage = null;
            ImagesSource.Remove(pict);
        }

        public void ClearDataControls(object param)
        {
            ClearAddStateData();
        }    

        public void SaveChanges(object param)
        {
            if (Name != null && Price > 0 && RoomNbr > 0)
            {
                SelectedRoom.Price = Price;
                SelectedRoom.Name = Name;
                SelectedRoom.NbrRooms = RoomNbr;
                SelectedRoom.updateRoomsFacilities(RoomFacilities);
                SelectedRoom.updateRoomsPictures(ImagesSource);
                RoomServiceLayer sv = new RoomServiceLayer();
                sv.ModifyRoom(SelectedRoom);
                Rooms.Clear();
                sv.GetRooms().ForEach(Rooms.Add);
                FacilityServiceLayer facil = new FacilityServiceLayer();
                Facilities.Clear();
                facil.GetFacilities().ForEach(Facilities.Add);
                MessageBox.Show("Room Updated");

                ClearAddStateData();
                ShowViewState = true;
                ShowEditState = false;
                if (Rooms != null && Rooms.Count > 0)
                {
                    SelectedRoom = Rooms[0];
                }
            }
            else
            {
                MessageBox.Show("Invalid Data");
            }   
        }

        public void CancelEditState(object param)
        {
            ClearAddStateData();
            ShowViewState = true;
            ShowEditState = false;
            if (Rooms != null && Rooms.Count > 0)
            {
                SelectedRoom = Rooms[0];
            }

        }

        public void ClearAddStateData()
        {
            Name = null;
            Price = 0;
            RoomNbr = 0;
            RoomFacilities.Clear();
            ImagesSource.Clear();
            CurrentImage = null;
            Facilities.Clear();
            specificFacilities.Clear();
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.GetFacilities().ForEach(Facilities.Add);
        }
    }
}
