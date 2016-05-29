using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaHotel.Model;

namespace TemaHotel.DataAccess
{
    public class ReservationRoomRowServiceLayer
    {
        public List<ReservationRoomRow> ReservedRoomByRoomDate(int roomId, DateTime start, DateTime end)
        {
            List<ReservationRoomRow> lst = new List<ReservationRoomRow>();
            using (var context = new FriendContext())
            {
                var activeUsers = from res in context.RowsOfReservations
                                  where (res.ReservedRoom.Id == roomId &&
                                  ((res.Start <= start && res.End >= end) || (res.Start >= start && res.Start <= end) ||
                                     (res.End >= start && res.End <= end) || (res.Start >= start && res.End <= end))
                                  && res.Active == true)
                                  select res;
                if (activeUsers.ToList<ReservationRoomRow>() != null)
                {
                    return activeUsers.ToList<ReservationRoomRow>();
                }

            }
            return lst;
        }

    }
}
