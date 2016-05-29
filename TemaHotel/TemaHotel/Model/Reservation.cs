using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public User CustomerUser { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int NightNr { get; set; }
        public double Price { get; set; }
        public String Status { get; set; }
        public bool Active { get; set; }
        public Collection<int> PriceByRoom { get; set; }

        public Reservation()
        {
            Active = true;
            Status = "WaitingPayment";
        }

        public Reservation(User customerUser, DateTime start, DateTime end )
        {
            Active = true;
            Status = "WaitingPayment";
        this.CustomerUser = customerUser;
        this.Start = start;
        this.End = end;
        NightNr = (End - Start).Days;
     
      
        }

        public virtual ICollection<Room> ReservedRooms { get; set; }
        public virtual ICollection<ExtraServices> ExtraSvReservation { get; set; }
        public virtual ICollection<Deal> DealOfReservation { get; set; }
    }
}
