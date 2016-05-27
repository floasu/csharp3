using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int NbrRooms { get; set; }
        public bool Active { get; set; }
        public ObservableCollection<String> pictures = new ObservableCollection<String>();


        public Room()
        {
            Active = true;
            this.Id = 1;
        }

        public Room(string nm, int price, int nbrRoom)
        {
            this.Id = 1;
            this.Name = nm;
            this.Price = price;
            this.NbrRooms = nbrRoom;
            this.Active = true;
        }

        public virtual ICollection<Facility> RoomFacilieties { get; set; }
        public virtual ICollection<ExtraServices> RoomExtraServices { get; set; }
        public virtual ICollection<Deal> RoomsDeals { get; set; }
        public virtual ICollection<Reservation> RoomsReservations { get; set; }


        public void initialize()
        {
      //      RoomFacilieties = new Collection<Facility>();
            RoomExtraServices = new Collection<ExtraServices>();
            RoomsDeals = new Collection<Deal>();
            RoomsReservations = new Collection<Reservation>();

        }
    }
}
