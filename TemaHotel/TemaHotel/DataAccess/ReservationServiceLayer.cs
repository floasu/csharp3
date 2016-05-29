using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemaHotel.Model;

namespace TemaHotel.DataAccess
{
    public class ReservationServiceLayer
    {

        public List<Reservation> getReservationByDate(int roomId, DateTime start, DateTime end)
        {
            using (var context = new FriendContext())
            {
                var roomIdParameter = new SqlParameter("@RoomId", roomId);
                var dateStartParameter = new SqlParameter("@StartDate", roomId);
                var dateEndParameter = new SqlParameter("@EndDate", roomId);

                var result = context.Database
                    .SqlQuery<Reservation>("GetPicturesRooms @RoomId", roomIdParameter, dateStartParameter, dateEndParameter);

                return result.ToList();
            }

        }
    }
}
