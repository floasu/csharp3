using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemaHotel.Model
{
    public class ReservationRoomRow
    {
        public int Id { get; set; }
        public int Price {get; set;}
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int ReservedRoomNumber { get; set; }
        public int NightNr { get { return (End - Start).Days; } }
       
        public bool Active {get; set;}

        public ReservationRoomRow()
        {
            Active = true;
        }

        public ReservationRoomRow(int price, int nbrRoom, Room rm, DateTime start, DateTime end)
        {
            this.Price = price;
            this.ReservedRoomNumber = nbrRoom;
            this.ReservedRoom = rm;
            this.Start = start;
            this.End = end;
            Active = true;
        }

        public virtual Reservation ReservedTo { get; set; }
        public virtual Room ReservedRoom { get; set; }
    }
}
