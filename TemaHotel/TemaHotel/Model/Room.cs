using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaHotel.DataAccess;

namespace TemaHotel.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int NbrRooms { get; set; }
        public bool Active { get; set; }

        public Room()
        {
            Active = true;
            this.Id = 1;
            initialize();
        }

        public Room(string nm, int price, int nbrRoom)
        {
            this.Id = 1;
            this.Name = nm;
            this.Price = price;
            this.NbrRooms = nbrRoom;
            this.Active = true;
            initialize();
        }

        public virtual ICollection<Picture> RoomPictures { get; set; }
        public virtual ICollection<Facility> RoomFacilieties { get; set; }
        public virtual ICollection<ExtraServices> RoomExtraServices { get; set; }
        public virtual ICollection<Deal> RoomsDeals { get; set; }
        public virtual ICollection<Reservation> RoomsReservations { get; set; }


        public void initialize()
        {
           // RoomPictures = new Collection<Picture>();
            RoomExtraServices = new Collection<ExtraServices>();
            RoomsDeals = new Collection<Deal>();
            RoomsReservations = new Collection<Reservation>();
           

        }

        public void setPictures(Collection<String> pictList)
        {
            Collection<Picture> pict = new Collection<Picture>();
            PicturesServiceLayer pcSv = new PicturesServiceLayer();
            foreach(String st in pictList)
            {
                Picture pc = pcSv.PictureExistInDb(st);
                if (pc == null)
                {
                    pict.Add(new Picture(st));
                }
                else
                {
                    pict.Add(pc);
                }       
            }
            RoomPictures = pict;
        }

        public ObservableCollection<Facility> getRoomFacilities()
        {
            ObservableCollection<Facility> facilities = new ObservableCollection<Facility>();
            FacilityServiceLayer facil = new FacilityServiceLayer();
            facil.getFacilityByRoom(this.Id).ForEach(facilities.Add);
            return facilities;
        }

        public ObservableCollection<String> getRoomPictures()
        {
            ObservableCollection<String> pictures = new ObservableCollection<String>();
            PicturesServiceLayer pictSv = new PicturesServiceLayer();
            pictSv.getPicturesByRoom(this.Id).ForEach(pictures.Add);
            return pictures;
        }

        public void updateRoomsPictures(Collection<String> pictList)
        {
            Collection<Picture> pcts = new Collection<Picture>();
            PicturesServiceLayer pcSv = new PicturesServiceLayer();
            pcSv.deletePicturesByRoom(this.Id);

            foreach (String st in pictList)
            {
                Picture pc = pcSv.PictureExistInDb(st);
                if (pc == null)
                {
                   pcSv.AddPicture(new Picture(st));
                }
               
            }

            pcSv.InsertPictureToRoom(this.Id, pictList);
        }

        public void deleteRoomPictures()
        {
            PicturesServiceLayer pictSv = new PicturesServiceLayer();
            pictSv.deletePicturesByRoom(this.Id);
        }

        public void updateRoomsFacilities(Collection<Facility> facilities)
        {
            FacilityServiceLayer pcSv = new FacilityServiceLayer();
            pcSv.deleteFacilitiesRoom(this.Id);
            pcSv.InsertFacilitiesToRoom(this.Id, facilities);
        }

        public void deleteRoomFacilities()
        {
            PicturesServiceLayer pictSv = new PicturesServiceLayer();
            pictSv.deletePicturesByRoom(this.Id);
        }
    }
}
