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
        public double Price { get; set; }
        public String Status { get; set; }
        public bool Active { get; set; }


        public Reservation()
        {
            Active = true;
            Status = "WaitingPayment";
        }

        public Reservation(User customerUser)
        {
            Active = true;
            Status = "WaitingPayment";
            this.CustomerUser = customerUser;
        }

        public virtual ICollection<ReservationRoomRow> ReservedRooms { get; set; }
        public virtual ICollection<Deal> DealOfReservation { get; set; }

    }
}
